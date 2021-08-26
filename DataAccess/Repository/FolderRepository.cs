using Domain.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.DataModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{

    public class FolderRepository : IFolderRepository
    {
        private ChaBarNoteContext _context;
        public FolderRepository(ChaBarNoteContext context)
        {
            _context = context;
        }
        public void CreateFolder(string foldername, int userid, int parentid)
        {
            Folder f = new Folder();
            f.FolderName = foldername;
            f.ParentId = parentid;
            f.UserId = userid;
            _context.Folder.Add(f);
            _context.SaveChanges();
        }
        public int GetFolderCount(int userid)
        {
            int y = _context.Folder
               .Count(x => x.UserId == userid);
            return y;
        }
        public int GetFolderID(string foldername, int userid, int parentid)
        {
            var folderid = _context.Folder.Where<Folder>(x => x.FolderName == foldername && x.UserId == userid && x.ParentId == parentid)
                .Select(x => x.FolderId).SingleOrDefault();
            return folderid;
        }
        public int GetFolderIDFirst(int userid)
        {
            return _context.Folder
                 .Where(x => x.UserId == userid)
                 .Select(x => x.FolderId).FirstOrDefault();
        }
        public List<string> GetSubFolderNames(int folderid, int userid)
        {
            return _context.Folder
                 .Where(x => x.UserId == userid && x.ParentId == folderid)
                 .Select(x => x.FolderName)?.ToList();
        }
        public bool IsFolderNameExist(string foldername, int userid, int parentid)
        {
            return _context.Folder.Any(x => x.FolderName == foldername && x.UserId == userid && x.ParentId == parentid);
        }
        public bool IsFolderIDExist(int folderid, int userid)
        {
            return _context.Folder.Any(x => x.FolderId == folderid && x.UserId == userid);
        }
        public void SetFolderName(int folderId, int userId, string newName)
        {
            var f = _context.Folder.Where(x => x.FolderId == folderId && x.UserId == userId).FirstOrDefault();
            f.FolderName = newName;
            _context.Update(f);
            _context.SaveChanges();
        }
        //====================================================================
        public int GetParentId(int userId, int folderId)
        {
            var checkRootFolder = _context.Folder.Where<Folder>(
                x => x.FolderId == folderId && x.UserId == userId)
                 .Select(x => x.ParentId).SingleOrDefault();
            return checkRootFolder;
        }
        public void DeleteFolder(int userId, int folderId)
        {
            List<int> subFolders = GetSubFolders(userId, folderId);
            List<int> TotalFolder = new List<int>();
            TotalFolder.Add(folderId);
            TotalFolder.AddRange(subFolders);

            Dictionary<int, List<int>> subNotes = GetSubNotes(userId, TotalFolder);

            //Delete Notes
            foreach (KeyValuePair<int, List<int>> item in subNotes)
            {
                int key = item.Key;
                List<int> list = item.Value;
                foreach (int n in list)
                {
                    Note note = new Note()
                    {
                        UserId = userId,
                        NoteId = n,
                        FolderId = key,
                    };
                    _context.Note.Remove(note);
                    _context.SaveChanges();
                }
            }
            //Delete Folders
            for (int i = 0; i < TotalFolder.Count; i++)
            {
                Folder folder = new Folder()
                {
                    UserId = userId,
                    FolderId = TotalFolder[i],
                };
                _context.Folder.Remove(folder);
                _context.SaveChanges();
            }
        }
        public List<int> GetSubFolders(int userId, int folderId)
        {
            List<int> subFolders = _context.Folder
                 .Where(x => x.UserId == userId && x.ParentId == folderId)
                 .Select(x => x.FolderId)?.ToList();
            if (subFolders.Count > 0)
            {
                for (int i = 0; i < subFolders.Count; i++)
                {
                    List<int> findSubFolders = _context.Folder
                    .Where(x => x.UserId == userId && x.ParentId == subFolders[i])
                    .Select(x => x.FolderId)?.ToList();
                    subFolders.AddRange(findSubFolders);
                }
            }
            return subFolders;
        }
        public Dictionary<int, List<int>> GetSubNotes(int userId, List<int> foldersId)
        {
            List<int> subNotes = new List<int>();
            Dictionary<int, List<int>> subNotesDictionary =
                       new Dictionary<int, List<int>>();
            for (int i = 0; i < foldersId.Count; i++)
            {
                List<int> findSubNotes = _context.Note
                .Where(x => x.UserId == userId && x.FolderId == foldersId[i])
                .Select(x => x.NoteId)?.ToList();
                subNotesDictionary.Add(foldersId[i], findSubNotes);
                subNotes.AddRange(findSubNotes);////
            }
            return subNotesDictionary;
        }

        public Dictionary<int, List<string>> SearchFolder(int userId, string folderName )
        {
            var folders = _context.Folder
                .Where(x => x.UserId == userId && x.FolderName.Contains(folderName.Trim()))
                //EF.Functions.Like(folderName,"%"+folderName+"%"))
                .Select(x=> 
                new Folder { FolderId =x.FolderId,FolderName = x.FolderName,ParentId = x.ParentId })
                .ToList();

           
            return  GetFolderDirectory(folders);
        }

        public Dictionary<int, List<string>> GetFolderDirectory(List<Folder> folders)
        {

           
            Dictionary<int, List<string>> folderDir =
                       new Dictionary<int, List<string>>();

            for (int i = 0; i < folders.Count; i++)
            {
                List<string> folderPath = new List<string>();
                int node = folders[i].ParentId;
                folderPath.Add($"FolderName = {folders[i].FolderName}  FolderId={folders[i].FolderId}");
                while (node != 0)
                {
                    var res =  _context.Folder
                        .Where(c => c.FolderId == node /*folders[i].ParentId*/)
                        .Select(x => new Folder 
                        { ParentId = x.ParentId,FolderId = x.FolderId,FolderName = x.FolderName})
                        .SingleOrDefault();
                    folderPath.Add($"FolderName = {res.FolderName}  FolderId={res.FolderId}");
                    node = res.ParentId;
                }
                folderDir.Add(folders[i].FolderId, folderPath);
            }
            return folderDir;
        }
    }
}

using Domain.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DataAccess.Repository
{
    public interface IFolderRepository
    {
        void CreateFolder(string foldername, int userid, int parentid);
        int GetFolderID(string foldername, int userid,int parentid);
        bool IsFolderNameExist(string foldername, int userid, int parentid);
        bool IsFolderIDExist(int folderid,int userid);
        int GetFolderCount(int userid);
        List<string> GetSubFolderNames(int folderid, int userid);
        int GetFolderIDFirst(int userid);
        void SetFolderName(int folderId, int userId, string newName);
        //====================================================================
        int GetParentId(int userId, int folderId);
        void DeleteFolder(int userId, int folderId);
        List<int> GetSubFolders(int userId, int folderId);
        Dictionary<int, List<int>> GetSubNotes(int userId, List<int> foldersId);/////////////////////////


        Dictionary<int, List<string>> SearchFolder(int userId, string folderName);

        Dictionary<int, List<string>> GetFolderDirectory(List<Folder> folders);
    }
}

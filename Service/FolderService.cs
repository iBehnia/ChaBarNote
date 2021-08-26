using Domain.DataAccess.Repository;
using Domain.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class FolderService : IFolderService
    {
    
        private IFolderRepository _folderRepository;
        private IUserRepository _userRepository;
        public FolderService(IFolderRepository folderRepository, IUserRepository userRepository)
        {
            _folderRepository = folderRepository;
            _userRepository = userRepository;
        }
        public int CreateFolder(string foldername, int userid, int parentid)
        {
            _folderRepository.CreateFolder(foldername, userid, parentid);
            var folderid = _folderRepository.GetFolderID(foldername, userid, parentid);
            return folderid;
        }
        public bool IsFolderNameExist(string foldername, int userid, int parentid)
        {
            return _folderRepository.IsFolderNameExist(foldername, userid, parentid);
        }
        public bool IsFolderIDExist(int folderid, int userid)
        {
            var res = _folderRepository.IsFolderIDExist(folderid, userid);
            return res;
        }
        public bool CheckFolderCount(int userid)
        {
            int number = _folderRepository.GetFolderCount(userid);
            return number > 10;
        }
        public string GetSubFolderNames(int folderid, int userid)
        {

            List<string> namesList = _folderRepository.GetSubFolderNames(folderid, userid);
            if (namesList == null || namesList.Count == 0) return "There is not Folder in here.";
            string names = "";
            foreach (var name in namesList)
            {
                names += name + "\n";
            }
            return names;
        }
        public void SetFolderName(int folderId, int userId, string newName)
        {
            _folderRepository.SetFolderName(folderId, userId, newName);
        }
        //====================================================================
        public bool isRootFolder(int userId, int folderId)
        {
            int checkRootFoldered= _folderRepository.GetParentId(userId, folderId);
            bool check = (checkRootFoldered == 0) ? true :false;
            return check;
        }
        public void DeleteFolder(int userId, int folderId)
        {
            _folderRepository.DeleteFolder(userId, folderId);
        }
    }
}

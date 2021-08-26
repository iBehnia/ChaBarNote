using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service
{
    public interface IFolderService
    {
        int CreateFolder(string foldername, int userid , int parentid);
        bool IsFolderNameExist(string foldername, int userid, int parentid);
        bool IsFolderIDExist(int folderid, int userid);
        bool CheckFolderCount(int userid);
        string GetSubFolderNames(int folderid, int userid);
        void SetFolderName(int folderId, int userId, string newName);
        //====================================================================
        bool isRootFolder(int userId, int folderId);
        void DeleteFolder(int userId, int folderId);
    }
}

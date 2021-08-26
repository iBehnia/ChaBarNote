using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service
{
    public enum UserState
    {
        Normal = 1,
        Special = 2,
    }
    public interface IUserService
    {
        string LoginUser(string username);
        bool IdentityTokenUser(string token);
        void LogoutUser(string username);
        bool IsLoginValid(string username, string password);
        bool IsLogoutValid(string username, string token);
        int GetUserID(string token);
        UserState GetUserState(int userid);
        void SetUserPassword(int userId, string newPassword);
        string GetUserPassword(int userId);
        List<string> SearchFolderDirectory(int userId, string folderName);
    }
}

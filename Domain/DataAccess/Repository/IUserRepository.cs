using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DataAccess.Repository
{
    public interface IUserRepository
    {
        string GetUserToken(string username);
        void ClearUserToken(string username);
        bool SetUserToken(string username, string token);
        string GetUserPassword(string username);
        bool IsTokenExists(string token);
        int GetUserID(string token);
        int GetUserState(int userid);
        int GetUserIDWithUserName(string username);
        void SetUserPassword(int userId , string newPassword);
        string GetUserPassword(int userId);
        void SetUserState(int userId , int state);
        bool IsExistUserId(int userid);
        void SetUserPasswordByAdmin(int userId, string newPassword);
    }
}



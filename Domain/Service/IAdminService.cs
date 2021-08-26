using Domain.DataModel;
using Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Service
{
    public interface IAdminService
    {
        Result<string> LoginAdmin(string username, string password);
        Task<bool> IdentityTokenAdmin(string token);
        Task<int> AddUser(string username, string password, int state);
        Task<bool> IsExistsUser(string username);
        string IdentityUser(string username);
        bool IsUserIdExists(int userid);
        void SetUserState(int userId, int userState);
        List<string> GetAllUsers();
        void SetUserPassword(int userId, string newPassword);
        Result<string> LogOutAdmin(string Token);
        bool IsLogoutValid(string token);
    }
}

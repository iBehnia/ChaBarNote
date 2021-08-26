using Domain.DataModel;
using Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.DataAccess.Repository
{
    public interface IAdminRepository
    {
        Result<string> Login(string username, string password);
        Task<bool> IsExistsToken(string token);
        Task<User> AddUserRepo(string username, string password, int state);
        Task<bool> IsExistUser(string username);
        List<GetAllUser> GetAllUsers();
        Result<string> LogOut(string token);
        bool IdentityTokenAdmin(string token);
        void ClearAdminToken(string token);
    }
}

using Domain.DataAccess.Repository;
using Domain.DataModel;
using Domain.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ChaBarNoteContext chaBarNoteContext;

        public AdminRepository(ChaBarNoteContext chaBarNoteContext)
        {
            this.chaBarNoteContext = chaBarNoteContext;
        }
        // Add User To Database Without Identity
        public async Task<User> AddUserRepo(string username, string password, int state)
        {
            // state=1 => noraml / state=2 => VIP
            await chaBarNoteContext.User.AddAsync(new User { UserName = username, Password = password, State = state, Token = null });
            await chaBarNoteContext.SaveChangesAsync();
            return new User { UserName = username, Password = password, State = state };
        }
        public List<GetAllUser> GetAllUsers()
        {
            return chaBarNoteContext.GetAllUsers.ToList();
        }
        // Check Admin Token For AdminToken Identity
        public async Task<bool> IsExistsToken(string token)
        {
            return await chaBarNoteContext.Admin.AnyAsync(r => r.Token == token);
        }
        // Check If User Exist For Preventing Add To Database
        public async Task<bool> IsExistUser(string username)
        {
            return await chaBarNoteContext.User.AnyAsync(r => r.UserName == username);

        }
        public Task<bool> IsExistUserId(int userid)
        {
            throw new NotImplementedException();
        }
        public Result<string> Login(string username, string password)
        {
            var admin = chaBarNoteContext.Admin.SingleOrDefault(x => x.UserName == username && x.Password == password);
            if (admin == null)
            {
                return new Result<string>
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Incorrect username or password"
                };
            }

            var token = Guid.NewGuid().ToString();
            if (string.IsNullOrEmpty(admin.Token))
            {
                admin.Token = token;
                chaBarNoteContext.SaveChanges();

                return new Result<string>
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = "Login is successful",
                    Data = token
                };
            }

            admin.Token = token;
            chaBarNoteContext.SaveChanges();

            return new Result<string>
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Relogin is successful",
                Data = token
            };
        }
        public void ClearAdminToken(String token)
        {
            var find = chaBarNoteContext.Admin
                .FirstOrDefault(x => x.UserName == "Admin");
            find.Token = null;
            chaBarNoteContext.Admin.Update(find);
            chaBarNoteContext.SaveChanges();
        }
        public bool IdentityTokenAdmin(string token)
        {
            var result = chaBarNoteContext.Admin.Any(a => a.Token == token);
            return result;
        }
        public Result<string> LogOut(string token)
        {
            if (token == null)//string.IsNullOrEmpty(token)
            {
                return new Result<string>
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = "Enter Token \n or Admin is Already Log Out",
                    Data = token
                };
            }
            else return new Result<string>
            {
                StatusCode = System.Net.HttpStatusCode.NoContent,
                Message = "NoContent",
                Data = token
            };
        }
    }
}

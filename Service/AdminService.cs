using Domain.DataAccess.Repository;
using Domain.DataModel;
using Domain.Dto;
using Domain.Service;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UserState = Domain.Service.UserState;

namespace Service
{ 
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFolderRepository _folderRepository;

        public AdminService(IAdminRepository adminRepository, IUserRepository userRepository, IFolderRepository folderRepository)
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            _folderRepository = folderRepository;
        }
        //Add User
        public async  Task<int> AddUser(string username, string password, int state)
        {

             await _adminRepository.AddUserRepo(username, password, state);
            int userid = _userRepository.GetUserIDWithUserName(username);
            _folderRepository.CreateFolder("Root", userid, 0);
            int folderid = _folderRepository.GetFolderIDFirst(userid);
            return folderid;

        }

        public List<string> GetAllUsers()
        {
            List<string> result = new List<string>();
            var users = _adminRepository.GetAllUsers();
            string temp = "";
            foreach (var item in users)
            {
                temp = item.UserName + " " + "[" +  item.State + "]" ;
                result.Add(temp);

            }
            
            return result;
        }

        //Identity AdminToken
        public async Task<bool> IdentityTokenAdmin(string token)
        {
            return await _adminRepository.IsExistsToken(token);
        }

        public string IdentityUser(string username)
        {
            //Regex regex = new Regex("[a-zA-Z]+$");
            if (username == "Admin" || username == "admin")
            {
                return "UserName Is Exist Before";
            }
            
            //else if (!regex.IsMatch(username))
            //{
            //    return "username must have only english character";
            //}
           
            else
            {
                return null;
            }
        }

        //Check If User Exist
        public async Task<bool> IsExistsUser(string username)
        {
            return await _adminRepository.IsExistUser(username);
        }

        public bool IsUserIdExists(int userid)
        {
            return _userRepository.IsExistUserId(userid);
        }

        public Result<string> LoginAdmin(string username, string password)
        {
            return _adminRepository.Login(username, password);
        }

        public void SetUserPassword(int userId, string newPassword)
        {
            _userRepository.SetUserPasswordByAdmin(userId, newPassword);
        }

        public void SetUserState(int userId, int userState)
        {
            _userRepository.SetUserState(userId, userState);
        }
        public Result<string> LogOutAdmin(string Token)
        {
            _adminRepository.ClearAdminToken(Token);
            return _adminRepository.LogOut(Token);
        }
        public bool IsLogoutValid(string token)
        {
            bool TokenIsValid = _adminRepository.IdentityTokenAdmin(token);
            if (token == null || !TokenIsValid) return false;
            return true;
        }
    }
}

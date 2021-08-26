using Domain.DataAccess.Repository;
using Domain.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IFolderRepository _folderRepository;
        public UserService(IUserRepository userRepository, IFolderRepository folderRepository)
        {
            _userRepository = userRepository;
            _folderRepository = folderRepository;
        }
        public bool IdentityTokenUser(string token)
        {
            return _userRepository.IsTokenExists(token);
        }

        public bool IsLoginValid(string username, string password)
        {
            var pass = _userRepository.GetUserPassword(username);
            if (pass == null || pass != password) return false;
            return true;
        }

        public string LoginUser(string username)
        {
            var token = Guid.NewGuid().ToString();
            _userRepository.SetUserToken(username, token);
            return token;
        }


        public bool IsLogoutValid(string username, string token)
        {
            var tkn = _userRepository.GetUserToken(username);
            if (tkn == null || tkn != token) return false;
            return true;
        }


        public void LogoutUser(string username)
        {
            _userRepository.ClearUserToken(username);
        }

        public int GetUserID(string token)
        {
            return _userRepository.GetUserID(token);
        }

        public UserState GetUserState(int userid)
        {
            return (UserState)_userRepository.GetUserState(userid);
        }
        public string GetUserPassword(int userId)
        {
            return _userRepository.GetUserPassword(userId);
        }
        public void SetUserPassword(int userId, string newPassword)
        {
            _userRepository.SetUserPassword(userId, newPassword);
        }

        public List<string> SearchFolderDirectory(int userId, string folderName)
        {

            List<string> result = new List<string>();
            var response = _folderRepository.SearchFolder(userId, folderName);

            foreach (KeyValuePair<int, List<string>> item in response)
            {

                List<string> list = item.Value;
                string temp = "";
                list.Reverse();
                foreach (string n in list)
                {
                    temp += n + "/";
                }
                
                result.Add(temp);
                result.Add("*********************************************************************");
            }
            return result;

        }
    }
}

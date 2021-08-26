using Domain.DataAccess.Repository;
using Domain.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private ChaBarNoteContext _context;

        public UserRepository(ChaBarNoteContext context)
        {
            _context = context;
        }

        public void ClearUserToken(string username)
        {
            var finduser = _context.User
                .FirstOrDefault(x=>x.UserName == username);
            finduser.Token = null;
            _context.User.Update(finduser);
            _context.SaveChanges();
        }

        public int GetUserID(string token)
        {
            int userid = _context.User
                .Where<User>(x => x.Token == token)
                .Select(x => x.UserId).SingleOrDefault<int>();
            return userid;
        }

        public int GetUserIDWithUserName(string username)
        {
           return _context.User
                .Where(x => x.UserName == username)
                .Select(x => x.UserId).FirstOrDefault();
        }

        public string GetUserPassword(string username)
        {
           string pass = _context.User
                .Where<User>(x => x.UserName == username)
                .Select(x => x.Password)?.SingleOrDefault<String>()?.ToString();
            
            return pass;
        }

        public string GetUserPassword(int userId)
        {
            var p = _context.User
                .Where(x => x.UserId == userId)
                .Select(x => x.Password)
                .FirstOrDefault();
            return p;
        }

        public void SetUserPassword(int userId, string newPassword)
        {
            var U = _context.User
                .Where(x => x.UserId == userId)
                .FirstOrDefault();
            U.Password = newPassword ;

            _context.Update(U);
            _context.SaveChanges();
        }

        public int GetUserState(int userid)
        {
            return _context.User
                .Where(x => x.UserId == userid)
                .Select(x => x.State).FirstOrDefault();
        }

        public string GetUserToken(string username)
        {
            var token = _context.User.Where<User>(x => x.UserName == username).Select(x => x.Token)?.SingleOrDefault<String>()?.ToString();
            return token;
        }

        public bool IsTokenExists(string token)
        {
            var result = _context.User.Any(a => a.Token == token);
            return result;
        }

        public bool SetUserToken(string username, string token)
        {
            var finduser = _context.User.FirstOrDefault(x => x.UserName == username);
            if (finduser == null) return false;
            finduser.Token = token;
            _context.User.Update(finduser);
            _context.SaveChanges();
            return true;
        }

        public void SetUserState(int userId, int state)
        {
            var finduser = _context.User.FirstOrDefault(x => x.UserId == userId);
            finduser.State = state;
            _context.User.Update(finduser);
            _context.SaveChanges();
            
        }

        public bool IsExistUserId(int userid)
        {
            var result = _context.User.Any(a => a.UserId == userid);
            return result;
        }

        public void SetUserPasswordByAdmin(int userId, string newPassword)
        {
            var U = _context.User
              .Where(x => x.UserId == userId)
              .FirstOrDefault();
            U.Password = newPassword;
            U.Token = null;
            _context.Update(U);
            _context.SaveChanges();
        }
    }
}

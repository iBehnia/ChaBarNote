using System;
using System.Collections.Generic;

namespace Domain.DataModel
{
    public partial class User
    {
        public User()
        {
            Folder = new HashSet<Folder>();
            Note = new HashSet<Note>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public int State { get; set; }

        public ICollection<Folder> Folder { get; set; }
        public ICollection<Note> Note { get; set; }
    }
}

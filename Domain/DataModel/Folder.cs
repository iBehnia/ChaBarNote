using System;
using System.Collections.Generic;

namespace Domain.DataModel
{
    public partial class Folder
    {
        public Folder()
        {
            Note = new HashSet<Note>();
        }

        public int FolderId { get; set; }
        public string FolderName { get; set; }
        public int ParentId { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<Note> Note { get; set; }
    }
}

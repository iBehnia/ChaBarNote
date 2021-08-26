using System;
using System.Collections.Generic;

namespace Domain.DataModel
{
    public partial class Note
    {
        public int NoteId { get; set; }
        public string NoteTitle { get; set; }
        public string NoteText { get; set; }
        public int FolderId { get; set; }
        public int UserId { get; set; }

        public Folder Folder { get; set; }
        public User User { get; set; }
    }
}

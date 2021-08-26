using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel.Note
{
    public class DeleteNoteModel
    {
        [Required(ErrorMessage = "Token is Required")]
        public string Token { get; set; }
        [Required(ErrorMessage = "FolderId is Required")]
        public int FolderId { get; set; }
        [Required(ErrorMessage = "NoteId is Required")]
        public int NoteId { get; set; }
    }
}

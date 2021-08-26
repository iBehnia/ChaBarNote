using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel.Note
{
    public class EditNoteModel
    {
        [Required(ErrorMessage = "Title is Required")]
        [MinLength(1, ErrorMessage = "Title MinLength must be >=1")]
        [MaxLength(10, ErrorMessage = "Title MaxLength must be <=10")]
        [RegularExpression("[a-zA-Z]+$", ErrorMessage = "NoteTitle must have only english character")]
        public string Title { get; set; }
        [MaxLength(50, ErrorMessage = "Text MaxLength must be <=50")]
        public string Text { get; set; }
        [Required(ErrorMessage = "Token is Required")]
        public string Token { get; set; }
        [Required(ErrorMessage = "NoteId is Required")]
        public int NoteId { get; set; }
    }
}

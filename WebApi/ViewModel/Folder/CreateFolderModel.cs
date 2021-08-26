using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class CreateFolderModel
    {
        [Required(ErrorMessage ="Enter Foldername plz")]
        [MinLength(1, ErrorMessage = "FolderName Must be >1")]
        [MaxLength(10, ErrorMessage = "FolderName Must be <10")]
        [RegularExpression("[a-zA-Z]+$",ErrorMessage = "FolderName must have only english character")]
        public string FolderName { get; set; }
        public int ParentID { get; set; }
        [Required(ErrorMessage = "Enter Token plz")]
        public string Token { get; set; }
    }
}

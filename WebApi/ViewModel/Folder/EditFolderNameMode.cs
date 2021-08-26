using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel.Folder
{
    public class EditFolderNameModel
    {
        [Required(ErrorMessage = "Enter Token plz")]
        public String Token { get; set; }
        [Required(ErrorMessage = "Enter FolderID plz")]
        public int FolderId { get; set; }
        [Required(ErrorMessage = "Enter Foldername plz")]
        [MinLength(1, ErrorMessage = "FolderName Must be >=1")]
        [MaxLength(10, ErrorMessage = "FolderName Must be <=10")]
        [RegularExpression("[a-zA-Z]+$", ErrorMessage = "FolderName must have only english character")]
        public String  name { get; set; }
    }
}

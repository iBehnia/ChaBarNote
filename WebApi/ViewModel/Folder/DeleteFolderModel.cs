using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel.Folder
{
    public class DeleteFolderModel
    {
        [Required(ErrorMessage = "Token is Required")]
        public string Token { get; set; }
        [Required(ErrorMessage = "FolderId is Required")]
        public int FolderId { get; set; }
    }
}

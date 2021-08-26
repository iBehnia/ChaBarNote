using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel.User
{
    public class SearchFoldersModel
    {
        [Required(ErrorMessage ="Token is Required")]
        public string Token { get; set; }
        [Required(ErrorMessage = "FolderName is Required")]
        public string FolderName { get; set; }
    }
}

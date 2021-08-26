using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel.Folder
{
    public class GetFolderNamesModel
    {
        [Required(ErrorMessage = "Token is Required")]
        public string Token { get; set; }
        public int FolderId { get; set; }
    }
}

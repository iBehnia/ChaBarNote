using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel.User
{
    public class GetFolderItemsModel
    {
        [Required(ErrorMessage = "Token is Required")]
        public string Token { get; set; }
        [Required(ErrorMessage = "FolderId is Required")]
        public int FolderId { get; set; }
    }
}

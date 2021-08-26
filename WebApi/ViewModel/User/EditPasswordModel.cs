using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel.User
{
    public class EditPasswordModel
    {
        [Required(ErrorMessage = "Token is Required")]
        public string Token { get; set; }
        [Required(ErrorMessage = "NewPassWord is Required")]
        [MinLength(8, ErrorMessage = "NewPassword Must be >8")]
        [MaxLength(20, ErrorMessage = "NewPassword Must be <20")]
        public string NewPassword { get; set; }
    }
}

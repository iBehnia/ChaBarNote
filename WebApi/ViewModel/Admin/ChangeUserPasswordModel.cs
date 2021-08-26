using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel.Admin
{
    public class ChangeUserPasswordModel
    {
        [Required(ErrorMessage = "Admin Token is Required")]
        public string AdminToken { get; set; }

        [Required(ErrorMessage = "UserId is Required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "NewPassWord is Required")]
        [MinLength(8, ErrorMessage = "NewPassword Must be >=8")]
        [MaxLength(20, ErrorMessage = "NewPassword Must be <=20")]
        public string NewPassword { get; set; }
    }
}

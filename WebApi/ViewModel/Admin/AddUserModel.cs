using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel.Admin
{
    public class AddUserModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "UserName is Required")]
        [MinLength(1, ErrorMessage = "UserName Must be >1")]
        [MaxLength(10, ErrorMessage = "UserName Must be <10")]
        [RegularExpression("[a-zA-Z]+$",ErrorMessage = "username must have only english character")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [MinLength(8, ErrorMessage = "Password Must be >8")]
        [MaxLength(20, ErrorMessage = "Password Must be <20")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Token is Required")]
        public string Token { get; set; }
        [Required(ErrorMessage = "State is Required")]
        [Range(1, 2, ErrorMessage = "State must be  1(Normal User) or 2(VIP User)")]
        public int State { get; set; }
    }
}

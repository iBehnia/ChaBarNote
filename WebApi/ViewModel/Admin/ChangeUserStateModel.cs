using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel.Admin
{
    public class ChangeUserStateModel
    {
        [Required(ErrorMessage = "Token is Required")]
        public string Token { get; set; }
        [Required(ErrorMessage = "UserId is Required")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "State is Required")]
        [Range(1, 2, ErrorMessage = "State must be  1(Normal User) or 2(VIP User)")]
        public int NewState { get; set; }
    }
}

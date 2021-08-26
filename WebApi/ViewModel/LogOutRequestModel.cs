using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class LogOutRequestModel
    {
        public string UserName { get; set; }
        public string token { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Domain.DataModel
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}

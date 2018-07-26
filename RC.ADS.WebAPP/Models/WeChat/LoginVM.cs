using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RC.ADS.WebAPP.Models.WeChat
{
    public class LoginVM
    {
        public string Username { get; set; }

         
        public string Password { get; set; }
 
        public string ReturnUrl { get; set; }
    }
}

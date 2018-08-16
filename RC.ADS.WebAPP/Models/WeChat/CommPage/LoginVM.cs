using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RC.ADS.WebAPP.Models.WeChat
{
    public class LoginVM
    {
        public string Username { get; set; }
        public string ImageValidateCode { get; set; }
        public string PhoneValidateCode { get; set; }
        public string ReferrerId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RC.ADS.WebAPP.Models.WeChat
{
    public class ModifPasswordDto
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string Msg { get; set; }
    }
}

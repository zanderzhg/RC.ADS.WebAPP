using RC.ADS.Data.Entity.AD_Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RC.ADS.WebAPP.Models.WeChat
{
    public class AccountInfoDto
    {
        public AccountInfoDto() { AccountInfos = new List<AccountInfo>(); }

        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public List<AccountInfo> AccountInfos { get; set; }
    }
}

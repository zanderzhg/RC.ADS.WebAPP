using System;
using System.Collections.Generic;
using System.Text;

namespace RC.ADS.Data.Entity.AD_Account
{
    public class AccountInfo
    {
        public AccountInfo() { Id = Guid.NewGuid().ToString("N"); }
        public string Id { get; set; }
    }
}

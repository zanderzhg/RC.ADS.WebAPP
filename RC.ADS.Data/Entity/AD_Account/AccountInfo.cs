using RC.ADS.Data.EnumDict;
using System;
using System.Collections.Generic;
using System.Text;

namespace RC.ADS.Data.Entity.AD_Account
{
    public class AccountInfo
    {
        public AccountInfo() { Id = Guid.NewGuid().ToString("N"); }
        public string Id { get; set; }
        public Account OwnerAccount { get; set; }
        public decimal Money { get; set; }
        public AccountInfoChangeTpyeEnum AccountInfoChangeTpye { get; set; }
        public string Describe { get; set; }
    }
}

using RC.ADS.Data.Entity.AD_Menber;
using System;
using System.Collections.Generic;
using System.Text;

namespace RC.ADS.Data.Entity.AD_Account
{
    public class Account
    {
        public Account() { Id = Guid.NewGuid().ToString("N"); }
        public string Id { get; set; }
        public decimal AccountSum { get; set; }
        public Menber Owner { get; set; }
        public ICollection<AccountInfo> AccountInfoList { get; set; }
    }
}

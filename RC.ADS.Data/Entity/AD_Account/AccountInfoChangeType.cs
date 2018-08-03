using System;
using System.Collections.Generic;
using System.Text;

namespace RC.ADS.Data.Entity.AD_Account
{
    public class AccountInfoChangeType
    {
        public AccountInfoChangeType() { Id = Guid.NewGuid().ToString("N"); }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Describe { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RC.ADS.Data.Entity.AD_Account
{
    //充值项目
    public class TopupItem
    {
        public TopupItem() { Id = Guid.NewGuid().ToString("N"); }
        public string Id { get; set; }
        public string TopupItemName { get; set; }
        /// <summary>
        /// 价钱 分
        /// </summary>
        public int Price { get; set; }
        public string Describe { get; set; }
        public bool IsDalete { get; set; }
    }
}

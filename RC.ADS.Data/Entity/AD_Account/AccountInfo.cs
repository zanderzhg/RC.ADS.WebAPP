using RC.ADS.Data.Entity.AD_Menber;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RC.ADS.Data.Entity.AD_Account
{
    public class AccountInfo
    {
        public AccountInfo() { Id = Guid.NewGuid().ToString("N"); }
        public string Id { get; set; }
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public Menber Owner { get; set; }
        /// <summary>
        /// 结算前金额
        /// </summary>
        public decimal BeforeMoney { get; set; }
        /// <summary>
        /// 结算金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 结算后金额
        /// </summary>
        public decimal AfterMoney { get; set; }

        public string AccountInfoChangeTpyeId { get; set; }
        [ForeignKey("AccountInfoChangeTpyeId")]
        public AccountInfoChangeType AccountInfoChangeTpye { get; set; }
        public string Describe { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

using RC.ADS.Data.Entity.AD_Menber;
using RC.ADS.Data.Enum;
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
        /// <summary>
        /// 交易号
        /// </summary>
        public string TradeNo { get; set; }
        /// <summary>
        /// 交易名称
        /// </summary>
        public string TradeName { get; set; }
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public Menber Owner { get; set; }
        /// <summary>
        /// 结算前金额
        /// </summary>
        public int BeforeMoney { get; set; }
        /// <summary>
        /// 结算金额
        /// </summary>
        public int Money { get; set; }
        /// <summary>
        /// 结算后金额
        /// </summary>
        public int AfterMoney { get; set; }
        public AccountInfoChangeTypeEnum AccountInfoChangeTpye { get; set; }
        public string Describe { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

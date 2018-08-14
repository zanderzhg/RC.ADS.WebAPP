using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RC.ADS.WebAPP.Models.WeChat
{
    public class AccountInfoDto
    {
        public string Id { get; set; }
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

        public string AccountInfoChangeTpyeName { get; set; }
        public string Describe { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

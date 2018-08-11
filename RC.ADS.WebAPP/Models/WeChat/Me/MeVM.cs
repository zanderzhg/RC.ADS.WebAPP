using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RC.ADS.WebAPP.Models.WeChat
{
    public class MeVM
    {
        public string ManberName { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public int IntegralSum { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 订单总数
        /// </summary>
        public int OrderSum { get; set; }
    }
}

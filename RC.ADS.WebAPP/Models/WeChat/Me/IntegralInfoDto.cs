using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RC.ADS.WebAPP.Models.WeChat
{
    public class IntegralInfoDto
    {
        public string Id { get; set; }
        /// <summary>
        /// 结算前积分
        /// </summary>
        public int BeforeScore { get; set; }
        /// <summary>
        /// 结算积分
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 结算后积分
        /// </summary>
        public int AfterScore { get; set; }
        public string IntegralInfoChangeTypeName { get; set; }
        public string Describe { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

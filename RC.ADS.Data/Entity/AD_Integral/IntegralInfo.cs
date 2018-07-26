using RC.ADS.Data.EnumDict;
using System;
using System.Collections.Generic;
using System.Text;

namespace RC.ADS.Data.Entity.AD_Integral
{
    public class IntegralInfo
    {
        public IntegralInfo() { Id = Guid.NewGuid().ToString("N"); }
        public string Id { get; set; }
        public Integral OwnerIntegral { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        public int Score { get; set; }
        public IntegralInfoChangeTypeEnum IntegralInfoChangeType { get; set; }
        public string Describe { get; set; }
    }
}

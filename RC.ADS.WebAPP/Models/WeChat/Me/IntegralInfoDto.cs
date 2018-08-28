using RC.ADS.Data.Entity.AD_Integral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RC.ADS.WebAPP.Models.WeChat
{
    public class IntegralInfoDto
    {
        public IntegralInfoDto() { IntegralInfos = new List<IntegralInfo>(); }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public List<IntegralInfo> IntegralInfos { get; set; }
    }
}

using RC.ADS.Data.Entity.AD_Menber;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RC.ADS.Data.Entity.AD_Integral
{
    public class IntegralInfo
    {
        public IntegralInfo() { Id = Guid.NewGuid().ToString("N"); }
        public string Id { get; set; }
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public Menber Owner { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        public int Score { get; set; }
        public string IntegralInfoChangeTypeId { get; set; }
        [ForeignKey("IntegralInfoChangeTypeId")]
        public IntegralInfoChangeType IntegralInfoChangeType { get; set; }
        public string Describe { get; set; }
    }
}

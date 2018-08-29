using RC.ADS.Data.Entity.AD_Account;
using RC.ADS.Data.Entity.AD_Integral;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RC.ADS.Data.Entity.AD_Menber
{
   public class Menber
    {
        public Menber() { Id = Guid.NewGuid().ToString("N"); AccountSum = 0; IntegralSum = 0; RegisterTime = DateTime.Now; IsDelete = false; }
        public string Id { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ReferrerId { get; set; }

        public string WeChatOpenId { get; set; }
        [ForeignKey("ReferrerId")]
        /// <summary>
        /// 推荐人
        /// </summary>
        public Menber Referrer { get; set; }
        public string LastLoginGuidCode { get; set; }
        /// <summary>
        /// 单位： 分
        /// </summary>
        public int AccountSum { get; set; }
        public int IntegralSum { get; set; }
        public DateTime RegisterTime { get; set; }
        public bool IsDelete { get; set; }
    }
}

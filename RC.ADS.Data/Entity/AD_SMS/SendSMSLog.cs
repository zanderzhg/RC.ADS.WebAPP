using System;
using System.Collections.Generic;
using System.Text;

namespace RC.ADS.Data.Entity.AD_SMS
{
   public class SendSMSLog
    {
        public SendSMSLog() { Id = Guid.NewGuid().ToString("N"); }
        public string Id { get; set; }
        public string Appid { get; set; }
        public string SMSAppTemplateId { get; set; }
        /// <summary>
        /// 发送之前数量
        /// </summary>
        public int QuantityBeforeSend { get; set; }
        /// <summary>
        /// 接收人手机号码列表，按英文逗号“,”分隔
        /// </summary>
        public string phoneNumbers { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 发送之后数量
        /// </summary>
        public int QuantityAfterSend { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        public DateTime CreateTime { get; set; }
    }
}

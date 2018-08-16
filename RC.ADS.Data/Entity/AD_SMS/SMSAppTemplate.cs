using RC.ADS.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace RC.ADS.Data.Entity.AD_SMS
{
   public class SMSAppTemplate
    {
        public SMSAppTemplate() { Id = Guid.NewGuid().ToString("N"); }
        public string Id { get; set; }
        public string Appid { get; set; }
        public string AppName { get; set; }
        public string Appkey { get; set; }
        public string TemplateId { get; set; }
        public SMSTemplateType TemplateType { get; set; }
        public string SMSAppTemplateName { get; set; }
        public string Describe { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

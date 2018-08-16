using System;
using System.Collections.Generic;
using System.Text;

namespace RC.ADS.Data.Entity.AD_SMS
{
   public class SMSApp
    {
        public SMSApp() { Id = Guid.NewGuid().ToString("N"); }

        public string Id { get; set; }
        public string appid { get; set; }
        public string appkey { get; set; }
        public string SMSAppName { get; set; }
        public string Describe { get; set; }
    }
}

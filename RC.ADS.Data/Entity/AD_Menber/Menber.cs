using System;
using System.Collections.Generic;
using System.Text;

namespace RC.ADS.Data.Entity.AD_Menber
{
   public class Menber
    {
        public Menber() { Id = Guid.NewGuid().ToString("N"); }
        public string Id { get; set; }
        public string ManberName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        /// <summary>
        /// 推荐人
        /// </summary>
        public Menber Referrer { get; set; }
    }
}

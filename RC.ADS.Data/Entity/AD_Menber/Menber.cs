﻿using RC.ADS.Data.Entity.AD_Account;
using RC.ADS.Data.Entity.AD_Integral;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string ReferrerId { get; set; }
        [ForeignKey("ReferrerId")]
        /// <summary>
        /// 推荐人
        /// </summary>
        public Menber Referrer { get; set; }
        public decimal AccountSum { get; set; }
        public int IntegralSum { get; set; }
        public DateTime RegisterTime { get; set; }
    }
}

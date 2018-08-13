using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RC.ADS.WebAPP.Models.WeChat
{
    public class OrderInfoDto
    {
        public string Id { get; set; }
        public string OrderName { get; set; }
        public decimal Price { get; set; }
        public string OrderStatuName { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}

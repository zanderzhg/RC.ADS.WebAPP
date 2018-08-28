using RC.ADS.Data.Entity.AD_Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RC.ADS.WebAPP.Models.WeChat
{
    public class OrderInfoDto
    {
        public OrderInfoDto(){ Orders = new List<Order>(); }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public List<Order> Orders { get; set; }
    }
}

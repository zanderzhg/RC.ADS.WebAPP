using System;
using System.Collections.Generic;
using System.Text;

namespace RC.ADS.Data.Entity.AD_Order
{
    public class Order
    {
        public Order() { Id = Guid.NewGuid().ToString("N"); }
        public string Id { get; set; }
    }
}

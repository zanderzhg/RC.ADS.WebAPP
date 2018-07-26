using RC.ADS.Data.Entity.AD_Menber;
using RC.ADS.Data.EnumDict;
using System;
using System.Collections.Generic;
using System.Text;

namespace RC.ADS.Data.Entity.AD_Order
{
    public class Order
    {
        public Order() { Id = Guid.NewGuid().ToString("N"); }
        public string Id { get; set; }
        public Menber  Owner { get; set; }
        public decimal Price { get; set; }
        public OrderStatusEnum orderStatu { get; set; }
        public string Description { get; set; }
    }
}

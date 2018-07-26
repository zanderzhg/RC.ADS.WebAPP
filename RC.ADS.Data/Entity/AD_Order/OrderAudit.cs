using RC.ADS.Data.EnumDict;
using System;
using System.Collections.Generic;
using System.Text;

namespace RC.ADS.Data.Entity.AD_Order
{
   public class OrderAudit
    {
        public OrderAudit() { Id = Guid.NewGuid().ToString("N"); }
        public string Id { get; set; }
        public Order AuditEntity { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public DateTime Creatime { get; set; }

    }
}

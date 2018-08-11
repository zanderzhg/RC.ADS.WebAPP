using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RC.ADS.Data.Entity.AD_Order
{
   public class OrderStatusChange
    {
        public OrderStatusChange() { Id = Guid.NewGuid().ToString("N"); }
        public string Id { get; set; }
        public string OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order OrderEntity { get; set; }
        public decimal Price { get; set; }
        public string OrderStatusId { get; set; }
        [ForeignKey("OrderStatusId")]
        public OrderStatus orderStatu { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

using RC.ADS.Data.Entity.AD_Menber;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RC.ADS.Data.Entity.AD_Order
{
    public class Order
    {
        public Order() { Id = Guid.NewGuid().ToString("N"); }
        public string Id { get; set; }
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public Menber  Owner { get; set; }
        public decimal Price { get; set; }
        public string OrderStatusId { get; set; }
        [ForeignKey("OrderStatusId")]
        public OrderStatus orderStatu { get; set; }
        public string Description { get; set; }
    }
}

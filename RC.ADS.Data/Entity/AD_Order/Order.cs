using RC.ADS.Data.Entity.AD_Menber;
using RC.ADS.Data.Enum;
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
        public string OrderName { get; set; }
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public Menber  Owner { get; set; }
        public decimal Price { get; set; }
         public OrderStatusEnum OrderStatu { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
    }
}

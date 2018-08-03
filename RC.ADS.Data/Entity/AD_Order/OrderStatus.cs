using System;
using System.Collections.Generic;
using System.Text;

namespace RC.ADS.Data.Entity.AD_Order
{
    public class OrderStatus
    {
        ///// <summary>
        ///// 已下单
        ///// </summary>
        //ordered = 0,
        ///// <summary>
        ///// 制作中
        ///// </summary>
        //Making = 1,
        ///// <summary>
        ///// 制作完成
        ///// </summary>
        //MakingComplete = 2,
        ///// <summary>
        ///// 配送中
        ///// </summary>
        //distribution = 3,
        ///// <summary>
        ///// 已完成
        ///// </summary>
        //OrderComplete = 4
        public OrderStatus() { Id = Guid.NewGuid().ToString("N"); }
        public string Id { get; set; }
        public string Name { get; set; }
        public string ChineseName { get; set; }
        public string Describe { get; set; }

    }
}

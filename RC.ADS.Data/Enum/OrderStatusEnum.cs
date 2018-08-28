using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RC.ADS.Data.Enum
{
    public enum OrderStatusEnum
    {
        /// <summary>
        /// 已下单
        /// </summary>
        [Display(Name = "充值")]
        ordered = 0,
        /// <summary>
        /// 制作中
        /// </summary>
        [Display(Name = "制作中")]
        Making = 1,
        /// <summary>
        /// 制作完成
        /// </summary>
        [Display(Name = "制作完成")]
        MakingComplete = 2,
        /// <summary>
        /// 配送中
        /// </summary>
        [Display(Name = "配送中")]
        distribution = 3,
        /// <summary>
        /// 已完成
        /// </summary>
        [Display(Name = "已完成")]
        OrderComplete = 4
    }
}

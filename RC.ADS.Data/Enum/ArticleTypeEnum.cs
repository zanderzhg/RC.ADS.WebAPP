using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RC.ADS.Data.Enum
{
    public enum ArticleTypeEnum
    {
        /// <summary>
        /// 关于我们
        /// </summary>
        [Display(Name = "关于我们")]
        AboutUS_1 = 1,
        /// <summary>
        /// 业务&报价
        /// </summary>
        [Display(Name = "业务&报价")]
        Business_2 = 2,
        /// <summary>
        /// 下单
        /// </summary>
        [Display(Name = "下单")]
        PlaceOrder_3 = 3,
        /// <summary>
        /// 公告
        /// </summary>
        [Display(Name = "公告")]
        Notice_4 = 4,
        /// <summary>
        /// 优惠
        /// </summary>
        [Display(Name = "优惠")]
        SpecialOffers_5 = 5,
        /// <summary>
        /// 客服
        /// </summary>
        [Display(Name = "客服")]
        CustomerService_6 = 6,
        /// <summary>
        /// 轮播图
        /// </summary>
        [Display(Name = "轮播图")]
        Slideshows_7 = 7



    }
}

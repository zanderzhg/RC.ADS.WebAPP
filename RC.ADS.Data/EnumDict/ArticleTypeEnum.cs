using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RC.ADS.Data.EnumDict
{
    public enum ArticleTypeEnum
    {
        [Display(Name = "业务范围文章")]
        Business,
        [Display(Name = "活动文章")]
        Activity,
        PlaceOrder,
        Python,
        SQL,
        Oracle

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RC.ADS.Data.Enum
{

    public enum IntegralInfoChangeTypeEnum
    {
        /// <summary>
        /// 新增 1
        /// </summary>
        [Display(Name = "新增")]
        Recharge_1 = 1,
        /// <summary>
        /// 减少 -1
        /// </summary>
        [Display(Name = "减少")]
        Consume_1_ = -1
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RC.ADS.Data.Enum
{

    public enum AccountInfoChangeTypeEnum
    {
        /// <summary>
        /// 充值 1
        /// </summary>
        [Display(Name = "充值")]
        Recharge_1 = 1,
        /// <summary>
        /// 消费 -1
        /// </summary>
        [Display(Name = "消费")]
        Consume_1_ = -1
    }
}

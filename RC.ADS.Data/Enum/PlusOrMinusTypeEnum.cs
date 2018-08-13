using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RC.ADS.Data.Enum
{
    /// <summary>
    /// 正数
    /// 负数
    /// </summary>
    public enum PlusOrMinusTypeEnum
    {
        /// <summary>
        /// 正数 1
        /// </summary>
         [Display(Name = "添加基数（1）")]

        
        Plus = 1,
        /// <summary>
        /// 负数 -1
        /// </summary>
         [Display(Name ="减少基数（-1）")]
        Minus = -1
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RC.ADS.WebAPP.Comm
{
    public static class EnumHelper
    {
        public static string GetChineseName<TEnum>(this TEnum eum)

            where TEnum : struct

        {

            Type type = eum.GetType();

            //使用反射获取该枚举的成员信息

            foreach (var memberInfo in type.GetMembers())

            {

                //判断名称是否相等

                if (memberInfo.Name != eum.ToString()) continue;



                //反射出自定义属性

                foreach (Attribute attr in memberInfo.GetCustomAttributes(true))

                {

                    var test = attr as DisplayAttribute;

                    if (test == null) continue;

                    return test.Name;

                }

            }

            //如果没有描述特性的值，返回该枚举值得字符串形式

            return eum.ToString();

        }
        public static SelectList GetSelectListByEnum<TEnum>(this TEnum enumObj, int? selectedItem = null)
        {
            if (Enum.GetValues(typeof(TEnum)).Length > 0)
            {
                List<SelectListItem> listResult = new List<SelectListItem>();
                foreach (TEnum e in Enum.GetValues(typeof(TEnum)))
                {
                    if (selectedItem != null && selectedItem == Convert.ToInt32(e)) // 选中
                    {
                        SelectListItem item = new SelectListItem
                        {
                            Value = Convert.ToInt32(e).ToString(),    // 传输值
                            Text = e.ToString(),      // 显示值
                            Selected = true
                        };
                        listResult.Add(item);
                    }
                    else
                    {
                        SelectListItem item = new SelectListItem     // 不选中
                        {
                            Value = Convert.ToInt32(e).ToString(),     // 传输值
                            Text = e.ToString()      // 显示值
                        };
                        listResult.Add(item);
                    }
                }
                if (selectedItem != null)
                    return new SelectList(listResult, "Value", "Text", selectedItem);
                else
                    return new SelectList(listResult, "Value", "Text");
            }
            return null;
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RC.ADS.WebAPP.Models.WeChat
{
    public class RegisterVM
    {
        [Required, MaxLength(256)]
        public string Username { get; set; }
        public string ImageValidateCode { get; set; }
        public string PhoneValidateCode { get; set; }
        public string ReferrerId { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}

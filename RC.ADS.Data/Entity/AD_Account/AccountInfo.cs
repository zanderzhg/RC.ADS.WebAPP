using RC.ADS.Data.Entity.AD_Menber;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RC.ADS.Data.Entity.AD_Account
{
    public class AccountInfo
    {
        public AccountInfo() { Id = Guid.NewGuid().ToString("N"); }
        public string Id { get; set; }
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public Menber Owner { get; set; }
        public decimal Money { get; set; }
        public string AccountInfoChangeTpyeId { get; set; }
        [ForeignKey("AccountInfoChangeTpyeId")]
        public AccountInfoChangeType AccountInfoChangeTpye { get; set; }
        public string Describe { get; set; }
    }
}

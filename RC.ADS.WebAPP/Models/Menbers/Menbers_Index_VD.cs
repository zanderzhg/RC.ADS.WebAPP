using RC.ADS.Data.Entity.AD_Menber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RC.ADS.WebAPP.Models.Menbers
{
    public class Menbers_Index_VD
    {
        public Menbers_Index_VD() { Menbers = new List<Menber>(); }
        public string MenberName { get; set; }
        public string MenberPhone { get; set; }
        public List<Menber> Menbers { get; set; }
    }
}

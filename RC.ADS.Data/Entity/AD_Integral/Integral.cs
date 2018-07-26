using RC.ADS.Data.Entity.AD_Menber;
using System;
using System.Collections.Generic;
using System.Text;

namespace RC.ADS.Data.Entity.AD_Integral
{
    public class Integral
    {
        public Integral() { Id = Guid.NewGuid().ToString("N"); }
        public string Id { get; set; }
        public int IntegralSum { get; set; }
        public  Menber Owner { get; set; }
    }
}

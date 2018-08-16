using Microsoft.Extensions.DependencyInjection;
using RC.ADS.WebAPP.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RC.ADS.WebAPP
{
    public class ConfigureRCServices
    {
        public static void ConfigureSMSServices(IServiceCollection services)
        {
            services.AddTransient(typeof(SMSHelper), typeof(SMSHelper));
        }
    }
}

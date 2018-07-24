using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RC.ADS.WebAPP.Controllers
{
    public class BaseController : Controller
    {
        public DbContext rcDbContext { get; set; }
        public BaseController(DbContext rcDbContext) { this.rcDbContext = rcDbContext; }
         
    }
}
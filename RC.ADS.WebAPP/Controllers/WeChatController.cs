using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RC.ADS.WebAPP.Comm;

namespace RC.ADS.WebAPP.Controllers
{
    public class WeChatController : Controller
    {
        #region 首页
        public IActionResult Index()
        {
            WmsLog.Info(this, "test");
            return View();
        }
        #endregion

        #region 业务范围
        public IActionResult Business()
        {
            
            return View();
        }
        #region 子功能
        public IActionResult BusinessDetail(string businessId)
        {
            return View();
        }
        #endregion

        #endregion

        #region 下单
        public IActionResult PlaceOrder()
        {
            return View();
        }
        #endregion

        #region 个人中心
        public IActionResult Me()
        {
            return View();
        }
        #region 子功能
        //public IActionResult BusinessDetail(string businessId)
        //{
        //    return View();
        //}
        #endregion
        #endregion
    }
}
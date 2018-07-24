using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RC.ADS.WebAPP.Controllers
{
    public class WeChatController : Controller
    {
        #region 首页
        public IActionResult Index()
        {
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
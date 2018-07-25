using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RC.ADS.WebAPP.Comm;

namespace RC.ADS.WebAPP.Controllers
{
    public class WeChatController : Controller
    {
        #region 验证码，登陆，注册
        /// <summary>
        /// 图形验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult ValidateCode()
        {
            string code = "";
            System.IO.MemoryStream ms = VierificationCodeHelper.Create(out  code);
            HttpContext.Session.SetString("LoginValidateCode", code);
            Response.Body.Dispose();
            return File(ms.ToArray(), @"image/png");
        }
        public IActionResult Login()
        {

            return View();
        }
        public IActionResult Register()
        {

            return View();
        }
        #endregion
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
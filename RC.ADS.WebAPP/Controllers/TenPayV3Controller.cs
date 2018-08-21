using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RC.ADS.Data;
using RC.ADS.WebAPP.Comm;
using RC.ADS.WebAPP.Filters;
using RC.ADS.WebAPP.Models;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CoreMvcExtension.BrowserUtility;
using Senparc.Weixin.MP.TenPayLibV3;
using ZXing;
using ZXing.Common;

namespace RC.ADS.WebAPP.Controllers
{
    public class TenPayV3Controller : Controller
    {
        private readonly DataContext _context;
       
        public TenPayV3Controller(DataContext context)
        {
            _context = context;
        }

        #region JsApi支付

        public ActionResult OAuthCallback(string code, string state, string returnUrl)
        {
            RCLog.Info(this, "OAuthCallback");
            if (string.IsNullOrEmpty(code))
            {
                return Content("您拒绝了授权！");
            }

            if (!state.Contains("|"))
            {
                //这里的state其实是会暴露给客户端的，验证能力很弱，这里只是演示一下
                //实际上可以存任何想传递的数据，比如用户ID
                return Content("验证失败！请从正规途径进入！1001");
            }

            //通过，用code换取access_token
            var openIdResult = OAuthApi.GetAccessToken(WeiXinConfig.appId, WeiXinConfig.AppSecret, code);
            if (openIdResult.errcode != ReturnCode.请求成功)
            {
                return Content("错误：" + openIdResult.errmsg);
            }
            RCLog.Info(this, "OAuthCallback="+ openIdResult.openid);
            HttpContext.Session.SetString("OpenId", openIdResult.openid);//进行登录

            //也可以使用FormsAuthentication等其他方法记录登录信息，如：
            //FormsAuthentication.SetAuthCookie(openIdResult.openid,false);
            RCLog.Info(this, " 结束OAuthCallback");
            return Redirect(returnUrl);
        }

        //需要OAuth登录
        [CustomOAuth(null, "/TenpayV3/OAuthCallback")]
        public ActionResult JsApi(string topupItemId="2",string bodytext="微信充值")
        {
            try
            {
                RCLog.Info(this, "进入JsApi");
                var topupItem = _context.TopupItems.FirstOrDefault(z => z.Id == topupItemId);
                if (topupItem == null)
                {
                    return Content("商品信息不存在，或非法进入！1002");
                }
               

                //var openId = User.Identity.Name;
                var openId = HttpContext.Session.GetString("OpenId");

                string sp_billno = DateTime.Now.Ticks.ToString();
                
                var timeStamp = TenPayV3Util.GetTimestamp();
                var nonceStr = TenPayV3Util.GetNoncestr();

                var body = bodytext;
                var price = topupItem.Price;//单位：分
                var xmlDataInfo = new TenPayV3UnifiedorderRequestData(WeiXinConfig.appId, WeiXinConfig.MchId, body, sp_billno, price, HttpContext.UserHostAddress()?.ToString(), WeiXinConfig.TenPayV3Notify, TenPayV3Type.JSAPI, openId, WeiXinConfig.Key, nonceStr);

                var result = TenPayV3.Unifiedorder(xmlDataInfo);//调用统一订单接口
                RCLog.Info(this,"订单号："+result.prepay_id);                                     //JsSdkUiPackage jsPackage = new JsSdkUiPackage(WeiXinConfig.appId, timeStamp, nonceStr,);
                var package = string.Format("prepay_id={0}", result.prepay_id);

                ViewData["topupItem"] = topupItem;
                ViewData["appId"] = WeiXinConfig.appId;
                ViewData["timeStamp"] = timeStamp;
                ViewData["nonceStr"] = nonceStr;
                ViewData["package"] = package;
                ViewData["paySign"] = TenPayV3.GetJsPaySign(WeiXinConfig.appId, timeStamp, nonceStr, package, WeiXinConfig.Key);

                //临时记录订单信息，留给退款申请接口测试使用
                //HttpContext.Session.SetString("BillNo", sp_billno);
                //HttpContext.Session.SetString("BillFee", price.ToString());
                
                return View();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                msg += "<br>" + ex.StackTrace;
                msg += "<br>==Source==<br>" + ex.Source;

                if (ex.InnerException != null)
                {
                    msg += "<br>===InnerException===<br>" + ex.InnerException.Message;
                }
                return Content(msg);
            }
        }
        //需要OAuth登录
        [CustomOAuth(null, "/TenpayV3/OAuthCallback")]

        public ActionResult JsApi2()
        {
            try
            {
                RCLog.Info(this, "进入JsApi2");

                //var openId = User.Identity.Name;
                var openId = HttpContext.Session.GetString("OpenId");

                string sp_billno = DateTime.Now.Ticks.ToString();

                var timeStamp = TenPayV3Util.GetTimestamp();
                var nonceStr = TenPayV3Util.GetNoncestr();

                var body = "网上支付";
                var price = 100;//单位：分
                var xmlDataInfo = new TenPayV3UnifiedorderRequestData(WeiXinConfig.appId, WeiXinConfig.MchId, body, sp_billno, price, HttpContext.UserHostAddress()?.ToString(), WeiXinConfig.TenPayV3Notify, TenPayV3Type.JSAPI, openId, WeiXinConfig.Key, nonceStr);

                var result = TenPayV3.Unifiedorder(xmlDataInfo);//调用统一订单接口
                RCLog.Info(this, "订单号：" + result.prepay_id);                                     //JsSdkUiPackage jsPackage = new JsSdkUiPackage(WeiXinConfig.appId, timeStamp, nonceStr,);
                var package = string.Format("prepay_id={0}", result.prepay_id);


                ViewData["appId"] = WeiXinConfig.appId;
                ViewData["timeStamp"] = timeStamp;
                ViewData["nonceStr"] = nonceStr;
                ViewData["package"] = package;
                ViewData["paySign"] = TenPayV3.GetJsPaySign(WeiXinConfig.appId, timeStamp, nonceStr, package, WeiXinConfig.Key);

                //临时记录订单信息，留给退款申请接口测试使用
                HttpContext.Session.SetString("BillNo", sp_billno);
                HttpContext.Session.SetString("BillFee", price.ToString());
                RCLog.Info(this, "成果返回Json");
                return Json(new { WeiXinConfig.appId, timeStamp, nonceStr, package , paySign = TenPayV3.GetJsPaySign(WeiXinConfig.appId, timeStamp, nonceStr, package, WeiXinConfig.Key)});
                }
            catch (Exception ex)
            {
                var msg = ex.Message;
                msg += "<br>" + ex.StackTrace;
                msg += "<br>==Source==<br>" + ex.Source;

                if (ex.InnerException != null)
                {
                    msg += "<br>===InnerException===<br>" + ex.InnerException.Message;
                }
                return Content(msg);
            }
        }

        /// <summary>
        /// JS-SDK支付回调地址（在统一下单接口中设置notify_url）
        /// </summary>
        /// <returns></returns>
        public ActionResult PayNotifyUrl()
        {
            try
            {
                RCLog.Info(this,"支付返回");
                ResponseHandler resHandler = new ResponseHandler(HttpContext);

                string return_code = resHandler.GetParameter("return_code");
                string return_msg = resHandler.GetParameter("return_msg");

                string res = null;

                resHandler.SetKey(WeiXinConfig.Key);
                //验证请求是否从微信发过来（安全）
                if (resHandler.IsTenpaySign() && return_code.ToUpper() == "SUCCESS")
                {
                    res = "success";//正确的订单处理
                                    //直到这里，才能认为交易真正成功了，可以进行数据库操作，但是别忘了返回规定格式的消息！
                    RCLog.Info(this, "支付成功");

                }
                else
                {
                    res = "wrong";//错误的订单处理
                }

                /* 这里可以进行订单处理的逻辑 */

                //发送支付成功的模板消息
              

                 


                string xml = string.Format(@"<xml>
<return_code><![CDATA[{0}]]></return_code>
<return_msg><![CDATA[{1}]]></return_msg>
</xml>", return_code, return_msg);
                return Content(xml, "text/xml");
            }
            catch (Exception ex)
            {
                RCLog.Error(this,"发生错误");
                throw;
            }
        }

        #endregion
 

        #region 产品展示

        public ActionResult ProductList()
        {
            var products = ProductModel.GetFakeProductList();
            return View(products);
        }


        public ActionResult ProductItem(int productId, int hc)
        {
            var products = ProductModel.GetFakeProductList();
            var product = products.FirstOrDefault(z => z.Id == productId);
            if (product == null || product.GetHashCode() != hc)
            {
                return Content("商品信息不存在，或非法进入！2003");
            }

            //判断是否正在微信端
            if (BrowserUtility.SideInWeixinBrowser(HttpContext))
            {
                //正在微信端，直接跳转到微信支付页面
                return RedirectToAction("JsApi", new { productId = productId, hc = hc });
            }
            else
            {
                //在PC端打开，提供二维码扫描进行支付
                return View(product);
            }
        }

    

        #endregion

         
    }
}
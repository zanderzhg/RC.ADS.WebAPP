using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RC.ADS.WebAPP.Comm;
using RC.ADS.WebAPP.Models;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.TenPayLibV3;

namespace RC.ADS.WebAPP.Controllers
{
    public class TenPayV3Controller : Controller
    {
        #region H5支付

        /// <summary>
        /// H5支付
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="hc"></param>
        /// <returns></returns>
        public ActionResult H5Pay(int productId, int hc)
        {
            {
                try
                {
                    //获取产品信息
                    var products = ProductModel.GetFakeProductList();
                    var product = products.FirstOrDefault(z => z.Id == productId);
                    if (product == null || product.GetHashCode() != hc)
                    {
                        return Content("商品信息不存在，或非法进入！1002");
                    }

                    string openId = null;//此时在外部浏览器，无法或得到OpenId

                    string sp_billno = Request.Query["order_no"];
                    if (string.IsNullOrEmpty(sp_billno))
                    {
                        //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
                        sp_billno = string.Format("{0}{1}{2}", WeiXinConfig.MchId/*10位*/, DateTime.Now.ToString("yyyyMMddHHmmss"),
                            TenPayV3Util.BuildRandomStr(6));
                    }
                    else
                    {
                        sp_billno = Request.Query["order_no"];
                    }

                    var timeStamp = TenPayV3Util.GetTimestamp();
                    var nonceStr = TenPayV3Util.GetNoncestr();

                    var body = product == null ? "test" : product.Name;
                    var price = product == null ? 100 : (int)product.Price * 100;
                    //var ip = Request.Params["REMOTE_ADDR"];
                    var xmlDataInfo = new TenPayV3UnifiedorderRequestData(WeiXinConfig.appId, WeiXinConfig.MchId, body, sp_billno, price, HttpContext.UserHostAddress()?.ToString(), WeiXinConfig.TenPayV3Notify, TenPayV3Type.MWEB/*此处无论传什么，方法内部都会强制变为MWEB*/, openId, WeiXinConfig.Key, nonceStr);

                    var result = TenPayV3.Html5Order(xmlDataInfo);//调用统一订单接口
                                                                  //JsSdkUiPackage jsPackage = new JsSdkUiPackage(TenPayV3Info.AppId, timeStamp, nonceStr,);

                    /*
                     * result:{"device_info":"","trade_type":"MWEB","prepay_id":"wx20170810143223420ae5b0dd0537136306","code_url":"","mweb_url":"https://wx.tenpay.com/cgi-bin/mmpayweb-bin/checkmweb?prepay_id=wx20170810143223420ae5b0dd0537136306\u0026package=1505175207","appid":"wx669ef95216eef885","mch_id":"1241385402","sub_appid":"","sub_mch_id":"","nonce_str":"juTchIZyhXvZ2Rfy","sign":"5A37D55A897C854F64CCCC4C94CDAFE3","result_code":"SUCCESS","err_code":"","err_code_des":"","return_code":"SUCCESS","return_msg":null}
                     */
                    //return Json(result, JsonRequestBehavior.AllowGet);

                    var package = string.Format("prepay_id={0}", result.prepay_id);

                    ViewData["product"] = product;

                    ViewData["appId"] = WeiXinConfig.appId;
                    ViewData["timeStamp"] = timeStamp;
                    ViewData["nonceStr"] = nonceStr;
                    ViewData["package"] = package;
                    ViewData["paySign"] = TenPayV3.GetJsPaySign(WeiXinConfig.appId, timeStamp, nonceStr, package, WeiXinConfig.Key);

                    //设置成功页面（也可以不设置，支付成功后默认返回来源地址）
                    var returnUrl =
                        string.Format("https://sdk.weixin.senparc.com/TenpayV3/H5PaySuccess?productId={0}&hc={1}",
                            productId, hc);

                    var mwebUrl = result.mweb_url;
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        mwebUrl += string.Format("&redirect_url={0}", returnUrl.AsUrlData());
                    }

                    ViewData["MWebUrl"] = mwebUrl;

                    //临时记录订单信息，留给退款申请接口测试使用
                    HttpContext.Session.SetString("BillNo", sp_billno);
                    HttpContext.Session.SetString("BillFee", price.ToString());

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
        }

        public ActionResult H5PaySuccess(int productId, int hc)
        {
            try
            {
                //TODO：这里可以校验支付是否真的已经成功

                //获取产品信息
                var products = ProductModel.GetFakeProductList();
                var product = products.FirstOrDefault(z => z.Id == productId);
                if (product == null || product.GetHashCode() != hc)
                {
                    return Content("商品信息不存在，或非法进入！1002");
                }
                ViewData["product"] = product;

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

        #endregion
    }
}
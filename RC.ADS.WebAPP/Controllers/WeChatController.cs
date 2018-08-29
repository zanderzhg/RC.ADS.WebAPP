using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qcloudsms_csharp;
using RC.ADS.Data;
using RC.ADS.Data.Entity.AD_Account;
using RC.ADS.Data.Entity.AD_Menber;
using RC.ADS.Data.Enum;
using RC.ADS.WebAPP.Comm;
using RC.ADS.WebAPP.Filters;
using RC.ADS.WebAPP.Models.WeChat;
using Senparc.Weixin;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.TenPayLibV3;

namespace RC.ADS.WebAPP.Controllers
{
    public class WeChatController : Controller
    {
        private readonly DataContext _context;
        private readonly SMSHelper _ssender;

        public WeChatController(DataContext context, SMSHelper ssender)
        {
            _context = context;
            _ssender = ssender;
        }
        public IActionResult Text()
        {
            return View();
        }

        #region 文章展示 完成
        public async Task<IActionResult> ShowArticle(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }
        #endregion

        #region 首页 完成
        public IActionResult Index()
        {
            IndexVM vm = new IndexVM();
            vm.About = _context.Articles.FirstOrDefault(x => x.ArticleType == ArticleTypeEnum.AboutUS_1);
            vm.Notice = _context.Articles.FirstOrDefault(x => x.ArticleType == ArticleTypeEnum.Notice_4);
            vm.Slideshows = _context.Articles.Where(x => x.ArticleType == ArticleTypeEnum.Slideshows_7).ToList();
            return View(vm);
        }
        #region 子功能

        #region 客服
        public IActionResult CustomerService()
        {
            RCLog.Info(this, "test");
            var vm = _context.Articles.FirstOrDefault(x => x.ArticleType  == ArticleTypeEnum.CustomerService_6);
            return View(nameof(ShowArticle), vm);
        }
        #endregion
        #region 公告列表
        public IActionResult NoticeList()
        {
            RCLog.Info(this, "test");
            var vm = _context.Articles.Where(x => x.ArticleType == ArticleTypeEnum.Notice_4);

            return View(vm);
        }
        #endregion
        #region 优惠列表
        public IActionResult SpecialOffersList()
        {
            RCLog.Info(this, "test");
            var vm = _context.Articles.Where(x => x.ArticleType == ArticleTypeEnum.Slideshows_7);
            return View(vm);
        }
        #endregion

        #endregion
        #endregion

        #region 业务范围 完成
        public async Task<IActionResult> Business()
        {
            var vm = await _context.Articles.Where(x => x.ArticleType == ArticleTypeEnum.Business_2).ToListAsync();
            return View(vm);
        }
        #region 子功能
        public async Task<IActionResult> BusinessDetail(string businessId)
        {

            var article = await _context.Articles.FirstOrDefaultAsync(m => m.Id == businessId);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }
        #endregion

        #endregion

        #region 下单 完成
        public async Task<IActionResult> PlaceOrder()
        {
            ;
            var vm = await _context.Articles.FirstOrDefaultAsync(m => m.Id == ArticleTypeHelper.ArticleType_PlaceOrderId);

            return View(vm);

        }
        #endregion

        #region 个人中心
        //需要OAuth登录
        [CustomOAuth(null, "/wechat/OAuthCallback")]
        public async Task<IActionResult> Me()
        {
            MeVM vm = new MeVM();
            Menber menber = null;
            ViewBag.headerUrl = HttpContext.Session.GetString("headimgurl");
            try
            {
                var weChatOpenId = HttpContext.Session.GetString("OpenId");
                menber = await _context.Menbers.FirstOrDefaultAsync(x => x.WeChatOpenId == weChatOpenId);
                if (menber == null)
                {
                    menber = new Menber();

                    menber.Username = HttpContext.Session.GetString("nickname");

                    menber.WeChatOpenId = weChatOpenId;
                    _context.Add(menber);
                    _context.SaveChanges();
                    ViewBag.headerUrl = HttpContext.Session.GetString("headimgurl");
                    return RedirectToAction("ModifPhoneNumber", "wechat");
                }
                vm.Balance = (decimal)(menber.AccountSum * 0.01);//转化为元
                vm.IntegralSum = menber.IntegralSum;
                vm.Username = menber.Username;
                vm.PhoneNo = string.IsNullOrEmpty(menber.PhoneNumber) ? "无" : menber.PhoneNumber;
                vm.OrderSum = await _context.Orders.Where(x => x.OwnerId == menber.Id).CountAsync();

            }
            catch (Exception ex)
            {
                RCLog.Error(this, ex.ToString());
            }
            return View(vm);
        }
        #region 子功能
        #region 余额 完成
        public IActionResult AccountInfoList(int pageIndex = 1)
        {
            int pageSize = 30;
            var openId = HttpContext.Session.GetString("OpenId");
            var owner = _context.Menbers.FirstOrDefault(x => x.WeChatOpenId == openId);
            var tempData = _context.AccountInfos.Where(x => x.OwnerId == owner.Id).OrderByDescending(x => x.CreateTime);

            AccountInfoDto vm = new AccountInfoDto();
            vm.AccountInfos = tempData.Skip(pageIndex - 1).Take(pageSize).ToList();
            vm.PageCount = (tempData.Count() % pageSize > 0) ? ((tempData.Count() / pageSize) + 1) : (tempData.Count() / pageSize);
            vm.PageIndex = pageIndex;


            return View(vm);


        }

        #endregion
        #region 积分 完成
        public IActionResult IntegralInfoList(int pageIndex = 1)
        {
            int pageSize = 30;
            var openId = HttpContext.Session.GetString("OpenId");
            var owner = _context.Menbers.FirstOrDefault(x => x.WeChatOpenId == openId);
            var tempData = _context.IntegralInfos.Where(x => x.OwnerId == owner.Id).OrderByDescending(x => x.CreateTime);

            IntegralInfoDto vm = new IntegralInfoDto();
            vm.IntegralInfos = tempData.Skip(pageIndex - 1).Take(pageSize).ToList();
            vm.PageCount = (tempData.Count() % pageSize > 0) ? ((tempData.Count() / pageSize) + 1) : (tempData.Count() / pageSize);
            vm.PageIndex = pageIndex;
            return View(vm);
        }

        #endregion
        #region 订单 完成
        public IActionResult OrderInfoList(int pageIndex = 1)
        {
          
            int pageSize = 30;
            var openId = HttpContext.Session.GetString("OpenId");
            var owner = _context.Menbers.FirstOrDefault(x => x.WeChatOpenId == openId);
            var tempData = _context.Orders.Where(x => x.OwnerId == owner.Id).OrderByDescending(x => x.CreateTime);

            OrderInfoDto vm = new OrderInfoDto();
            vm.Orders = tempData.Skip(pageIndex - 1).Take(pageSize).ToList();
            vm.PageCount = (tempData.Count() % pageSize > 0) ? ((tempData.Count() / pageSize) + 1) : (tempData.Count() / pageSize);
            vm.PageIndex = pageIndex;
            return View(vm);

        }

        #endregion

        #region 充值 完成
        /// <summary>
        /// 充值选择 
        /// </summary>
        /// <returns></returns>
        [CustomOAuth(null, "/wechat/OAuthCallback")]
        public IActionResult RechargeChoice()
        {

            return View(_context.TopupItems.Where(x => x.IsDalete == false));
        }

        #endregion

        #region 我的推广码 完成
        public IActionResult PromoCode()
        {

            try
            {
                var openId = HttpContext.Session.GetString("OpenId");
                var result = Senparc.Weixin.MP.AdvancedAPIs.QrCodeApi.Create(WeiXinConfig.appId, 2592000, 1222, Senparc.Weixin.MP.QrCode_ActionName.QR_STR_SCENE, openId);

                ViewBag.urlstr = result.url; 
            }
            catch (Exception ex)
            {
                RCLog.Error(this, ex.ToString());
            }
            return View();
        }

        #endregion
        #region 我推广的用户 完成
        public async Task<IActionResult> SuggestedUsers()
        {
            var openId = HttpContext.Session.GetString("OpenId");
            var menber = _context.Menbers.FirstOrDefault(x => x.WeChatOpenId == openId);
            return View(await _context.Menbers.Where(x => x.ReferrerId == menber.Id).ToListAsync());

        }

        #endregion

        #region 验证码，修改手机
        /// <summary>
        /// 图形验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult ValidateCode()
        {
            string code = "";
            System.IO.MemoryStream ms = VierificationCodeHelper.Create(out code);
            HttpContext.Session.SetString("ImageValidateCode", code);
            Response.Body.Dispose();
            return File(ms.ToArray(), @"image/png");
        }
        public IActionResult SendPhoneValidateCode()
        {
            var PhoneNumber = Request.Form["PhoneNumber"].ToString().Trim();
            var openId = HttpContext.Session.GetString("OpenId");
            if (!string.IsNullOrEmpty(openId))
            {
                Random rd = new Random();
                var PhoneValidateCode = rd.Next(1000, 9999).ToString();
                var result = _ssender.SendVerificationCode(PhoneValidateCode, PhoneNumber);
                if (result)
                {
                    HttpContext.Session.SetString(PhoneNumber, PhoneValidateCode);
                    return Json(new { statu = "OK", Msg = "验证码已经发送!" });
                }
            }
            return Json(new { statu = "Error", Msg = "验证码发送失败!" });

        }
        #region 修改手机


        [HttpGet]
        public IActionResult ModifPhoneNumber()
        {
            ModifPhoneNumberVM model = new ModifPhoneNumberVM();
            ViewBag.headerUrl = HttpContext.Session.GetString("headimgurl");

            return View(model);
        }
        [HttpPost]
        public IActionResult ModifPhoneNumber(ModifPhoneNumberVM model)
        {
            if (ModelState.IsValid)
            {
                var PhoneValidateCode = HttpContext.Session.GetString(model.PhoneNumber);
                var openId = HttpContext.Session.GetString("OpenId");
                var member = _context.Menbers.FirstOrDefault(x => x.WeChatOpenId == openId);

                if (model.PhoneValidateCode == PhoneValidateCode)
                {
                    member.PhoneNumber = model.PhoneNumber;
                    RCLog.Info(this, $"新手机号码为{ model.PhoneNumber}");
                    _context.Update(member);
                    _context.SaveChanges();
                    return RedirectToAction("Me", "WeChat");
                }

            }
            ViewBag.headerUrl = HttpContext.Session.GetString("headimgurl");

            return View(model);

        }
        #endregion

        #endregion
        #region 安全退出 完成
        public IActionResult LoginOut()
        {
            HttpContext.Session.Remove("OpenId");
            return RedirectToAction("index", "WeChat");
        }
        #endregion


        #endregion
        #endregion

        #region JsApi支付

        public ActionResult OAuthCallback(string code, string state, string returnUrl)
        {
            //RCLog.Info(this, "OAuthCallback");
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
            RCLog.Info(this, "OAuthCallback=" + openIdResult.openid);
            HttpContext.Session.SetString("OpenId", openIdResult.openid);//进行登录
            OAuthUserInfo userInfo = OAuthApi.GetUserInfo(openIdResult.access_token, openIdResult.openid);
            HttpContext.Session.SetString("nickname", userInfo.nickname);
            HttpContext.Session.SetString("headimgurl", userInfo.headimgurl);


            //也可以使用FormsAuthentication等其他方法记录登录信息，如：
            //FormsAuthentication.SetAuthCookie(openIdResult.openid,false);
            //RCLog.Info(this, " 结束OAuthCallback");
            return Redirect(returnUrl);
        }


        public ActionResult JsApi(string topupItemId = "2", string bodytext = "微信充值")
        {
            try
            {
                //RCLog.Info(this, "进入JsApi");
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
                RCLog.Info(this, "订单号：" + result.prepay_id);                                     //JsSdkUiPackage jsPackage = new JsSdkUiPackage(WeiXinConfig.appId, timeStamp, nonceStr,);
                var package = string.Format("prepay_id={0}", result.prepay_id);
                RCLog.Info(this, $"sp_billno={sp_billno}");
                RCLog.Info(this, $"price={price}");

                //临时记录订单信息，留给退款申请接口测试使用
                HttpContext.Session.SetString("BillNo", sp_billno);
                HttpContext.Session.SetString("BillFee", price.ToString());

                return Json(new
                {
                    appId = WeiXinConfig.appId,
                    timeStamp = timeStamp,
                    nonceStr = nonceStr,
                    package = package,
                    paySign = TenPayV3.GetJsPaySign(WeiXinConfig.appId, timeStamp, nonceStr, package, WeiXinConfig.Key)
                });
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
                RCLog.Info(this, "支付返回");
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
                    string total_fee = resHandler.GetParameter("total_fee");
                    string out_trade_no = resHandler.GetParameter("out_trade_no");
                    string openid = resHandler.GetParameter("openid");
                    string package = resHandler.GetParameter("attach");



                    Menber owner = _context.Menbers.FirstOrDefault(x => x.WeChatOpenId == openid);
                    AccountInfo accountinfo = new AccountInfo();
                    accountinfo.BeforeMoney = owner.AccountSum;
                    accountinfo.AfterMoney = owner.AccountSum + int.Parse(total_fee);
                    accountinfo.OwnerId = owner.Id;
                    accountinfo.Money = int.Parse(total_fee);
                    accountinfo.CreateTime = DateTime.Now;
                    accountinfo.TradeNo = out_trade_no;
                    accountinfo.TradeName = package;

                    owner.AccountSum = accountinfo.AfterMoney;
                    _context.Add(accountinfo);
                    _context.SaveChanges();
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
                RCLog.Info(this, xml);
                return Content(xml, "text/xml");
            }
            catch (Exception ex)
            {
                RCLog.Error(this, $"发生错误{ex.ToString()}");
                throw;
            }
        }

        #endregion
    }
}
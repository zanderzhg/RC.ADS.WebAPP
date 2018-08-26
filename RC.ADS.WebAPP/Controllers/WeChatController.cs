using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qcloudsms_csharp;
using RC.ADS.Data;
using RC.ADS.Data.Entity.AD_Menber;
using RC.ADS.WebAPP.Comm;
using RC.ADS.WebAPP.Filters;
using RC.ADS.WebAPP.Models.WeChat;

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
        #region 验证码，登陆，注册
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
            var Username = Request.Form["Username"].ToString().Trim();
            var ImageValidateCode = Request.Form["ImageValidateCode"].ToString().Trim();

            var ImageValidateCodeCash = HttpContext.Session.GetString("ImageValidateCode");
            if (ImageValidateCode.ToUpper() == ImageValidateCodeCash.ToUpper())
            {
                Random rd = new Random();
                var PhoneValidateCode = rd.Next(1000, 9999).ToString();
                var result = _ssender.SendVerificationCode(PhoneValidateCode, Username);
                if (result)
                {
                    HttpContext.Session.SetString(Username, PhoneValidateCode);
                    return Json(new { statu = "OK", Msg = "验证码已经发送!" });
                }
                else
                {
                    return Json(new { statu = "Error", Msg = "验证码发送失败!" });
                }
            }
            else
            {
                return Json(new { statu = "Error", Msg = "图片验证码不对!" });
            }
        }
        #region 登陆


        [HttpGet]
        public IActionResult Login(string referrerId = "")
        {


            var model = new LoginVM { ReferrerId = referrerId };
            return View(model);
        }
        [HttpPost]
        public IActionResult Login(LoginVM model)
        {

            if (ModelState.IsValid)
            {
                if (model.PhoneValidateCode != HttpContext.Session.GetString(model.Username))
                {
                    ModelState.AddModelError("", "登陆码不对");
                    return View(model);
                }
                var result = _context.Menbers.FirstOrDefault(x => x.Username == model.Username);
                if (result != null)
                {
                    result.LastLoginGuidCode = Guid.NewGuid().ToString("N");
                }
                else
                {
                    result = new Menber
                    {
                        Username = model.Username,
                        PhoneNumber = model.Username,
                        ReferrerId = model.ReferrerId,
                        LastLoginGuidCode = Guid.NewGuid().ToString("N"),
                        RegisterTime = DateTime.Now
                    };
                    _context.Menbers.Add(result);

                }
                _context.SaveChanges();
                HttpContext.Session.SetString("LoginMenberId", result.Id);
                HttpContext.Response.Cookies.Append("LastLoginGuidCode", result.LastLoginGuidCode, new CookieOptions
                {
                    Expires = DateTime.Now.AddMonths(1)
                });
                HttpContext.Response.Cookies.Append("Username", result.Username, new CookieOptions
                {
                    Expires = DateTime.Now.AddMonths(1)
                });
                return RedirectToAction("Me", "WeChat");

            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);

        }
        #endregion

        #endregion
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
            RCLog.Info(this, "test");
            IndexVM vm = new IndexVM();
            vm.About = _context.Articles.FirstOrDefault(x => x.ArticleTypeId == ArticleTypeHelper.ArticleType_AboutUsId);
            vm.Notice = _context.Articles.FirstOrDefault(x => x.ArticleTypeId == ArticleTypeHelper.ArticleType_NoticeId);
            vm.Slideshows = _context.Articles.Where(x => x.ArticleTypeId == ArticleTypeHelper.ArticleType_SlideshowsId).ToList();
            return View(vm);
        }
        #region 子功能

        #region 客服
        public IActionResult CustomerService()
        {
            RCLog.Info(this, "test");
            var vm = _context.Articles.FirstOrDefault(x => x.ArticleTypeId == ArticleTypeHelper.ArticleType_AboutUsId);
            return View(nameof(ShowArticle), vm);
        }
        #endregion
        #region 公告列表
        public IActionResult NoticeList()
        {
            RCLog.Info(this, "test");
            var vm = _context.Articles.Where(x => x.ArticleTypeId == ArticleTypeHelper.ArticleType_NoticeId);

            return View(vm);
        }
        #endregion
        #region 优惠列表
        public IActionResult SpecialOffersList()
        {
            RCLog.Info(this, "test");
            var vm = _context.Articles.Where(x => x.ArticleTypeId == ArticleTypeHelper.ArticleType_SpecialOffersId);
            return View(vm);
        }
        #endregion

        #endregion
        #endregion

        #region 业务范围 完成
        public async Task<IActionResult> Business()
        {
            var vm = await _context.Articles.Where(x => x.ArticleTypeId == ArticleTypeHelper.ArticleType_BusinessId).ToListAsync();
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
        [CustomOAuth(null, "/TenpayV3/OAuthCallback")]
        public async Task<IActionResult> Me()
        {
            MeVM vm = new MeVM();
            Menber menber = null;
            try
            {
                var weChatOpenId = HttpContext.Session.GetString("WeChatOpenId");
                menber = await _context.Menbers.FirstOrDefaultAsync(x => x.WeChatOpenId == weChatOpenId);
                if (menber == null)
                {
                    menber = new Menber();

                    menber.Username = HttpContext.Session.GetString("nickname");
                    menber.WeChatOpenId = HttpContext.Session.GetString("WeChatOpenId");
                    _context.Add(menber);
                    _context.SaveChanges();
                }
                vm.Balance = menber.AccountSum;
                vm.IntegralSum = menber.IntegralSum;
                vm.Username = menber.Username;
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
        public IActionResult AccountInfoList()
        {
            var CurrentMemberId = HttpContext.Session.GetString("LoginMenberId");
            if (string.IsNullOrEmpty(CurrentMemberId))
            {
                return RedirectToAction("Login", "WeChat");
            }
            else
            {

                var vm = from A in _context.AccountInfos
                         join T in _context.AccountInfoChangeTpyes on A.AccountInfoChangeTpyeId equals T.Id
                         where A.OwnerId == CurrentMemberId
                         select new AccountInfoDto
                         {
                             Id = A.Id,
                             AccountInfoChangeTpyeName = T.Name,
                             CreateTime = A.CreateTime,
                             AfterMoney = A.AfterMoney,
                             BeforeMoney = A.BeforeMoney,
                             Money = A.Money,
                             PlusOrMinus = T.PlusOrMinus,
                             Describe = A.Describe
                         };
                return View(vm);
            }

        }

        #endregion
        #region 积分 完成
        public IActionResult IntegralInfoList()
        {
            var CurrentMemberId = HttpContext.Session.GetString("LoginMenberId");
            if (string.IsNullOrEmpty(CurrentMemberId))
            {
                return RedirectToAction("Login", "WeChat");
            }
            else
            {

                var vm = from I in _context.IntegralInfos
                         join T in _context.IntegralInfoChangeType on I.IntegralInfoChangeTypeId equals T.Id
                         where I.OwnerId == CurrentMemberId
                         select new IntegralInfoDto
                         {

                             Id = I.Id,
                             IntegralInfoChangeTypeName = T.Name,
                             CreateTime = I.CreateTime,
                             AfterScore = I.AfterScore,
                             BeforeScore = I.BeforeScore,
                             Score = I.Score,
                             PlusOrMinus = T.PlusOrMinus,
                             Describe = I.Describe
                         };
                return View(vm);
            }
        }

        #endregion
        #region 订单 完成
        public IActionResult OrderInfoList()
        {
            var CurrentMemberId = HttpContext.Session.GetString("LoginMenberId");
            if (string.IsNullOrEmpty(CurrentMemberId))
            {
                return RedirectToAction("Login", "WeChat");
            }
            else
            {
                var vm = from O in _context.Orders
                         join T in _context.OrderStatus on O.OrderStatusId equals T.Id
                         where O.OwnerId == CurrentMemberId
                         select new OrderInfoDto
                         {
                             Id = O.Id,

                             OrderName = O.OrderName,
                             Price = O.Price,
                             OrderStatuName = T.ChineseName,

                             Description = O.Description,
                             CreateTime = O.CreateTime,
                             LastUpdateTime = O.LastUpdateTime
                         };
                return View(vm);
            }
        }

        #endregion

        #region 充值 TODO
        /// <summary>
        /// 充值选择 
        /// </summary>
        /// <returns></returns>
        public IActionResult RechargeChoice()
        {

            return View(_context.TopupItems.Where(x => x.IsDalete == false));
        }
        /// <summary>
        /// 充值
        /// </summary>
        /// <returns></returns>
        public IActionResult Recharge()
        {
            var CurrentMemberId = HttpContext.Session.GetString("LoginMenberId");
            if (string.IsNullOrEmpty(CurrentMemberId))
            {
                return RedirectToAction("Login", "WeChat");
            }
            else
            {
                //TODO 调用支付接口
                return View();
            }
        }
        #endregion

        #region 我的推广码 完成
        public IActionResult PromoCode()
        {

            try
            {
                var openId = HttpContext.Session.GetString("OpenId");
                var result = Senparc.Weixin.MP.AdvancedAPIs.QrCodeApi.Create(WeiXinConfig.appId, 2592000,1222, Senparc.Weixin.MP.QrCode_ActionName.QR_STR_SCENE, openId);


                // var CurrentMemberId = HttpContext.Session.GetString("LoginMenberId");
                ViewBag.urlstr = result.url;// $"http://www.circle-rect.com/wechat/login/?ReferrerId={CurrentMemberId}";
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
            var CurrentMemberId = HttpContext.Session.GetString("LoginMenberId");
            if (string.IsNullOrEmpty(CurrentMemberId))
            {
                return RedirectToAction("Login", "WeChat");
            }
            else
            {
                return View(await _context.Menbers.Where(x => x.ReferrerId == CurrentMemberId).ToListAsync());
            }
        }

        #endregion
        #region 修改密码 完成
        [HttpGet]
        public IActionResult ModifPassword()
        {
            ModifPasswordDto dto = new ModifPasswordDto();
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> ModifPassword(ModifPasswordDto dto)
        {
            var CurrentMemberId = HttpContext.Session.GetString("LoginMenberId");
            if (string.IsNullOrEmpty(CurrentMemberId))
            {
                return RedirectToAction("Login", "WeChat");
            }
            else
            {
                var member = await _context.Menbers.FirstOrDefaultAsync(x => x.Id == CurrentMemberId);
                if (member.Password == dto.OldPassword)
                {
                    if (dto.NewPassword == dto.ConfirmPassword)
                    {
                        member.Password = dto.NewPassword;
                        _context.Update(member);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Me", "WeChat");
                    }
                    else
                    {
                        dto.Msg = "新密码与确认新密码不一致！";
                    }
                }
                else
                {
                    dto.Msg = "旧密码不对！";
                }
            }
            return View(dto);
        }
        #endregion
        #region 安全退出 完成
        public IActionResult LoginOut()
        {
            HttpContext.Session.Remove("LoginMenberId");
            HttpContext.Response.Cookies.Delete("LastLoginGuidCode");
            HttpContext.Response.Cookies.Delete("Username");

            return RedirectToAction("index", "WeChat");
        }
        #endregion


        #endregion
        #endregion
    }
}
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
            var phone = Request.Form["mobile"].ToString().Trim();
            var Vcode = Request.Form["Vcode"].ToString().Trim();

            var ImageValidateCode = HttpContext.Session.GetString("ImageValidateCode");
            if (Vcode.ToUpper() == ImageValidateCode.ToUpper())
            {
                Random rd = new Random();
                var PhoneValidateCode = rd.Next(1000, 9999).ToString();
                var result = _ssender.SendVerificationCode(PhoneValidateCode, phone);
                if (result)
                {
                    HttpContext.Session.SetString("PhoneValidateCode", PhoneValidateCode);
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
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginVM { ReturnUrl = returnUrl };
            return View(model);
        }
        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result = _context.Menbers.FirstOrDefault(x => x.ManberName == model.Username && x.Password == model.Password);

                if (result != null)
                {
                    HttpContext.Session.SetString("LoginMenberId", result.Id);
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Me", "WeChat");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }
        #endregion
        #region 注册
        [HttpGet]
        public IActionResult Register(string referrerId = "")
        {
            RegisterVM vm = new RegisterVM { ReferrerId = referrerId };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                ///TODO 验证数据
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("", "两次输入密码不对");
                    return View(model);
                }
                if (model.ImageValidateCode.ToUpper() != HttpContext.Session.GetString("ImageValidateCode").ToUpper())
                {
                    ModelState.AddModelError("", "验证码不对");
                    return View(model);
                }
                if (model.PhoneValidateCode != HttpContext.Session.GetString("PhoneValidateCode"))
                {
                    ModelState.AddModelError("", "注册码不对");
                    return View(model);
                }

                var result = _context.Menbers.FirstOrDefault(x => x.ManberName == model.Username);
                if (result != null)
                {
                    ModelState.AddModelError("", "该号码已经被注册了");
                    return View(model);
                }
                else
                {
                    Menber entity = new Menber
                    {
                        ManberName = model.Username,
                        PhoneNumber = model.Username,
                        Referrer = _context.Menbers.FirstOrDefault(x => x.Id == model.ReferrerId),
                        Password = model.Password
                    };
                    _context.Menbers.Add(entity);
                    _context.SaveChanges();
                    HttpContext.Session.SetString("LoginMenberId", entity.Id);
                    return RedirectToAction("Me", "WeChat");
                }
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
        {;
            var vm = await _context.Articles.FirstOrDefaultAsync(m => m.Id == ArticleTypeHelper.ArticleType_PlaceOrderId);

            return View("ShowArticl",vm);

        }
        #endregion

        #region 个人中心
        public async Task<IActionResult> Me()
        {
            var CurrentMemberId = HttpContext.Session.GetString("LoginMenberId");
            if (string.IsNullOrEmpty(CurrentMemberId))
            {
                return RedirectToAction("Login", "WeChat");
            }
            else
            {
                MeVM vm = new MeVM();
                var member = await _context.Menbers.FirstOrDefaultAsync(x => x.Id == CurrentMemberId);
                vm.Balance = member.AccountSum;
                vm.IntegralSum = member.IntegralSum;
                vm.ManberName = member.ManberName;
                vm.OrderSum = await _context.Orders.Where(x => x.OwnerId == member.Id).CountAsync();
                return View(vm);
            }
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
            return View();
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
            return View();
        }
        public IActionResult ShowCode()
        {
            System.IO.MemoryStream ms = BarCodeHelper.CreateCodeEwm("www.baidu.com");
            //HttpContext.Session.SetString("ImageValidateCode", code);
            Response.Body.Dispose();
            return File(ms.ToArray(), @"image/png");
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
            return RedirectToAction("index", "WeChat");
        }
        #endregion


        #endregion
        #endregion
    }
}
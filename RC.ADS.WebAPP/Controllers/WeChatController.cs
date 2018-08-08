using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RC.ADS.Data;
using RC.ADS.Data.Entity.AD_Menber;
using RC.ADS.WebAPP.Comm;
using RC.ADS.WebAPP.Models.WeChat;

namespace RC.ADS.WebAPP.Controllers
{
    public class WeChatController : Controller
    {
        private readonly DataContext _context;

        public WeChatController(DataContext context)
        {
            _context = context;
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

            var ImageValidateCode= HttpContext.Session.GetString("ImageValidateCode");
            if (Vcode.ToUpper()== ImageValidateCode.ToUpper())
            {
                Random rd = new Random();
                var PhoneValidateCode = rd.Next(1000, 9999).ToString();
                var result = SMSHelper.SendVerificationCode(PhoneValidateCode, phone);
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

        #region 首页
        public IActionResult Index()
        {
            RCLog.Info(this, "test");
            IndexVM vm = new IndexVM();
            vm.About = _context.Articles.FirstOrDefault(x => x.ArticleTypeId == "45395e0707cb47bca2f537085910bcbd");
            vm.Achievement = _context.Articles.FirstOrDefault(x => x.ArticleTypeId == "4d5f55f74fd84c4e932f5f14e402974d");
            vm.Notice = _context.Articles.FirstOrDefault(x => x.ArticleTypeId == "a875b58bf4c441a1a254037e161a72bb");
            vm.Slideshows = _context.Articles.Where(x => x.ArticleTypeId == "c3c01a5114be496d822fad2fd1bdfb26").ToList();
            return View(vm);
        }
        #endregion

        #region 业务范围
        public async Task<IActionResult> Business()
        {
            //TODO 设置id
            string articleTypeId = "";
            return View(await _context.Articles.Where(x => x.ArticleTypeId == articleTypeId).ToListAsync());
        }
        #region 子功能
        public async Task<IActionResult> BusinessDetail(string businessId)
        {
            if (businessId == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.Id == businessId);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }
        #endregion

        #endregion

        #region 下单
        public async Task<IActionResult> PlaceOrder()
        {
            //TODO
            string articleId = "";
            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.Id == articleId);
            return View(article);

        }
        #endregion

        #region 个人中心
        public IActionResult Me()
        {
            var CurrentMemberId = HttpContext.Session.GetString("LoginMenberId");
            if (string.IsNullOrEmpty(CurrentMemberId))
            {
                return RedirectToAction("Login", "WeChat");
            }
            else
            {
                MeVM vm = new MeVM();
                return View(vm);
            }
        }
        #region 子功能
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
        #endregion
    }
}
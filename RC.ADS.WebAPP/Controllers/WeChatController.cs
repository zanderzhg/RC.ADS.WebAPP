using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Loginaaa(string returnUrl = "")
        {
            
            return View( );
        }
        #region 登陆
        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginVM { ReturnUrl = returnUrl };
            return View(model);
        }
        [HttpPost]
        public  IActionResult  Login(LoginVM model)
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
                if (model.Password!=model.ConfirmPassword)
                {
                    ModelState.AddModelError("", "两次输入密码不对");
                    return View(model);
                }
                if (model.ImageValidateCode != HttpContext.Session.GetString("ImageValidateCode"))
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
            return View(vm);
        }
        #endregion

        #region 业务范围
        public IActionResult Business()
        {
            BusinessVM vm = new BusinessVM();

            return View(vm);
        }
        #region 子功能
        public IActionResult BusinessDetail(string businessId)
        {
            BusinessDetailVM vm = new BusinessDetailVM();
            return View(vm);
        }
        #endregion

        #endregion

        #region 下单
        public IActionResult PlaceOrder()
        {
            PlaceOrderVM vm = new PlaceOrderVM();
            return View(vm);
        }
        #endregion

        #region 个人中心
        public IActionResult Me()
        {
            MeVM vm = new MeVM();
            return View(vm);
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
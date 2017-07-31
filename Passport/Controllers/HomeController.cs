using System;
using System.Web;
using System.Web.Mvc;
using SSO.Core;

namespace Passport.Controllers
{
    public class HomeController : Controller
    {
        private PassportService Passportservice => new PassportService();
        public ActionResult Index()
        {
            return Content("server running...");
        }

        public ActionResult PassportCenter()
        {
            //没有授权Token非法访问
            if (string.IsNullOrEmpty(Request["token"]))
            {
                return Content("没有授权Token，非法访问");
            }
            if (Session["user"] != null)
            {
                return Redirect(Passportservice.GetReturnUrl("", Request["token"], Request["returnUrl"]));
            }
            return View();
        }

        /// <summary>
        /// 授权登陆验证
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PassportVertify()
        {
            var cookie = Request.Cookies[ConstantHelper.USER_COOKIE_KEY];
            if (cookie == null || string.IsNullOrEmpty(cookie.ToString()))
            {
                return RedirectToAction("Login", new { ReturnUrl = Request["ReturnUrl"], Token = Request["Token"] });
            }
            string userinfo = cookie.ToString();
            var success = Passportservice.AuthernVertify(Request["Token"], Convert.ToDateTime(Request["TimeStamp"]));
            if (!success)
            {
                return RedirectToAction("Login", new { ReturnUrl = Request["ReturnUrl"], Token = Request["Token"] });
            }
            return Redirect(Passportservice.GetReturnUrl(userinfo, Request["Token"], Request["ReturnUrl"]));
        }

        /// <summary>
        /// 统一登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            ViewBag.ReturnUrl = Request["ReturnUrl"];
            ViewBag.Token = Request["Token"];
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var result = Passportservice.Login(username, password);
            if (result.IsSuccess)
            {
                Session["user"] = result.UserInfo;
                Response.Cookies.Add(new HttpCookie(ConstantHelper.USER_COOKIE_KEY) { HttpOnly = true, Value = result.UserInfo.UserId.ToString(), Expires = DateTime.Now.AddHours(2) });
                var redirectUrl = Passportservice.GetReturnUrl(result.UserInfo.UserId.ToString(), Request["Token"], Request["ReturnUrl"]);
                return Redirect(redirectUrl);
            }
            return View();
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            Session["user"] = null;
            Response.Cookies[ConstantHelper.USER_COOKIE_KEY].Expires = DateTime.Now.AddDays(-1);
            return Content("退出成功");
        }
    }
}
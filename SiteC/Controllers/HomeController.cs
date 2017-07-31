using System.Web.Mvc;
using SSO.Core;

namespace SiteC.Controllers
{
    public class HomeController : Controller
    {
        [Auth(Code = AuthCodeEnum.Login)]
        public ActionResult Index()
        {
            return View();
        }
    }
}
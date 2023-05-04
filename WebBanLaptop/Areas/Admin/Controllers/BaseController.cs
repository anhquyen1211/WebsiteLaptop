using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebBanLaptop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        //đăng xuất admin quay về trang chủ
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/Home/Index");
        }
        //chuyển từ trang admin sang trang thông tin cá nhân
        public ActionResult ViewProfile()
        {
            return Redirect("~/Account/Editprofile");
        }
        //chuyển từ trang admin sang trang chủ
        public ActionResult BackToHome()
        {
            return Redirect("~/Home/Index");
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using WebBanLaptop.Common;
using WebBanLaptop.Models;

namespace WebBanLaptop.Controllers
{
    public class AccountController : Controller
    {
        private LQShopDb db = new LQShopDb();
        //View đăng nhập
        public ActionResult Login(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) && Request.UrlReferrer != null && Request.UrlReferrer.ToString().Length > 0)
            {
                return RedirectToAction("Login", new { returnUrl = Request.UrlReferrer.ToString() });
            }
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        //Code xử lý đăng nhập
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModels model, string returnUrl)
        {
            model.Password = Crypto.Hash(model.Password);
            var account = db.Accounts.FirstOrDefault(a => a.Email == model.Email && a.Password == model.Password && a.Status == "1");
            if(account != null)
            {
                LoggedUserData userData = new LoggedUserData
                {
                    UserId = account.Account_id,
                    Name = account.Name,
                    Email = account.Email,
                    RoleCode = account.Role,
                    Avatar = account.Avatar
                };
                Notification.SetNotification1_5s("Đăng nhập thành công", "success");
                FormsAuthentication.SetAuthCookie(JsonConvert.SerializeObject(userData), false);
                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }
            Notification.SetNotification3s("Email hoặc mật khẩu không đúng, hoặc tài khoản bị vô hiệu hóa", "error");
            return View(model);
        }
        //Đăng xuất tài khoản
        public ActionResult Logout(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) && Request.UrlReferrer != null && Request.UrlReferrer.ToString().Length > 0)
            {
                return RedirectToAction("Logout", new { returnUrl = Request.UrlReferrer.ToString() });//tạo url khi đăng xuất, đăng xuất thành công thì quay lại trang trước đó
            }
            FormsAuthentication.SignOut();
            Notification.SetNotification1_5s("Đăng xuất thành công", "success");
            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");
        }
        //View đăng ký
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        //Code xử lý đăng ký
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModels model, Account account)
        {
            var checkemail = db.Accounts.Any(m => m.Email == model.Email);
            if (checkemail)
            {
                Notification.SetNotification1_5s("Email đã tồn tại", "error");
                return View();
            }
            account.Role = Role.ROLE_MEMBER_CODE; //admin quyền là 0: thành viên quyền là 1             
            account.Status = "1";
            //account.Role = 1;
            account.Email = model.Email;
            account.Create_by = model.Email;
            account.Update_by = model.Email;
            account.Name = model.Name;
            account.Phone = model.PhoneNumber;
            account.Update_at = DateTime.Now;
            account.Avatar = "/Content/Images/logo/logo1.png";
            db.Configuration.ValidateOnSaveEnabled = false;
            account.Password = Crypto.Hash(model.Password); //mã hoá password
            account.Create_at = DateTime.Now; //thời gian tạo tạo khoản
            db.Accounts.Add(account);
            Notification.SetNotification1_5s("Đăng ký thành công", "success");           
            db.SaveChanges(); //add dữ liệu vào database          
            return RedirectToAction("Login", "Account");
        }
    }
}
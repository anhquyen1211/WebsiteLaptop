using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using WebBanLaptop.Common;
using WebBanLaptop.Common.Helpers;
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
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Notification.SetNotification1_5s("Đăng xuất thành công", "success");
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
        //View cập nhật thông tin cá nhân
        [Authorize]     // Đăng nhập mới có thể truy cập
        public ActionResult Editprofile()
        {
            var userId = User.Identity.GetUserID();
            var user = db.Accounts.Where(u => u.Account_id == userId).FirstOrDefault();
            if (user != null)
            {
                return View(user);
            }
            return View();
        }
        [HttpPost]
        //Code xử lý cập nhật thông tin cá nhân
        [Authorize]// Đăng nhập mới có thể truy cập
        public ActionResult Editprofile(FormCollection form)
        {
            var userId = User.Identity.GetUserID();
            var account = db.Accounts.Where(m => m.Account_id == userId).FirstOrDefault();
            if (account != null)
            {
                HttpPostedFileBase file = Request.Files["image-file"];
                account.Account_id = userId;
                account.Name = form["userName"];
                account.Phone = form["phoneNumber"];
                account.Update_by = userId.ToString();
                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    account.Avatar = "/Content/Images/" + fileName;
                    file.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/"), fileName));
                    account.Update_at = DateTime.Now;
                }
                db.Configuration.ValidateOnSaveEnabled = false;
                UpdateModel(account);
                Notification.SetNotification1_5s("Cập nhật thông tin tài khoản thành công", "success");
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
        //View thay đổi mật khẩu
        public ActionResult ChangePassword()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        //Code xử lý Thay đổi mật khẩu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModels model)
        {
            if (User.Identity.IsAuthenticated)
            {
                int userID = User.Identity.GetUserID();
                model.NewPassword = Crypto.Hash(model.NewPassword);
                Account account = db.Accounts.FirstOrDefault(m => m.Account_id == userID);
                if (model.NewPassword == account.Password)
                {
                    Notification.SetNotification3s("Mật khẩu mới và cũ không được trùng!", "error");
                    return View(model);
                }
                account.Update_at = DateTime.Now;
                account.Update_by = User.Identity.GetEmail();
                account.Password = model.NewPassword;
                db.Configuration.ValidateOnSaveEnabled = false;
                UpdateModel(account);
                db.SaveChanges();
                Notification.SetNotification3s("Đổi mật khẩu thành công", "success");
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }
        //Quản lý sổ địa chỉ
        public ActionResult Address()
        {
            if (User.Identity.IsAuthenticated)
            {
                int userID = User.Identity.GetUserID();
                var address = db.AccountAddresses.Where(m => m.Account_id == userID).ToList();
                ViewBag.Check_address = db.AccountAddresses.Where(m => m.Account_id == userID).Count();
                ViewBag.ProvincesList = db.Provinces.OrderBy(m => m.Province_name).ToList();
                ViewBag.DistrictsList = db.Districts.OrderBy(m => m.Type).ThenBy(m => m.District_name).ToList();
                ViewBag.WardsList = db.Wards.OrderBy(m => m.Type).ThenBy(m => m.Ward_name).ToList();
                return View(address);
            }
            return RedirectToAction("Index", "Home");
        }
        //Thêm mới địa chỉ 
        public ActionResult AddressCreate(AccountAddress address)
        {
            bool result = false;
            var userid = User.Identity.GetUserID();
            var checkdefault = db.AccountAddresses.Where(a => a.Account_id == userid).ToList();
            var limit_address = db.AccountAddresses.Where(a => a.Account_id == userid).ToList();
            try
            {
                if (limit_address.Count() == 4)//tối đa 4 địa chỉ
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                foreach (var item in checkdefault)
                {
                    if (item.IsDefault == true && address.IsDefault == true)
                    {
                        item.IsDefault = false;
                        db.SaveChanges();
                    }
                }
                address.Account_id = userid;
                db.AccountAddresses.Add(address);
                db.SaveChanges();
                result = true;
                Notification.SetNotification1_5s("Thêm thành công", "success");
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        //Sửa địa chỉ
        [HttpPost]
        public JsonResult AddressEdit(int id, string username, string phonenumber, int province_id, int district_id, int ward_id, string address_content)
        {
            var address = db.AccountAddresses.FirstOrDefault(m => m.Account_address_id == id);
            bool result;
            if (address != null)
            {
                address.Province_id = province_id;
                address.Account_username = username;
                address.Account_phonenumber = phonenumber;
                address.District_id = district_id;
                address.Ward_id = ward_id;
                address.Content = address_content;
                address.Account_id = User.Identity.GetUserID();
                db.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //Thay đổi địa chỉ mặc định
        [HttpPost]
        public JsonResult DefaultAddress(int id)
        {
            bool result = false;
            var userid = User.Identity.GetUserID();
            var address = db.AccountAddresses.FirstOrDefault(m => m.Account_address_id == id);
            var checkdefault = db.AccountAddresses.ToList();
            if (User.Identity.IsAuthenticated && !address.IsDefault == true)
            {
                foreach (var item in checkdefault)
                {
                    if (item.IsDefault == true && item.Account_id == userid)
                    {
                        item.IsDefault = false;
                        db.SaveChanges();
                    }
                }
                address.IsDefault = true;
                db.SaveChanges();
                result = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        //Xóa địa chỉ
        [HttpPost]
        public JsonResult AddressDelete(int id)
        {
            bool result = false;
            try
            {
                var address = db.AccountAddresses.FirstOrDefault(m => m.Account_address_id == id);
                db.AccountAddresses.Remove(address);
                db.SaveChanges();
                result = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }
        //lấy danh sách quận huyện
        public JsonResult GetDistrictsList(int province_id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Districts> districtslist = db.Districts.Where(d => d.Province_id == province_id).OrderBy(d => d.Type).ThenBy(d => d.District_name).ToList();
            return Json(districtslist, JsonRequestBehavior.AllowGet);
        }
        //lấy danh sách phường xã
        public JsonResult GetWardsList(int district_id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Wards> wardslist = db.Wards.Where(w => w.District_id == district_id).OrderBy(w => w.Type).ThenBy(w => w.Ward_name).ToList();
            return Json(wardslist, JsonRequestBehavior.AllowGet);
        }
        //Lịch sử mua hàng
        [Authorize]
        public ActionResult TrackingOrder(int? page)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserID();
                int pageSize = 9;
                int pageNumber = (page ?? 1); //nếu null trả về 1
                var list = db.Orders.Where(m => m.Account_id == userId).OrderByDescending(m => m.Order_date);
                return View(list.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //Chi tiết đơn hàng đã mua
        public ActionResult TrackingOrderDetail(int id)
        {
            List<Order_Detail> order = db.Order_Detail.Where(p => p.Order_id == id).ToList();
            ViewBag.Order = db.Orders.FirstOrDefault(p => p.Order_id == id);
            ViewBag.OrderID = id;
            if (User.Identity.IsAuthenticated)
            {
                return View(order);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //đánh số trang
        private IPagedList GetOrder(int? page)
        {
            var userId = User.Identity.GetUserID();
            int pageSize = 10;
            int pageNumber = (page ?? 1); //đánh số trang
            var list = db.Orders.Where(p => p.Account_id == userId).OrderByDescending(p => p.Order_id)
                .ToPagedList(pageNumber, pageSize);
            return list;
        }
        public ActionResult UserLogged()
        {
            // Được gọi khi nhấn [Thanh toán]
            return Json(User.Identity.IsAuthenticated, JsonRequestBehavior.AllowGet);
        }
    }
}
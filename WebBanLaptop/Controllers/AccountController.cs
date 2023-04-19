using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
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
        //Thêm mới địa chỉ 
        public ActionResult AddressCreate(AccountAddress address)
        {
            bool result = false;
            var userid = User.Identity.GetUserID();
            var checkdefault = db.AccountAddresses.Where(a => a.Account_id == userid).ToList();
            var limit_address = db.AccountAddresses.Where(a => a.Account_id == userid).ToList();
            try
            {
                if (limit_address.Count() == 4)//tối đa 4 ký tự
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
        public ActionResult TrackingOrder(int? page)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View("TrackingOrder", GetOrder(page));
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
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanLaptop.Common.Helpers;
using WebBanLaptop.Models;

namespace WebBanLaptop.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        private LQShopDb db = new LQShopDb();
        // GET: Admin/Auth
        public ActionResult Index(int? page, string search)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            ViewBag.search = search;
            ViewBag.countTrash = db.Accounts.Where(a => a.Status == "0").Count();
            var list = db.Accounts.Where(a => a.Status != "0").OrderBy(a => a.Create_at);
            if (!string.IsNullOrEmpty(search))
            {
                list = db.Accounts.Where(a => a.Email.Contains(search) || a.Account_id.ToString().Contains(search) || a.Name.Contains(search))
                    .OrderBy(a => a.Create_at);
            }
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Trash(int? page, string search)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            ViewBag.search = search;
            var list = db.Accounts.Where(a => a.Status == "0").OrderBy(a => a.Create_at);
            if (!string.IsNullOrEmpty(search))
            {
                list = db.Accounts.Where(a => a.Email.Contains(search) || a.Account_id.ToString().Contains(search) || a.Name.Contains(search))
                    .OrderBy(a => a.Create_at);
            }
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Details(int id)
        {
            Account account = db.Accounts.FirstOrDefault(m => m.Account_id == id);
            ViewBag.ListAddress = db.AccountAddresses.Where(m => m.Account_id == id).ToList();
            if (account == null)
            {
                Notification.SetNotification1_5s("Không tồn tại! (ID = " + id + ")", "warning");
                return RedirectToAction("Index");
            }
            return View(account);
        }
        public JsonResult ChangeRoles(int accountID, int roleID)
        {
            var account = db.Accounts.FirstOrDefault(m => m.Account_id == accountID);
            int role = User.Identity.GetRole();
            bool result = false;
            try
            {
                if (account != null && role == 0)
                {
                    account.Role = roleID;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Entry(account).State = EntityState.Modified;
                    db.SaveChanges();
                    result = true;
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Disable(int id)
        {
            string result = "error";
            Account account = db.Accounts.FirstOrDefault(m => m.Account_id == id);
            try
            {
                if (User.Identity.GetUserID() != id && User.Identity.GetRole() == 0)
                {
                    result = "success";
                    account.Status = "0";
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Entry(account).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult IsActive(int id)
        {
            string result = "error";
            Account account = db.Accounts.FirstOrDefault(m => m.Account_id == id);
            try
            {
                if (User.Identity.GetUserID() != id && User.Identity.GetRole() == 0)
                {
                    result = "success";
                    account.Status = "1";
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Entry(account).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
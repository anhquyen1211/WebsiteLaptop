using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanLaptop.Common.Helpers;
using WebBanLaptop.Models;

namespace WebBanLaptop.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        private LQShopDb db = new LQShopDb();
        // GET: Admin/Brand
        public ActionResult Index(int? page, string search)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            ViewBag.search = search;
            var list = db.Brands.OrderBy(a => a.Create_at);
            if (!string.IsNullOrEmpty(search))
            {
                list = db.Brands.Where(a => a.Brand_name.Contains(search)).OrderBy(a => a.Create_at);
            }
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Create(string brandName, Brand brand)
        {
            string result = "false";
            try
            {
                Brand checkExist = db.Brands.SingleOrDefault(m => m.Brand_name == brandName);
                if (checkExist != null)
                {
                    result = "exist";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                brand.Brand_name = brandName;
                brand.Create_by = User.Identity.GetEmail();
                brand.Update_by = User.Identity.GetEmail();
                brand.Create_at = DateTime.Now;
                brand.Update_at = DateTime.Now;
                db.Brands.Add(brand);
                db.SaveChanges();
                result = "success";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Edit(int id, string brandName)
        {
            string result = "error";
            Brand brand = db.Brands.FirstOrDefault(m => m.Brand_id == id);
            var checkExist = db.Brands.SingleOrDefault(m => m.Brand_name == brandName);
            try
            {
                if (checkExist != null)
                {
                    result = "exist";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                result = "success";
                brand.Brand_name = brandName;
                brand.Update_at = DateTime.Now;
                brand.Update_by = User.Identity.GetEmail();
                UpdateModel(brand);
                db.SaveChanges();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id)
        {
            string result = "error";
            Brand brand = db.Brands.FirstOrDefault(m => m.Brand_id == id);
            try
            {
                result = "delete";
                db.Brands.Remove(brand);
                db.SaveChanges();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        
    }
}
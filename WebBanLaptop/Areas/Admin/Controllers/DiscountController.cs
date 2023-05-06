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
    public class DiscountController : Controller
    {
        private LQShopDb db = new LQShopDb();
        // GET: Admin/Discount
        public ActionResult Index(int? page, string search)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            ViewBag.search = search;
            var list = db.Discounts.OrderByDescending(a => a.Create_at);
            if (!string.IsNullOrEmpty(search))
            {
                list = db.Discounts
                    .Where(a => a.Discount_name.Contains(search) || a.Discount_price.ToString().Contains(search))
                    .OrderByDescending(a => a.Create_at);
            }
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Create(DateTime discountStart, DateTime discountEnd, double discountPrice, string discountCode, Discount discount, int quantity)
        {
            string result = "false";
            try
            {
                discount.Discount_name = "Giảm " +
                        discountPrice.ToString("#,0₫") + " Từ " +
                        discountStart.ToString("dd-MM-yyyy") + " => " +
                        discountEnd.ToString("dd-MM-yyyy");
                discount.Discount_price = discountPrice;
                discount.Quantity = quantity;
                discount.Discount_star = discountStart;
                discount.Discount_end = discountEnd;
                discount.Discount_code = discountCode;
                discount.Create_by = User.Identity.GetEmail();
                discount.Update_by = User.Identity.GetEmail();
                discount.Create_at = DateTime.Now;
                discount.Update_at = DateTime.Now;
                db.Discounts.Add(discount);
                db.SaveChanges();
                result = "success";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, DateTime discountStart, DateTime discountEnd, double discountPrice, string discountCode, int quantity)
        {
            string result = "error";
            Discount discount = db.Discounts.FirstOrDefault(m => m.Disscount_id == id);
            try
            {
                discount.Discount_name = "Giảm " +
                discountPrice.ToString("#,0₫") + " Từ " +
                discountStart.ToString("dd-MM-yyyy") + " => " +
                discountEnd.ToString("dd-MM-yyyy");
                discount.Discount_price = discountPrice;
                discount.Discount_star = discountStart;
                discount.Discount_end = discountEnd;
                discount.Quantity = quantity;
                discount.Discount_code = discountCode;
                discount.Update_at = DateTime.Now;
                discount.Update_by = User.Identity.GetEmail();
                UpdateModel(discount);
                db.SaveChanges();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            string result = "error";
            Discount discount = db.Discounts.FirstOrDefault(m => m.Disscount_id == id);
            try
            {
                result = "delete";
                db.Discounts.Remove(discount);
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
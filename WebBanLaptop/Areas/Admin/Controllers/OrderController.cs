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
    public class OrderController : Controller
    {
        private LQShopDb db = new LQShopDb();
        // GET: Admin/Order
        public ActionResult Index(int? page, string search)
        {
            int pageSize = 9;
            int pageNumber = page ?? 1;
            ViewBag.search = search;
            ViewBag.countTrash = db.Orders.Where(a => a.Status == "0").Count(); //  đếm tổng sp có trong thùng rác
            var list = db.Orders.Where(a => a.Status != "0").OrderByDescending(a => a.Create_at);
            if (!string.IsNullOrEmpty(search))
            {
                list = db.Orders.Where(a => a.Order_id.ToString().Contains(search)).OrderByDescending(a => a.Create_at);
            }
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Trash(int? page, string search)
        {
            int pageSize = 9;
            int pageNumber = page ?? 1;
            ViewBag.search = search;
            var list = db.Orders.Where(a => a.Status == "0").OrderByDescending(a => a.Create_at);
            if (!string.IsNullOrEmpty(search))
            {
                list = db.Orders.Where(a => a.Order_id.ToString().Contains(search)).OrderByDescending(a => a.Create_at);
            }
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Details(int? id)
        {
            Order order = db.Orders.FirstOrDefault(m => m.Order_id == id);
            ViewBag.ListProduct = db.Order_Detail.Where(m => m.Order_id == order.Order_id).ToList();
            ViewBag.OrderHistory = db.Orders.Where(m => m.Account_id == order.Account_id).OrderByDescending(m => m.Order_date).Take(10).ToList();
            if (order == null)
            {
                Notification.SetNotification1_5s("Không tồn tại! (ID = " + id + ")", "warning");
                return RedirectToAction("Index");
            }
            return View(order);
        }
        public JsonResult UpdateOrder(int id, string status)
        {
            string result = "error";
            Order order = db.Orders.FirstOrDefault(m => m.Order_id == id);
            try
            {
                if (order.Status != "3")
                {
                    result = "success";
                    order.Status = status;
                    order.Update_at = DateTime.Now;
                    order.Update_by = User.Identity.GetEmail();
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    result = "false";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CancelOrder(int id)
        {
            string result = "error";
            Order order = db.Orders.FirstOrDefault(m => m.Order_id == id);
            var order_Details = db.Order_Detail.Where(m => m.Order_id == id).ToList();
            try
            {
                if (order.Status != "3")
                {
                    result = "success";
                    order.Status = "0";
                    order.Update_at = DateTime.Now;
                    order.Update_by = User.Identity.GetEmail();
                    int quan; //tạo biến lưu số lượng sp trước khi hủy đơn hàng
                    foreach (var item in order_Details)
                    {
                        var product = db.Products.FirstOrDefault(m => m.Product_id == item.Product_id);
                        quan = Int32.Parse(product.Quantity.Trim()); 
                        quan += item.Quantity; //tăng số lượng sp
                        product.Quantity = quan.ToString();
                        product.Update_by = User.Identity.GetName();
                        db.Entry(product).State = EntityState.Modified; //update lại số lượng sản phẩm

                        var discount = db.Discounts.FirstOrDefault(m => m.Disscount_id == item.Disscount_id);
                        discount.Quantity += 1;
                        db.Entry(discount).State = EntityState.Modified; //update lại số lượng discount
                    }
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    result = "false";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanLaptop.Areas.Admin.Controllers
{
    public class DashBoardController : Controller
    {
        private LQShopDb db = new LQShopDb();
        // GET: Admin/DashBoard
        public ActionResult Index()
        {
            ViewBag.Order = db.Orders.Where(m => m.Status == "3").ToList();
            ViewBag.OrderDetail = db.Order_Detail.ToList();
            ViewBag.ListOrderDetail = db.Order_Detail.Where(m => m.Order.Status == "3").OrderByDescending(m => m.Create_at).Take(3).ToList();
            ViewBag.ListOrder = db.Orders.OrderByDescending(m => m.Create_at).Take(7).ToList();
            return View();
        }
    }
}
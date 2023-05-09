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
        public ActionResult Index(FormCollection form)
        {
            ViewBag.Order = db.Orders.Where(m => m.Status == "3").ToList();
            ViewBag.OrderDetail = db.Order_Detail.ToList();
            ViewBag.ListOrderDetail = db.Order_Detail.Where(m => m.Order.Status == "3").OrderByDescending(m => m.Create_at).Take(3).ToList();
            ViewBag.ListOrder = db.Orders.OrderByDescending(m => m.Create_at).Take(7).ToList();
            
            var fromdate = Convert.ToDateTime(form["from-date"]);
            var todate = Convert.ToDateTime(form["to-date"]);
            var order = db.Orders.Where(m => m.Order_date > fromdate && m.Order_date < todate).ToList();
            ViewBag.Statistical = order;
            return View();
        }

    }
}
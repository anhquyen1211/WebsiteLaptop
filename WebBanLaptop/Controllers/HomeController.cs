using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanLaptop.Controllers
{
    public class HomeController : Controller
    {
        private LQShopDb db = new LQShopDb();
        public ActionResult Index()
        {
            ViewBag.HotProduct = db.Products.Where(p => p.Status == "1" && p.Quantity != "0").OrderByDescending(p => p.Buyturn + p.View).Take(8).ToList();
            ViewBag.NewProduct = db.Products.Where(p => p.Status == "1" && p.Quantity != "0").OrderByDescending(p => p.Create_at).Take(8).ToList();
            ViewBag.Laptop = db.Products.Where(p => p.Status == "1" && p.Type == 1 && p.Quantity != "0").Take(8).ToList();
            ViewBag.Accessory = db.Products.Where(p => p.Status == "1" && p.Type == 2 && p.Quantity != "0").Take(8).ToList();
            return View();
        }

    }
}
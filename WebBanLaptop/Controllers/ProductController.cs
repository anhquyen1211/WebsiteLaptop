using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using WebBanLaptop.Common;
using WebBanLaptop.Models;

namespace WebBanLaptop.Controllers
{
    public class ProductController : Controller
    {
        private LQShopDb db = new LQShopDb();
        // GET: Product
        //lấy danh sách laptop
        public ActionResult Laptop(int? page, string sortOrder)
        {
            ViewBag.Type = "Laptop";
            ViewBag.SortBy = "laptop" + "?";
            ViewBag.CountProduct = db.Products.Where(p => p.Type == 1).Count();
            return View("Products", GetProduct(p => p.Status == "1" && p.Type == ProductType.LAPTOP, page, sortOrder));
        }
        //Lấy danh sách phụ kiện
        public ActionResult Accessory(int? page, string sortOrder)
        {
            ViewBag.Type = "Phụ kiện";
            ViewBag.SortBy = "accessory" + "?";
            ViewBag.CountProduct = db.Products.Where(p => p.Type == 2).Count();
            return View("Products", GetProduct(p => p.Status == "1" && p.Type == ProductType.ACCESSORY, page, sortOrder));
        }
        //xem chi tiết sản phẩm
        public ActionResult ProductDetail(int id)
        {
            var product = db.Products.SingleOrDefault(p => p.Status == "1" && p.Product_id == id);
            if(product == null)
            {
                return Redirect("/");
            }
            ViewBag.RelatedProduct = db.Products.Where(p => p.Status == "1" && p.Quantity != "0" && p.Product_id != product.Product_id && p.Genre_id == product.Genre_id).Take(8).ToList();
            ViewBag.ListFeedback = db.Feedbacks.Where(m => m.Status == "2").ToList();
            ViewBag.OrderFeedback = db.Order_Detail.ToList();
            ViewBag.ListReplyFeedback = db.ReplyFeedbacks.Where(m => m.Status == "2").ToList();
            product.View++;
            db.SaveChanges();
            return View(product);
        }
        //Get product 
        private IPagedList GetProduct(Expression<Func<Product, bool>> expr, int? page, string sortOrder)
        {
            int pageSize = 9; //1 trang hiện thỉ tối đa 9 sản phẩm
            int pageNumber = (page ?? 1); //đánh số trang
            ViewBag.AvgFeedback = db.Feedbacks.ToList();
            ViewBag.OrderDetail = db.Order_Detail.ToList();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ResetSort = String.IsNullOrEmpty(sortOrder) ? "" : "";
            ViewBag.DateAscSort = sortOrder == "date_asc" ? "date_asc" : "date_asc";
            ViewBag.DateDescSort = sortOrder == "date_desc" ? "date_desc" : "date_desc";
            ViewBag.PopularSort = sortOrder == "popular" ? "popular" : "popular";
            ViewBag.PriceDescSort = sortOrder == "price_desc" ? "price_desc" : "price_desc";
            ViewBag.PriceAscSort = sortOrder == "price_asc" ? "price_asc" : "price_asc";
            ViewBag.NameAscSort = sortOrder == "name_asc" ? "name_asc" : "name_asc";
            ViewBag.NameDescSort = sortOrder == "name_desc" ? "name_desc" : "name_desc";
            var list = db.Products.Where(expr).OrderByDescending(p => p.Product_id).ToPagedList(pageNumber, pageSize);
            switch (sortOrder)
            {
                case "price_asc":
                    list = db.Products.Where(expr).OrderBy(p => (p.Price - p.Discount.Discount_price)).ToPagedList(pageNumber, pageSize);
                    break;
                case "price_desc":
                    list = db.Products.Where(expr).OrderByDescending(p => (p.Price - p.Discount.Discount_price)).ToPagedList(pageNumber, pageSize);
                    break;
                case "date_asc":
                    list = db.Products.Where(expr).OrderBy(p => p.Create_at).ToPagedList(pageNumber, pageSize);
                    break;
                case "date_desc":
                    list = db.Products.Where(expr).OrderByDescending(p => p.Create_at).ToPagedList(pageNumber, pageSize);
                    break;
                case "name_asc":
                    list = db.Products.Where(expr).OrderBy(p => p.Product_name).ToPagedList(pageNumber, pageSize);
                    break;
                case "name_desc":
                    list = db.Products.Where(expr).OrderByDescending(p => p.Product_name).ToPagedList(pageNumber, pageSize);
                    break;
                default:
                    list = db.Products.Where(expr).OrderByDescending(p => p.Create_at).ToPagedList(pageNumber, pageSize);
                    break;
            }
            ViewBag.Showing = list.Count();
            return list;
        }
    }
}
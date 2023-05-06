using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using WebBanLaptop.Common;
using WebBanLaptop.Common.Helpers;
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
            return View("Products", GetProduct(p => p.Status == "1" && p.Type == ProductType.ACCESSORY && p.Quantity != "0", page, sortOrder));
        }
        //xem chi tiết sản phẩm
        public ActionResult ProductDetail(int id, int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 1;
            var product = db.Products.SingleOrDefault(p => p.Status == "1" && p.Product_id == id);
            if(product == null)
            {
                return Redirect("/");
            }
            ViewBag.RelatedProduct = db.Products.Where(m => m.Status == "1" && m.Quantity != "0" && m.Product_id != product.Product_id && m.Genre_id == product.Genre_id).Take(8).ToList();
            ViewBag.ProductImage = db.ProductImages.Where(m => m.Product_id == id).ToList();
            ViewBag.ListFeedback = db.Feedbacks.Where(m => m.Product_id == product.Product_id && m.Status == "2")
                .OrderByDescending(m => m.Create_at).ToPagedList(pageNumber, pageSize);
            ViewBag.OrderFeedback = db.Order_Detail.ToList();
            ViewBag.ListReplyFeedback = db.ReplyFeedbacks.Where(m => m.Status == "2").ToList();
            ViewBag.CountFeedback = db.Feedbacks.Where(m => m.Status == "2" && m.Product_id == product.Product_id).Count();
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
        //Tìm kiếm sản phẩm
        public ActionResult SearchResult(int? page, string sortOrder, string s)
        {
            ViewBag.SortBy = "search?s=" + s + "&";
            ViewBag.Type = "Kết quả tìm kiếm - " + s;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ResetSort = String.IsNullOrEmpty(sortOrder) ? "" : "";
            ViewBag.DateAscSort = sortOrder == "date_asc" ? "date_asc" : "date_asc";
            ViewBag.DateDescSort = sortOrder == "date_desc" ? "date_desc" : "date_desc";
            ViewBag.PopularSort = sortOrder == "popular" ? "popular" : "popular";
            ViewBag.PriceDescSort = sortOrder == "price_desc" ? "price_desc" : "price_desc";
            ViewBag.PriceAscSort = sortOrder == "price_asc" ? "price_asc" : "price_asc";
            ViewBag.NameAscSort = sortOrder == "name_asc" ? "name_asc" : "name_asc";
            ViewBag.NameDescSort = sortOrder == "name_desc" ? "name_desc" : "name_desc";
            var list = db.Products.OrderByDescending(m => m.Product_id);
            switch (sortOrder)
            {
                case "price_asc":
                    list = (IOrderedQueryable<Product>)db.Products.OrderBy(m => (m.Price - m.Discount.Discount_price)).Where(m => m.Status == "1" && m.Product_name.Contains(s));
                    break;
                case "price_desc":
                    list = (IOrderedQueryable<Product>)db.Products.OrderByDescending(m => m.Price - m.Discount.Discount_price).Where(m => m.Status == "1" && m.Product_name.Contains(s));
                    break;
                case "date_asc":
                    list = (IOrderedQueryable<Product>)db.Products.OrderBy(m => m.Create_at).Where(m => m.Status == "1" && m.Product_name.Contains(s));
                    break;
                case "date_desc":
                    list = (IOrderedQueryable<Product>)db.Products.OrderByDescending(m => m.Create_at).Where(m => m.Status == "1" && m.Product_name.Contains(s));
                    break;
                case "name_asc":
                    list = (IOrderedQueryable<Product>)db.Products.OrderBy(m => m.Product_name).Where(m => m.Status == "1" && m.Product_name.Contains(s));
                    break;
                case "name_desc":
                    list = (IOrderedQueryable<Product>)db.Products.OrderByDescending(m => m.Product_name).Where(m => m.Status == "1" && m.Product_name.Contains(s));
                    break;
                default:
                    list = (IOrderedQueryable<Product>)db.Products.OrderByDescending(m => m.Create_at);
                    break;
            }
            ViewBag.Countproduct = db.Products.Where(m => m.Status == "1" && m.Product_name.Contains(s)).Count();
            return View("Products", GetProduct(m => m.Status == "1" && (m.Product_name.Contains(s) || m.Product_id.ToString().Contains(s)), page, sortOrder));
        }

        //Bình luận đánh giá
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult ProductComment(Feedback comment, int productID, int discountID, int genreID, int rateStar, string commentContent)
        {
            bool result = false;
            int userID = User.Identity.GetUserID();
            if (User.Identity.IsAuthenticated)
            {
                comment.Account_id = userID;
                comment.Rate_star = rateStar;
                comment.Product_id = productID;
                comment.Disscount_id = discountID;
                comment.Genre_id = genreID;
                comment.Content = commentContent;
                comment.Status = "2";
                comment.Create_at = DateTime.Now;
                comment.Update_at = DateTime.Now;
                comment.Create_by = userID.ToString();
                comment.Update_by = userID.ToString();
                db.Feedbacks.Add(comment);
                db.SaveChanges();
                result = true;
                Notification.SetNotification3s("Bình luận thành công", "success");
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        //Phản hồi bình luận/đánh giá
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ReplyComment(int id, string reply_content, ReplyFeedback reply)
        {
            bool result = false;
            if (User.Identity.IsAuthenticated)
            {
                reply.Feedback_id = id;
                reply.Account_id = User.Identity.GetUserID();
                reply.Content = reply_content;
                reply.Status = "2";
                reply.Create_at = DateTime.Now;
                db.ReplyFeedbacks.Add(reply);
                db.SaveChanges();
                result = true;
                Notification.SetNotification3s("Phản hồi thành công", "success");
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebBanLaptop.Common.Helpers;
using WebBanLaptop.Models;

namespace WebBanLaptop.Controllers
{
    public class CartController : Controller
    {
        private LQShopDb db = new LQShopDb();
        //Xem trước giỏ hàng ở bất kì layout nào
        public PartialViewResult PreviewCart()
        {
            var cart = this.GetCart();
            ViewBag.Quans = cart.Item2;
            var listProduct = cart.Item1.ToList();
            return PartialView("PreviewCart", listProduct);
        }
        //Xem giỏ hàng
        public ActionResult ViewCart()
        {
            var cart = this.GetCart();
            ViewBag.Quans = cart.Item2;
            double discount = 0d;
            var listProduct = cart.Item1.ToList();
            if (Session["Discount"] != null && Session["Discountcode"] != null)
            {
                var code = Session["Discountcode"].ToString();
                var discountupdatequan = db.Discounts.Where(d => d.Discount_code == code).FirstOrDefault();
                if (discountupdatequan.Quantity == 0 || discountupdatequan.Discount_star >= DateTime.Now || discountupdatequan.Discount_end <= DateTime.Now)
                {
                    Notification.SetNotification3s("Mã giảm giá không thể sử dụng", "error");
                    return View(listProduct);
                }
                discount = Convert.ToDouble(Session["Discount"].ToString());
                Session.Remove("Discount");
                Session.Remove("Discountcode");
                return View(listProduct);
            }
            return View(listProduct);
        }
        //Thanh toán giỏ hàng
        [Authorize]
        public ActionResult Checkout()
        {
            int userId = User.Identity.GetUserID();
            var user = db.Accounts.SingleOrDefault(u => u.Account_id == userId);
            var cart = this.GetCart();
            ViewBag.Quans = cart.Item2;
            ViewBag.ListProduct = cart.Item1.ToList();
            ViewBag.CountAddress = db.AccountAddresses.Where(m => m.Account_id == userId).Count();
            ViewBag.ListDistrict = db.Districts.OrderBy(m => m.District_name).ToList();
            ViewBag.ListProvince = db.Provinces.OrderBy(m => m.Province_name).ToList();
            ViewBag.ListWard = db.Wards.ToList().OrderBy(m => m.Ward_name).ToList();
            ViewBag.ListAddress = db.AccountAddresses.Where(m => m.Account_id == userId).OrderByDescending(m => m.IsDefault).ToList();
            ViewBag.MyAddress = db.AccountAddresses.FirstOrDefault(u => u.Account_id == userId && u.IsDefault == true);
            if (cart.Item2.Count < 1)
            {
                return RedirectToAction(nameof(ViewCart));
            }
            var products = cart.Item1;
            double total = 0d;
            double discount = 0d;
            double productPrice = 0d;
            for (int i = 0; i < products.Count; i++)
            {
                var item = products[i];
                productPrice = item.Price;
                if (item.Discount != null)
                {
                    if (item.Discount.Discount_star < DateTime.Now && item.Discount.Discount_end > DateTime.Now)
                    {
                        productPrice = item.Price - item.Discount.Discount_price;
                    }
                }
                total += productPrice * cart.Item2[i];
            }
            // Áp dụng mã giảm giá
            if (Session["Discount"] != null)
            {
                discount = Convert.ToDouble(Session["Discount"].ToString());
                total -= discount;
            }
            ViewBag.Total = total;
            ViewBag.Discount = discount;
            return View(user);
        }
        //Lưu đơn hàng
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> SaveOrder(OrderAddress orderAdress)
        {
            try
            {
                //double priceSum = 0;
                string productquancheck = "0";
                if (Session["Discount"] != null && Session["Discountcode"] != null)
                {
                    string check_discount = Session["Discountcode"].ToString();
                    var discountupdatequan = db.Discounts.Where(d => d.Discount_code == check_discount).SingleOrDefault();
                    //khi mua hàng có sử dụng mã giảm giá và đặt hàng thành công thì trừ số lượng mã giảm giá
                    if (discountupdatequan.Quantity == 0 || discountupdatequan.Discount_star >= DateTime.Now || discountupdatequan.Discount_end <= DateTime.Now)
                    {
                        Notification.SetNotification3s("Mã giảm giá không thể sử dụng", "error");
                        return RedirectToAction("ViewCart", "Cart");
                    }
                    else
                    {
                        int newquantity = (discountupdatequan.Quantity - 1);
                        discountupdatequan.Quantity = newquantity;
                    }
                }
                db.OrderAddresses.Add(orderAdress);
                var cart = this.GetCart();
                var listProduct = new List<Product>();
                var order = new Order()
                {
                    Account_id = User.Identity.GetUserID(),
                    Create_at = DateTime.Now,
                    Create_by = User.Identity.GetUserID().ToString(),
                    Status = "1", // Đơn hàng trạng thái chờ xử lý
                    Order_note = Request.Form["OrderNote"].ToString(),
                    Delivery_id = 1,
                    OrderAddressId = orderAdress.OrderAddressId,
                    Order_date = DateTime.Now,
                    Update_at = DateTime.Now,
                    Payment_id = 1,
                    Update_by = User.Identity.GetUserID().ToString(),
                    Total = Convert.ToDouble(TempData["Total"])
                };
                for (int i = 0; i < cart.Item1.Count; i++)
                {
                    var item = cart.Item1[i];
                    var _price = item.Price;
                    if (item.Discount != null)
                    {
                        if (item.Discount.Discount_star < DateTime.Now && item.Discount.Discount_end > DateTime.Now)
                        {
                            _price = item.Price - item.Discount.Discount_price;
                        }
                    }
                    order.Order_Detail.Add(new Order_Detail
                    {
                        Create_at = DateTime.Now,
                        Create_by = User.Identity.GetUserID().ToString(),
                        Disscount_id = item.Disscount_id,
                        Genre_id = item.Genre_id,
                        Price = _price,
                        Product_id = item.Product_id,
                        Quantity = cart.Item2[i],
                        Status = "1", // Đơn hàng trạng thái chờ xử lý
                        Update_at = DateTime.Now,
                        Update_by = User.Identity.GetUserID().ToString()
                    });
                    // Xóa cart
                    Response.Cookies["product_" + item.Product_id].Expires = DateTime.Now.AddDays(-10);
                    // Thay đổi số lượng và số lượt mua của product 
                    var product = db.Products.SingleOrDefault(p => p.Product_id == item.Product_id);
                    productquancheck = product.Quantity;
                    product.Buyturn += cart.Item2[i];
                    product.Quantity = (Convert.ToInt32(product.Quantity ?? "0") - cart.Item2[i]).ToString();
                    listProduct.Add(product);
                }
                //thêm dữ liệu vào table
                if (productquancheck.Trim() != "0")
                {
                    db.Orders.Add(order);
                }
                else
                {
                    Notification.SetNotification3s("Sản phẩm đã hết hàng", "error");
                    return RedirectToAction("ViewCart", "Cart");
                }
                db.Configuration.ValidateOnSaveEnabled = false;

                await db.SaveChangesAsync();
                Notification.SetNotification3s("Đặt hàng thành công", "success");
                Session.Remove("Discount");
                Session.Remove("Discountcode");
                return RedirectToAction("TrackingOrder", "Account");
            }
            catch
            {
                Notification.SetNotification3s("Lỗi! đặt hàng không thành công", "error");
                return RedirectToAction("Checkout", "Cart");
            }
        }

        //Áp dụng mã giảm giá
        public ActionResult UseDiscountCode(string code)
        {
            ViewBag.DiscountPrice = 0d;
            var discount = db.Discounts.SingleOrDefault(d => d.Discount_code == code);
            if (discount != null)
            {
                if (discount.Discount_star < DateTime.Now && discount.Discount_end > DateTime.Now && discount.Quantity != 0)
                {
                    Session["Discountcode"] = discount.Discount_code;
                    Session["Discount"] = discount.Discount_price;
                    return Json(new { success = true, discountPrice = discount.Discount_price }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false, discountPrice = 0 }, JsonRequestBehavior.AllowGet);
        }

        //Mapping sản phẩm lên view
        private Tuple<List<Product>, List<int>> GetCart()
        {
            //check null 
            var cart = Request.Cookies.AllKeys.Where(c => c.IndexOf("product_") == 0);
            var productIds = new List<int>();
            var quantities = new List<int>();
            var errorProduct = new List<string>();
            var cValue = "";
            // Lấy mã sản phẩm & số lượng trong giỏ hàng
            foreach (var item in cart)
            {
                try
                {
                    var tempArr = item.Split('_');
                    if (tempArr.Length != 2)
                    {
                        //Nếu không lấy được thì xem như sản phẩm đó bị lỗi
                        errorProduct.Add(item);
                        continue;
                    }
                    cValue = Request.Cookies[item].Value;
                    productIds.Add(Convert.ToInt32(tempArr[1]));
                    quantities.Add(Convert.ToInt32(String.IsNullOrEmpty(cValue) ? "0" : cValue));
                    if (cValue == "0")
                    {
                        Response.Cookies["product_" + tempArr[1]].Expires = DateTime.Now;
                    }
                }
                catch { }
            }
            // Select sản phẩm để hiển thị
            var listProduct = new List<Product>();
            foreach (var id in productIds)
            {
                try
                {
                    var product = db.Products.SingleOrDefault(p => p.Status == "1" && p.Product_id == id);
                    if (product != null)
                    {
                        listProduct.Add(product);
                    }
                    else
                    {
                        // Trường hợp ko chọn được sản phẩm như trong giỏ hàng
                        // Đánh dấu sản phẩm trong giỏ hàng là sản phẩm lỗi
                        errorProduct.Add("product-" + id);
                        quantities.RemoveAt(productIds.IndexOf(id));
                    }
                }
                catch { }
            }
            //Xóa sản phẩm bị lỗi khỏi cart
            foreach (var err in errorProduct)
            {
                Response.Cookies[err].Expires = DateTime.Now.AddDays(-1);
            }
            return new Tuple<List<Product>, List<int>>(listProduct, quantities);//lấy id sản phẩm và số lượng
        }
    }
}
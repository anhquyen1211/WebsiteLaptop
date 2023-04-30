using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebBanLaptop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //rút gọn link tìm kiếm sản phẩm
            routes.MapRoute(
              name: "search",
              url: "search",
             defaults: new { Controller = "Product", action = "SearchResult" }
            );

            //rút gọn link chi tiết sản phẩm
            routes.MapRoute(
                name: "chi tiet san pham",
                url: "{slug}-{id}",
                defaults: new { controller = "Product", action = "ProductDetail" }
            );

            //rút gọn link laptop
            routes.MapRoute(
                name: "laptop",
                url: "laptop",
                defaults: new { controller = "Product", action = "Laptop"}
            );

            //rút gọn link phụ kiện
            routes.MapRoute(
                name: "phu kien",
                url: "accessory",
                defaults: new { controller = "Product", action = "Accessory" }
            );

            //rút gọn link thông tin cá nhan
            routes.MapRoute(
              name: "Thong tin ca nhan",
              url: "infor",
              defaults: new { controller = "Account", action = "Editprofile", id = UrlParameter.Optional }
            );

            //rút gọn link đăng nhập
            routes.MapRoute(
                name: "Dang nhap",
                url: "login",
                defaults: new { controller = "Account", action = "Login" }
            );

            //rút gọn link đăng ký
            routes.MapRoute(
                name: "Dang ky",
                url: "register",
                defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional }
            );

            //thay đổi mật khảu
            routes.MapRoute(
              name: "doi mat khau",
              url: "change_password",
              defaults: new { controller = "Account", action = "ChangePassword", id = UrlParameter.Optional }
            );

            //xem chi tiết đơn hàng
            routes.MapRoute(
              name: "chi tiet don hang",
              url: "order_detail/{id}",
              defaults: new { controller = "Account", action = "TrackingOrderDetail", id = UrlParameter.Optional }
            );

            //quản lý địa chỉ
            routes.MapRoute(
              name: "quan ly dia chi",
              url: "list_address",
              defaults: new { controller = "Account", action = "Address", id = UrlParameter.Optional }
            );

            //rút gọn link quản lý đơn hàng
            routes.MapRoute(
              name: "lich su mua hang",
              url: "list_order",
              defaults: new { controller = "Account", action = "TrackingOrder", id = UrlParameter.Optional }
            );

            //rút gọn link thanh toán
            routes.MapRoute(
               name: "Thanh toan",
               url: "checkout",
               defaults: new { controller = "Cart", action = "Checkout", id = UrlParameter.Optional }
            );

            //rút gọn link giỏ hàng
            routes.MapRoute(
                name: "Gio Hang",
                url: "cart",
                defaults: new { controller = "Cart", action = "ViewCart", id = UrlParameter.Optional }
            );

            //default
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );   
        }
    }
}

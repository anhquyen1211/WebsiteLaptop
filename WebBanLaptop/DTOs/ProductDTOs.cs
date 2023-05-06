using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanLaptop.DTOs
{
    public class ProductDTOs
    {
        public string Product_name { get; set; }
        public string Genre_name { get; set; }
        public string Brand_name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public string Quantity { get; set; }
        public int Product_id { get; set; }
        public string Create_by { get; set; }
        public DateTime Create_at { get; set; }
        public string Update_by { get; set; }
        public DateTime Update_at { get; set; }
        public string Status { get; set; }
        public long View { get; set; }
        public double Discount_price { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Discount_start { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Discount_end { get; set; }

        public string Discount_name { get; set; }
        public double Discount_id { get; set; }

        public string Description { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}
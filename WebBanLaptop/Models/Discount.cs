using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanLaptop.Models
{
    [Table("Discount")]
    public class Discount
    {
        [Key] public int Disscount_id { get; set; }

        [Required] [StringLength(100)] public string Discount_name { get; set; }

        public DateTime Discount_star { get; set; }

        public DateTime Discount_end { get; set; }

        public double Discount_price { get; set; }
        public int Quantity { get; set; }
        [StringLength(10)] public string Discount_code { get; set; }

        public DateTime Create_at { get; set; }

        [Required] [StringLength(100)] public string Create_by { get; set; }


        [Required] [StringLength(100)] public string Update_by { get; set; }

        public DateTime Update_at { get; set; }
    }
}
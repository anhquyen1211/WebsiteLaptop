using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanLaptop.Models
{
    [Table("Delivery")]
    public class Delivery
    {
        [Key] public int Delivery_id { get; set; }

        [Required] [StringLength(100)] public string Delivery_name { get; set; }

        [Column(TypeName = "money")] public decimal Price { get; set; }

        public DateTime Create_at { get; set; }

        [Required] [StringLength(20)] public string Create_by { get; set; }

        [StringLength(1)] public string Status { get; set; }

        [Required] [StringLength(20)] public string Update_by { get; set; }

        public DateTime? Update_at { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanLaptop.Models
{
    [Table("Payment")]
    public class Payment
    {
        public Payment()
        {
            Orders = new HashSet<Order>();
        }
        [Key] public int Payment_id { get; set; }

        [Required] [StringLength(50)] public string Payment_name { get; set; }

        public DateTime Create_at { get; set; }

        [Required] [StringLength(20)] public string Create_by { get; set; }

        [StringLength(1)] public string Status { get; set; }

        [StringLength(20)] public string Update_by { get; set; }

        public DateTime? Update_at { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
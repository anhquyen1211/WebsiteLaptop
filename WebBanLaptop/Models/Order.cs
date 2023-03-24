using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanLaptop.Models
{
    [Table("Order")]
    public class Order
    {
        public Order()
        {
            Order_Detail = new HashSet<Order_Detail>();
        }
        [Key] 
        [Column(Order = 0)]
        public int Order_id { get; set; }

        public int? OrderAddressId { get; set; }
        public int Payment_id { get; set; }
        public int Delivery_id { get; set; }
        public DateTime Order_date { get; set; }

        [Column(Order = 1)]
        public int Account_id { get; set; }
        [StringLength(1)] public string Status { get; set; }

        [StringLength(200)]
        public string Order_note { get; set; }
        public DateTime Create_at { get; set; }
        public double Total { get; set; }
        [Required] [StringLength(100)] public string Create_by { get; set; }
        [Required] [StringLength(100)] public string Update_by { get; set; }
        public DateTime Update_at { get; set; }
        public virtual Account Account { get; set; }
        public virtual Delivery Delivery { get; set; }
        public virtual ICollection<Order_Detail> Order_Detail { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual OrderAddress OrderAddress { get; set; }

    }
}
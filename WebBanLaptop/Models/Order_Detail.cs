using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanLaptop.Models
{
    [Table("OrderDetail")]
    public partial class Order_Detail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Product_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Genre_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Disscount_id { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Order_id { get; set; }

        public double Price { get; set; }

        [StringLength(1)]
        public string Status { get; set; }

        public int Quantity { get; set; }
        [Required]
        [StringLength(20)]
        public string Create_by { get; set; }

        public DateTime Create_at { get; set; }

        [Required]
        [StringLength(20)]
        public string Update_by { get; set; }
        public DateTime Update_at { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

    }
}
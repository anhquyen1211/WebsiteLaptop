using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace WebBanLaptop.Models
{
    [Table("Product")]
    public class Product
    {
        public Product()
        {
            Feedbacks = new HashSet<Feedback>();
            Order_Detail = new HashSet<Order_Detail>();
            ProductImages = new HashSet<ProductImages>();
        }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key] [Column(Order = 0)] public int Product_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Vui lòng chọn thể loại")]
        public int Genre_id { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn thương hiệu")]
        public int Brand_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Vui lòng chọn chương trình giảm giá")]
        public int Disscount_id { get; set; }


        [StringLength(200, ErrorMessage = "Tên sản phẩm không được quá 200 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        public string Product_name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá")]
        public double Price { get; set; }

        public long View { get; set; }

        public long Buyturn { get; set; }


        private string _quantity;
        [StringLength(10)]
        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        public string Quantity
        {
            get { return ((this._quantity != "" && this._quantity != null) ? this._quantity.Trim() : this._quantity); }
            set { this._quantity = (value == null) ? "" : value.Trim(); }
        }

        [StringLength(1)] public string Status { get; set; }

        [Required] [StringLength(100)] public string Create_by { get; set; }

        public DateTime Create_at { get; set; }
        [StringLength(100)]
        public string Update_by { get; set; }

        public DateTime Update_at { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập loại sản phẩm")]
        public int? Type { get; set; }
        public string Specifications { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Discount Discount { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual ICollection<Order_Detail> Order_Detail { get; set; }
        public virtual ICollection<ProductImages> ProductImages { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }

        [NotMapped]
        public HttpPostedFileBase[] ImageUploadMulti { get; set; }
    }
}
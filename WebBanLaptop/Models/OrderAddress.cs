using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanLaptop.Models
{
    [Table("OrderAddress")]
    public class OrderAddress
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderAddressId { get; set; }
        //tỉnh thành phố
        public int? Province_id { get; set; }
        //quận, huyện
        public int? District_id { get; set; }
        //phường xã
        public int? Ward_id { get; set; }
        [StringLength(10)]
        public string OrderPhonenumber { get; set; }
        //số diện
        [StringLength(20)]
        public string OrderUsername { get; set; }
        [StringLength(150)]
        public string Content { get; set; }
        public virtual Wards Wards { get; set; }
        public virtual Districts Districts { get; set; }
        public virtual Provinces Provinces { get; set; }
    }
}
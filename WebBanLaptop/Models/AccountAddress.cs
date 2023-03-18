using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanLaptop.Models
{
    [Table("AccountAddress")]
    public class AccountAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Account_address_id { get; set; }

        [Required]
        public int Account_id { get; set; }

        [Required]
        //tỉnh thành phố
        public int Province_id { get; set; }

        [Required]
        //quận, huyện
        public int District_id { get; set; }

        [Required]
        //phường xã
        public int Ward_id { get; set; }

        [StringLength(10)]
        public string Account_phonenumber { get; set; }

        [StringLength(20)]
        public string Account_username { get; set; }

        [StringLength(50)]
        //địa chỉ cụ thể
        public string Content { get; set; }

        //đặt làm đia chỉ mặc định hay không
        public bool IsDefault { get; set; }
    }
}
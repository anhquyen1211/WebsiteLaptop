using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanLaptop.Models
{
    [Table("Provinces")]
    public class Provinces
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Province_id { get; set; }

        [Required(ErrorMessage = "Nhập tên Tỉnh/Thành Phố")]
        [StringLength(50)]
        public string Province_name { get; set; }

        [Required]
        [StringLength(20)]
        public string Type { get; set; }
        public virtual ICollection<Districts> Districts { get; set; }
        public virtual ICollection<AccountAddress> AccountAddresses { get; set; }
        public virtual ICollection<OrderAddress> OrderAddress { get; set; }
    }
}
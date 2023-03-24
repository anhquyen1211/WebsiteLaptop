using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanLaptop.Models
{
    [Table("Wards")]
    public class Wards
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Ward_id { get; set; }

        [Required]
        public int District_id { get; set; }

        [Required]
        [StringLength(50)]
        public string Ward_name { get; set; }

        [Required]
        [StringLength(20)]
        public string Type { get; set; }
        public virtual Districts Districts { get; set; }
        public virtual ICollection<AccountAddress> AccountAddresses { get; set; }
        public virtual ICollection<OrderAddress> OrderAddress { get; set; }

    }
}
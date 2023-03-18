using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanLaptop.Models
{
    [Table("Brand")]
    public class Brand
    {
        [Key] public int Brand_id { get; set; }

        [Required] 
        [StringLength(50)] 
        public string Brand_name { get; set; }

        [Required]
        [StringLength(100)] 
        public string Create_by { get; set; }

        public DateTime Create_at { get; set; }

        [Required]
        [StringLength(100)] 
        public string Update_by { get; set; }
        public DateTime Update_at { get; set; }
    }
}
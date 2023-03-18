using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanLaptop.Models
{
    [Table("Districts")]
    public class Districts
    {
        //district id
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int District_id { get; set; }
        [Required]
        //city id
        public int Province_id { get; set; }
        //district name
        [Required]
        [StringLength(50)]
        public string District_name { get; set; }
        //district type
        [Required]
        [StringLength(20)]
        public string Type { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanLaptop.Models
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Contact_id { get; set; }

        [Required] 
        public string Name { get; set; }

        [Required] 
        public string Phone { get; set; }

        [Required] 
        public string Email { get; set; }

        [Required] 
        public string Content { get; set; }


        [Required] 
        [StringLength(1)] 
        public string Status { get; set; }

        [Required] 
        [StringLength(20)] 
        public string Create_by { get; set; }

        public DateTime Create_at { get; set; }

        [Required] 
        [StringLength(20)] 
        public string Update_by { get; set; }

        public DateTime? Update_at { get; set; }
    }
}
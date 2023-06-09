﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public virtual Provinces Provinces { get; set; }
        public virtual ICollection<Wards> Wards { get; set; }
        public virtual ICollection<AccountAddress> AccountAddresses { get; set; }
        public virtual ICollection<OrderAddress> OrderAddress { get; set; }
    }
}
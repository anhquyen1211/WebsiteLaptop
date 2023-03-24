using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanLaptop.Models
{
    [Table("Feedback")]
    public partial class Feedback
    {
        public Feedback()
        {
            ReplyFeedbacks = new HashSet<ReplyFeedback>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Feedback_id { get; set; }

        public int Account_id { get; set; }

        public int Product_id { get; set; }

        public int Genre_id { get; set; }

        public int Disscount_id { get; set; }

        public string Content { get; set; }

        [Required]
        public int Rate_star { get; set; }

        public DateTime Create_at { get; set; }

        [Required]
        [StringLength(100)]
        public string Create_by { get; set; }

        [StringLength(1)]
        public string Status { get; set; }

        [Required]
        [StringLength(100)]
        public string Update_by { get; set; }
        public DateTime? Update_at { get; set; }
        public virtual Account Account { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<ReplyFeedback> ReplyFeedbacks { get; set; }
    }
}
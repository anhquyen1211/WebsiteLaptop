using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanLaptop.Models
{
    [Table("ReplyFeedback")]
    public partial class ReplyFeedback
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Rep_feedback_id { get; set; }

        public int Feedback_id { get; set; }

        public int Account_id { get; set; }

        public string Content { get; set; }

        [StringLength(1)]
        public string Status { get; set; }
        public DateTime Create_at { get; set; }
        public virtual Account Account { get; set; }
        public virtual Feedback Feedback { get; set; }

    }
}
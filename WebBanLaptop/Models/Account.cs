using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanLaptop.Models
{
    [Table("Account")]
    public class Account
    {      
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key] public int Account_id { get; set; }

        [Required(ErrorMessage = "Nhập Email")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng định dạng email")]
        //[RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Must be a valid Email Address")]
        //[RegularExpression(@"[A-Za-z0-9._%+-]+[A-Za-z0-9.-]+\.[A-Za-z] {2,4}")]
        public string Email { get; set; }

        // [Required(ErrorMessage = "Nhập mật khẩu")]
        //[RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])).{8,}$",
        //ErrorMessage = "Mật khẩu tổi thiếu 8 ký tự bao gồm: chữ thường, chừ hoa, và chữ số")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Request_code { get; set; }
        public int Role { get; set; }

        [Required(ErrorMessage = "Nhập họ tên")]
        [StringLength(50)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nhập số điện thoại")]
        [StringLength(10, ErrorMessage = "Số điện thoại phải đúng 10 chữ số", MinimumLength = 10)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Column(TypeName = "text")]
        public string Avatar { get; set; }

        [StringLength(100)]
        public string Create_by { get; set; }
        public DateTime Create_at { get; set; }

        [StringLength(100)]
        public string Update_by { get; set; }
        public DateTime Update_at { get; set; }
        public string Status { get; set; }

    }
}
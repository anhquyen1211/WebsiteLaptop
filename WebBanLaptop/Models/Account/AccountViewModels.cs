﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanLaptop.Models
{
    public class LoginViewModels
    {
        [DisplayName("Email")]
        [Required(ErrorMessage = "Nhập Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class RegisterViewModels
    {
        [DisplayName("Email")]
        [Required(ErrorMessage = "Nhập Email")]
        [MaxLength(100, ErrorMessage = "Email tối đa 100 ký tự")]
        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng định dạng email")]
        public string Email { get; set; }

        [DisplayName("Họ tên")]
        [Required(ErrorMessage = "Nhập họ tên")]
        [MaxLength(30, ErrorMessage = "Họ tên tối đa 30 ký tự")]
        public string Name { get; set; }

        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Nhập Mật khẩu")]
        [MaxLength(30, ErrorMessage = "Mật khẩu tối đa 30 ký tự")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]))(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Mật khẩu tổi thiếu 8 ký tự bao gồm: chữ thường, chữ hoa, chữ số và 1 ký tự đặc biệt")]
        public string Password { get; set; }

        [DisplayName("Mật khẩu xác nhận")]
        [Required(ErrorMessage = "Nhập Mật khẩu xác nhận")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không trùng nhau")]
        public string PasswordConfirm { get; set; }

        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Nhập số điện thoại")]
        [StringLength(10)]
        [RegularExpression("^(0)([0-9]{9})$", ErrorMessage = "Số điện thoại phải bắt đầu bằng 0, chứa ký tự số từ (0 -> 9) và đủ 10 chữ số")]
        public string PhoneNumber { get; set; }

    }
    public class ChangePasswordViewModels
    {
        public int AccountID { get; set; }

        [DisplayName("Mật khẩu cũ")]
        [Required(ErrorMessage = "Nhập Mật khẩu cũ")]
        public string OldPassword { get; set; }

        [DisplayName("Mật khẩu mới")]
        [Required(ErrorMessage = "Nhập Mật khẩu mới")]
        [MaxLength(30, ErrorMessage = "Mật khẩu tối đa 30 ký tự")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]))(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Mật khẩu tổi thiếu 8 ký tự bao gồm: chữ thường, chữ hoa, chữ số và 1 ký tự đặc biệt")]
        public string NewPassword { get; set; }

        [DisplayName("Mật khẩu xác nhận")]
        [Required(ErrorMessage = "Nhập Mật khẩu xác nhận")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không trùng với mật khẩu mới")]
        public string PasswordConfirm { get; set; }

    }
    
}
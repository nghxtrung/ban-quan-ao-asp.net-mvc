using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ban_quan_ao_asp.net_mvc.Models
{
    public class Account
    {
        public string MaNguoiDung { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
        public string TenDangNhap { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
        public string TenNguoiDung { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
        public string SoDienThoai { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
        public string MatKhau { get; set; }
    }
}
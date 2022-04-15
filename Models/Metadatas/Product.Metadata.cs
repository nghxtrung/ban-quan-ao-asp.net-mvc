using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ban_quan_ao_asp.net_mvc.Models
{
    public class Product
    {
        [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
        public string MaSanPham { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
        public string TenSanPham { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
        public decimal GiaNhap { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
        public decimal GiaBan { get; set; }
        public Nullable<bool> TrangThai { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
        public string MoTaNgan { get; set; }
        public string MoTaChiTiet { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn dữ liệu cho trường này")]
        public HttpPostedFileBase AnhDaiDien { get; set; }
        public Nullable<bool> NoiBat { get; set; }
        public string PhanLoaiPhu { get; set; }
    }
}
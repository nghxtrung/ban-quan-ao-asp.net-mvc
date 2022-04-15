using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ban_quan_ao_asp.net_mvc.Models
{
    public class ProductCategory
    {
        public string MaSanPham { get; set; }
        public string MaMau { get; set; }
        public string KichCo { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
        public int SoLuong { get; set; }
        public List<HttpPostedFileBase> AnhPhanLoai { get; set; }
    }
}
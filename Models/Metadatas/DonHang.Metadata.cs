using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ban_quan_ao_asp.net_mvc.Models
{
    [MetadataTypeAttribute(typeof(DonHangMetadata))]
    public partial class DonHang
    {
        internal sealed class DonHangMetadata
        {
            public string MaDonHang { get; set; }
            public string MaKM { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
            public string TenKhachHang { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
            public string SoDienThoaiNhanHang { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
            public string DiaChiGiaoHang { get; set; }
            public DateTime NgayDatHang { get; set; }
            public string TinhTrang { get; set; }
            [Required(ErrorMessage = "Vui lòng chọn dữ liệu cho trường này")]
            public string PTThanhToan { get; set; }
            public string GhiChu { get; set; }
            public string MaNguoiDung { get; set; }
        }
    }
}
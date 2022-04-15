using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ban_quan_ao_asp.net_mvc.Models
{
    public class ProductCart
    {
        QLShopBanHangOnlineEntities db = new QLShopBanHangOnlineEntities();
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string AnhDaiDien { get; set; }
        public string MaMau { get; set; }
        public string TenMau { get; set; }
        public string KichCo { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien 
        {
            get { return DonGia * SoLuong; }
        }

        public ProductCart(string maSanPham, string maMau, string kichCo, int soLuong)
        {
            SanPham sanPham = db.SanPhams.SingleOrDefault(n => n.MaSanPham == maSanPham);
            MauSac mauSac = db.MauSacs.SingleOrDefault(n => n.MaMau == maMau);
            this.MaSanPham = maSanPham;
            this.TenSanPham = sanPham.TenSanPham;
            this.AnhDaiDien = sanPham.AnhDaiDien;
            this.MaMau = maMau;
            this.TenMau = mauSac.TenMau;
            this.KichCo = kichCo;
            this.DonGia = sanPham.GiaBan;
            this.SoLuong = soLuong;
        }
    }
}
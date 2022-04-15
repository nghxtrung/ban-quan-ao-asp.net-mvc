using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using ban_quan_ao_asp.net_mvc.Models;

namespace ban_quan_ao_asp.net_mvc.Controllers
{
    public class ProductAPIController : ApiController
    {
        QLShopBanHangOnlineEntities db = new QLShopBanHangOnlineEntities();

        [HttpGet]
        public List<getAllImageColorProduct_Result> GetImageColorProduct(string masanpham, string mamau)
        {
            return db.getAllImageColorProduct(masanpham, mamau).ToList();
        }

        [HttpGet]
        public List<getSizeProduct_Result> GetSizeProduct(string masanpham, string mamau)
        {
            return db.getSizeProduct(masanpham, mamau).ToList();
        }

        [HttpGet]
        public Coupon GetCoupon(string makm)
        {
            KhuyenMai khuyenMai = db.KhuyenMais.FirstOrDefault(n => n.MaKM == makm);
            if (khuyenMai != null)
                return new Coupon(khuyenMai.MaKM, khuyenMai.PhanTramGiam);
            return null;
        }

        [HttpGet]
        public List<PhanLoai> GetSecondaryCategory(string primarycategory)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.PhanLoais.Where(n => n.PhanLoaiChinh == primarycategory).ToList();
        }

        [HttpGet]
        public List<MauSac> GetColor()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.MauSacs.ToList();
        }
    }
}

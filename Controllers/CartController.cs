using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ban_quan_ao_asp.net_mvc.Models;
using System.Dynamic;

namespace ban_quan_ao_asp.net_mvc.Controllers
{
    public class CartController : Controller
    {
        QLShopBanHangOnlineEntities db = new QLShopBanHangOnlineEntities();

        // GET: Cart
        public List<ProductCart> GetProductCartList()
        {
            List<ProductCart> productCartList = Session["Cart"] as List<ProductCart>;
            if (productCartList == null)
            {
                productCartList = new List<ProductCart>();
                Session["Cart"] = productCartList;
            }
            return productCartList;
        }

        public ActionResult AddCart(string masanpham, string mamau, string kichco, int soluong)
        {
            List<ProductCart> productCartList = GetProductCartList();
            ProductCart productCart = productCartList.Find(n => n.MaSanPham == masanpham 
                                        && n.MaMau == mamau && n.KichCo == kichco);
            if (productCart == null)
            {
                productCart = new ProductCart(masanpham, mamau, kichco, soluong);
                productCartList.Add(productCart);
                return RedirectToAction("Detail", "Cart");
            }
            else
            {
                productCart.SoLuong += soluong;
                return RedirectToAction("Detail", "Cart");
            }
        }

        public decimal SumTotalProduct()
        {
            decimal sumTotal = 0;
            List<ProductCart> productCartList = GetProductCartList();
            if (productCartList != null)
            {
                sumTotal = productCartList.Sum(n => n.ThanhTien);
            }
            return sumTotal;
        }

        public ActionResult Detail()
        {
            if (Session["Cart"] == null)
            {
                RedirectToAction("Category", "Product");
            }
            List<ProductCart> productCartList = GetProductCartList();
            ViewBag.sumTotalProduct = SumTotalProduct();
            string couponCode;
            decimal couponPrice;
            if (Session["Coupon"] != null)
            {
                couponCode = ((Coupon)Session["Coupon"]).couponCode;
                couponPrice = SumTotalProduct() / 100 * (int)((Coupon)Session["Coupon"]).percentDecrease;
            }
            else
            {
                couponCode = "";
                couponPrice = 0;
            }
            if (SumTotalProduct() != 0)
                ViewBag.sumTotal = SumTotalProduct() + 30000 - couponPrice;
            else
                ViewBag.sumTotal = 0;
            ViewBag.couponCode = couponCode;
            ViewBag.couponPrice = couponPrice;
            return View(productCartList);
        }

        [HttpGet]
        public bool UpdateProductCartQuantity(string masanpham, string mamau, string kichco, int soluong)
        {
            List<ProductCart> productCartList = GetProductCartList();
            ProductCart productCart = productCartList.Find(n => n.MaSanPham == masanpham
                                        && n.MaMau == mamau && n.KichCo == kichco);
            productCart.SoLuong = soluong;
            return true;
        }

        [HttpGet]
        public bool RemoveProductCartQuantity(string masanpham, string mamau, string kichco)
        {
            List<ProductCart> productCartList = GetProductCartList();
            ProductCart productCart = productCartList.Find(n => n.MaSanPham == masanpham
                                        && n.MaMau == mamau && n.KichCo == kichco);
            productCartList.Remove(productCart);
            return true;
        }

        [HttpGet]
        public bool SaveCoupon(string makm)
        {
            KhuyenMai khuyenMai = db.KhuyenMais.FirstOrDefault(n => n.MaKM == makm);
            if (khuyenMai != null)
                Session["Coupon"] = new Coupon(khuyenMai.MaKM, khuyenMai.PhanTramGiam);
            else
                Session["Coupon"] = null;
            return true;
        }

        [HttpGet]
        public JsonResult GetCoupon()
        {
            return Json((Coupon)Session["Coupon"], JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            if (Session["User"] == null || Session["User"].ToString() == "")
            {
                return RedirectToAction("Login", "User");
            }
            if (GetProductCartList().Count == 0)
            {
                return RedirectToAction("Category", "Product");
            }
            decimal couponPrice;
            if (Session["Coupon"] != null)
            {
                couponPrice = SumTotalProduct() / 100 * (int)((Coupon)Session["Coupon"]).percentDecrease;
            }
            else
            {
                couponPrice = 0;
            }
            ViewBag.sumTotalProduct = SumTotalProduct();
            ViewBag.couponPrice = couponPrice;
            ViewBag.sumTotal = SumTotalProduct() + 30000 - couponPrice;
            ViewBag.userName = ((NguoiDung)Session["User"]).TenNguoiDung;
            ViewBag.phoneNumber = ((NguoiDung)Session["User"]).SoDienThoai;
            ViewBag.address = ((NguoiDung)Session["User"]).DiaChi;
            ViewBag.productCartList = GetProductCartList();
            return View();
        }

        public string generateOrderCode()
        {
            int quantityOrder = db.DonHangs.ToList().Count;
            return "DH" + (quantityOrder + 1);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout([Bind(Include = "TenKhachHang, SoDienThoaiNhanHang, DiaChiGiaoHang, GhiChu, PTThanhToan")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                donHang.MaDonHang = generateOrderCode();
                donHang.MaKM = ((Coupon)Session["Coupon"]) != null ? ((Coupon)Session["Coupon"]).couponCode : null;
                donHang.NgayDatHang = DateTime.Now;
                donHang.TinhTrang = "Chưa xác nhận";
                donHang.MaNguoiDung = ((NguoiDung)Session["User"]).MaNguoiDung;
                decimal couponPrice;
                if (Session["Coupon"] != null)
                {
                    couponPrice = SumTotalProduct() / 100 * (int)((Coupon)Session["Coupon"]).percentDecrease;
                }
                else
                {
                    couponPrice = 0;
                }
                donHang.TongTien = SumTotalProduct() + 30000 - couponPrice;
                db.DonHangs.Add(donHang);
                foreach (var productCartItem in GetProductCartList())
                {
                    ChiTietDH chiTietDH = new ChiTietDH();
                    chiTietDH.MaDonHang = generateOrderCode();
                    chiTietDH.MaMSSP = db.PhanLoaiMSSPs.Single(n => n.MaSanPham == productCartItem.MaSanPham
                                                                && n.MaMau == productCartItem.MaMau).MaMSSP;
                    chiTietDH.KichCo = productCartItem.KichCo;
                    chiTietDH.SoLuongMua = productCartItem.SoLuong;
                    db.ChiTietDHs.Add(chiTietDH);
                }
                db.SaveChanges();
                return RedirectToAction("Order", "User");
            }
            else
            {
                if (donHang.TenKhachHang != null)
                    ViewBag.userName = donHang.TenKhachHang;
                if (donHang.SoDienThoaiNhanHang != null)
                    ViewBag.phoneNumber = donHang.SoDienThoaiNhanHang;
                if (donHang.DiaChiGiaoHang != null)
                    ViewBag.address = donHang.DiaChiGiaoHang;
                decimal couponPrice;
                if (Session["Coupon"] != null)
                {
                    couponPrice = SumTotalProduct() / 100 * (int)((Coupon)Session["Coupon"]).percentDecrease;
                }
                else
                {
                    couponPrice = 0;
                }
                ViewBag.sumTotalProduct = SumTotalProduct();
                ViewBag.couponPrice = couponPrice;
                ViewBag.sumTotal = SumTotalProduct() + 30000 - couponPrice;
                ViewBag.productCartList = GetProductCartList();
            }
            return View(donHang);
        }
    }
}
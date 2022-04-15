using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ban_quan_ao_asp.net_mvc.Models;
using System.IO;
using System.Dynamic;

namespace ban_quan_ao_asp.net_mvc.Controllers
{
    public class AdminController : Controller
    {
        QLShopBanHangOnlineEntities db = new QLShopBanHangOnlineEntities();

        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return RedirectToAction("Login", "User");
            }
            ViewBag.ProductCount = db.SanPhams.ToList().Count;
            ViewBag.UserCount = db.NguoiDungs.ToList().Count;
            ViewBag.OrderCount = db.DonHangs.ToList().Count;
            ViewBag.Revenue = db.DonHangs.Sum(n => n.TongTien);
            return View();
        }

        public ActionResult Logout()
        {
            Session["Admin"] = null;
            return RedirectToAction("Login", "User");
        }

        public ActionResult AddProduct()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            string primaryCategoryFirst = db.PhanLoais.GroupBy(n => n.PhanLoaiChinh).Select(n => n.FirstOrDefault()).FirstOrDefault().PhanLoaiChinh;
            ViewBag.PhanLoaiChinh = new SelectList(db.PhanLoais.GroupBy(n => n.PhanLoaiChinh).Select(n => n.FirstOrDefault()), "PhanLoaiChinh", "PhanLoaiChinh");
            ViewBag.PhanLoaiPhu = new SelectList(db.PhanLoais.Where(n => n.PhanLoaiChinh == primaryCategoryFirst), "MaPhanLoai", "PhanLoaiPhu");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(Product product)
        {
            string primaryCategoryFirst = db.PhanLoais.GroupBy(n => n.PhanLoaiChinh).Select(n => n.FirstOrDefault()).FirstOrDefault().PhanLoaiChinh;
            ViewBag.PhanLoaiChinh = new SelectList(db.PhanLoais.GroupBy(n => n.PhanLoaiChinh).Select(n => n.FirstOrDefault()), "PhanLoaiChinh", "PhanLoaiChinh");
            ViewBag.PhanLoaiPhu = new SelectList(db.PhanLoais.Where(n => n.PhanLoaiChinh == primaryCategoryFirst), "MaPhanLoai", "PhanLoaiPhu");
            if (ModelState.IsValid)
            {
                string representImageFileName = Path.GetFileName(product.AnhDaiDien.FileName);
                string representImageFilePath = Path.Combine(Server.MapPath("~/Content/assets/img/test-sp-img"), representImageFileName);
                product.AnhDaiDien.SaveAs(representImageFilePath);

                SanPham sanPham = new SanPham();
                sanPham.MaSanPham = product.MaSanPham;
                sanPham.TenSanPham = product.TenSanPham;
                sanPham.GiaNhap = product.GiaNhap;
                sanPham.GiaBan = product.GiaBan;
                sanPham.TrangThai = product.TrangThai;
                sanPham.MoTaNgan = product.MoTaNgan;
                sanPham.AnhDaiDien = representImageFileName;
                sanPham.NoiBat = product.NoiBat;
                sanPham.MaPhanLoai = product.PhanLoaiPhu;
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("AddCategoryProduct", new { masanpham = product.MaSanPham });
            }
            return View(product);
        }

        [HttpGet]
        public ActionResult AddCategoryProduct(string masanpham)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            if (masanpham == null || masanpham.Trim() == "")
            {
                return RedirectToAction("AddProduct");
            }
            SanPham sanPham = db.SanPhams.SingleOrDefault(n => n.MaSanPham == masanpham);
            if (sanPham == null)
            {
                return RedirectToAction("AddProduct");
            }
            ViewBag.MaSanPham = masanpham;
            ViewBag.CategorySizeColorList = db.getAllCategoryColorSize(masanpham);
            ViewBag.MaMau = new SelectList(db.MauSacs, "MaMau", "TenMau");
            ViewBag.ErrorMessageCategory = "";
            ViewBag.errorMessageFile = "";
            return View();
        }

        public string generateCategoryColorCode()
        {
            int quantityCategoryColor = db.PhanLoaiMSSPs.ToList().Count;
            return "MSSP" + (quantityCategoryColor + 1);
        }

        public string generateImageCode()
        {
            int quantityImage = db.Anhs.ToList().Count;
            return "A" + (quantityImage + 1);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategoryProduct(ProductCategory productCategory)
        {
            ViewBag.MaSanPham = productCategory.MaSanPham;
            ViewBag.MaMau = new SelectList(db.MauSacs, "MaMau", "TenMau");
            if (db.getCategoryColorSize(productCategory.MaSanPham, productCategory.MaMau, productCategory.KichCo).ToList().Count != 0)
                ViewBag.ErrorMessageCategory = "Vui lòng chọn lại dữ liệu cho trường này";
            if (productCategory.AnhPhanLoai.First() == null)
                ViewBag.ErrorMessageFile = "Vui lòng chọn dữ liệu cho trường này";
            else
            {
                if (ModelState.IsValid)
                {
                    PhanLoaiMSSP phanLoaiMSSP = new PhanLoaiMSSP();
                    phanLoaiMSSP.MaMSSP = generateCategoryColorCode();
                    phanLoaiMSSP.MaSanPham = productCategory.MaSanPham;
                    phanLoaiMSSP.MaMau = productCategory.MaMau;
                    db.PhanLoaiMSSPs.Add(phanLoaiMSSP);
                    db.SaveChanges();
                    foreach (var item in productCategory.AnhPhanLoai)
                    {
                        string categoryImageFileName = Path.GetFileName(item.FileName);
                        string categoryImageFilePath = Path.Combine(Server.MapPath("~/Content/assets/img/test-sp-img"), categoryImageFileName);
                        item.SaveAs(categoryImageFilePath);

                        Anh anh = new Anh();
                        anh.MaAnh = generateImageCode();
                        anh.TenFileAnh = categoryImageFileName;
                        db.Anhs.Add(anh);
                        db.SaveChanges();
                        db.insertImageProduct(phanLoaiMSSP.MaMSSP, anh.MaAnh);
                    }
                    ChiTietSP chiTietSP = new ChiTietSP();
                    chiTietSP.MaMSSP = phanLoaiMSSP.MaMSSP;
                    chiTietSP.KichCo = productCategory.KichCo;
                    chiTietSP.SoLuong = productCategory.SoLuong;
                    db.ChiTietSPs.Add(chiTietSP);
                    db.SaveChanges();
                }
            }
            ViewBag.CategorySizeColorList = db.getAllCategoryColorSize(productCategory.MaSanPham);
            return View(productCategory);
        }

        public ActionResult Product()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            ViewBag.ProductList = db.getAllProduct().ToList();
            return View();
        }

        public ActionResult DeleteProduct(string masanpham)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            db.deleteProduct(masanpham);
            return RedirectToAction("Product");
        }

        public ActionResult UpdateStatus(string masanpham ,bool trangthai)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            bool status = trangthai == true ? false : true;
            db.updateStatusProduct(masanpham, status);
            return RedirectToAction("Product");
        }

        public ActionResult UpdateFeatureStatus(string masanpham, bool noibat)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            bool status = noibat == true ? false : true;
            db.updateFeatureStatusProduct(masanpham, status);
            return RedirectToAction("Product");
        }

        public ActionResult Coupon()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            ViewBag.CouponList = db.KhuyenMais.ToList();
            return View();
        }

        public ActionResult Classify()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            ViewBag.ClassifyList = db.PhanLoais.ToList();
            return View();
        }

        public ActionResult Order()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View(db.DonHangs.ToList());
        }

        public PartialViewResult OrderDetailPartial(string madonhang)
        {
            ViewBag.MaDonHang = madonhang;
            return PartialView(db.getAllDetailOrder(madonhang).ToList());
        }

        public ActionResult RemoveOrder(string madonhang)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            db.removeOrder(madonhang);
            return RedirectToAction("Order", "Admin");
        }

        public ActionResult User()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            ViewBag.UserList = db.NguoiDungs.ToList();
            return View();
        }

        public ActionResult Revenue()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            ViewBag.RevenueList = db.DonHangs.ToList();
            ViewBag.SumRevenue = db.DonHangs.Sum(n => n.TongTien);
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ban_quan_ao_asp.net_mvc.Models;

namespace ban_quan_ao_asp.net_mvc.Controllers
{
    public class UserController : Controller
    {
        QLShopBanHangOnlineEntities db = new QLShopBanHangOnlineEntities();

        // GET: User
        [HttpGet]
        public ActionResult Login()
        {
            if (Session["User"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (Session["Admin"] != null)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            string accountName = f["txtAccountName"].ToString();
            string password = f["txtPassword"].ToString();
            NguoiDung nguoiDung = db.NguoiDungs.SingleOrDefault(n => n.TenDangNhap == accountName
                                            && n.MatKhau == password);
            if (nguoiDung != null)
            {
                if (accountName == "admin" && password == "admin")
                {
                    Session["Admin"] = nguoiDung;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    Session["User"] = nguoiDung;
                    return RedirectToAction("Index", "Home");
                }
            }
            Session["User"] = null;
            Session["Admin"] = null;
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (Session["User"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (Session["Admin"] != null)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        public string generateUserCode()
        {
            int quantityUser = db.NguoiDungs.ToList().Count;
            return "ND" + (quantityUser + 1);
        }

        [HttpPost]
        public ActionResult Register(Account account)
        {
            if (ModelState.IsValid)
            {
                NguoiDung nguoiDung = new NguoiDung();
                nguoiDung.MaNguoiDung = generateUserCode();
                nguoiDung.TenDangNhap = account.TenDangNhap;
                nguoiDung.TenNguoiDung = account.TenNguoiDung;
                nguoiDung.Email = account.Email;
                nguoiDung.SoDienThoai = account.SoDienThoai;
                nguoiDung.MatKhau = account.MatKhau;
                db.NguoiDungs.Add(nguoiDung);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Info()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            ViewBag.accountName = ((NguoiDung)Session["User"]).TenNguoiDung;
            ViewBag.phoneNumber = ((NguoiDung)Session["User"]).SoDienThoai;
            ViewBag.email = ((NguoiDung)Session["User"]).Email;
            ViewBag.address = ((NguoiDung)Session["User"]).DiaChi;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Info([Bind(Include = "TenNguoiDung, SoDienThoai, Email, DiaChi")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                nguoiDung.MaNguoiDung = ((NguoiDung)Session["User"]).MaNguoiDung;
                nguoiDung.TenDangNhap = ((NguoiDung)Session["User"]).TenDangNhap;
                nguoiDung.MatKhau = ((NguoiDung)Session["User"]).MatKhau;
                db.Entry(nguoiDung).State = EntityState.Modified;
                db.SaveChanges();
                Session["User"] = nguoiDung;
                ViewBag.accountName = nguoiDung.TenNguoiDung;
                ViewBag.phoneNumber = nguoiDung.SoDienThoai;
                ViewBag.email = nguoiDung.Email;
                ViewBag.address = nguoiDung.DiaChi;
                return View();
            }
            else
            {
                if (nguoiDung.TenNguoiDung != null)
                    ViewBag.accountName = nguoiDung.TenNguoiDung;
                if (nguoiDung.SoDienThoai != null)
                    ViewBag.phoneNumber = nguoiDung.SoDienThoai;
                if (nguoiDung.Email != null)
                    ViewBag.email = nguoiDung.Email;
                if (nguoiDung.DiaChi != null)
                    ViewBag.address = nguoiDung.DiaChi;
            }
            return View(nguoiDung);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(FormCollection f)
        {
            string oldPassword = f["txtOldPassword"].ToString();
            string newPassword = f["txtNewPassword"].ToString();
            if (newPassword.Trim() == "")
                ViewBag.errorMessageNewPass = "Vui lòng nhập dữ liệu cho trường này";
            if (oldPassword.Trim() == "")
                ViewBag.errorMessageOldPass = "Vui lòng nhập dữ liệu cho trường này";
            else
            {
                if (oldPassword != ((NguoiDung)Session["User"]).MatKhau)
                    ViewBag.errorMessageOldPass = "Vui lòng kiếm tra lại mật khẩu";
                else
                {
                    NguoiDung nguoiDung = new NguoiDung();
                    nguoiDung.MaNguoiDung = ((NguoiDung)Session["User"]).MaNguoiDung;
                    nguoiDung.TenDangNhap = ((NguoiDung)Session["User"]).TenDangNhap;
                    nguoiDung.MatKhau = newPassword;
                    nguoiDung.TenNguoiDung = ((NguoiDung)Session["User"]).TenNguoiDung;
                    nguoiDung.SoDienThoai = ((NguoiDung)Session["User"]).SoDienThoai;
                    nguoiDung.Email = ((NguoiDung)Session["User"]).Email;
                    nguoiDung.DiaChi = ((NguoiDung)Session["User"]).DiaChi;
                    db.Entry(nguoiDung).State = EntityState.Modified;
                    db.SaveChanges();
                    Session["User"] = nguoiDung;
                }
            }
            return View();
        }

        public PartialViewResult OrderDetailPartial(string madonhang)
        {
            ViewBag.MaDonHang = madonhang;
            return PartialView(db.getAllDetailOrder(madonhang).ToList());
        }

        [HttpGet]
        public ActionResult Order()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            string userCode = ((NguoiDung)Session["User"]).MaNguoiDung;
            List<DonHang> orderList = db.DonHangs.Where(n => n.MaNguoiDung == userCode).ToList();
            return View(orderList);
        }

        public ActionResult RemoveOrder(string madonhang)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            db.removeOrder(madonhang);
            return RedirectToAction("Order", "User");
        }
    }
}
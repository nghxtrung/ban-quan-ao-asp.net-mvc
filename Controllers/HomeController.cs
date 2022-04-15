using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ban_quan_ao_asp.net_mvc.Models;
using System.Dynamic;

namespace ban_quan_ao_asp.net_mvc.Controllers
{
    public class HomeController : Controller
    {
        QLShopBanHangOnlineEntities db = new QLShopBanHangOnlineEntities();

        public ActionResult Index()
        {
            dynamic homeModel = new ExpandoObject();
            homeModel.phanLoaiList = db.PhanLoais.ToList();
            homeModel.sanPhamNoiBatList = db.SanPhams.Where(n => n.NoiBat == true);
            return View(homeModel);
        }

        public PartialViewResult UserPartial()
        {
            NguoiDung nguoiDung = Session["User"] as NguoiDung;
            return PartialView(nguoiDung);
        }

        public PartialViewResult CategoryPartial()
        {
            dynamic phanLoaiModel = new ExpandoObject();
            phanLoaiModel.phanLoaiList = db.PhanLoais.ToList();
            phanLoaiModel.phanLoaiChinhList = db.PhanLoais.GroupBy(n => n.PhanLoaiChinh).Select(n => n.FirstOrDefault()).ToList();
            return PartialView(phanLoaiModel);
        }
    }
}

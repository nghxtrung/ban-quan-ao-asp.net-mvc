using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ban_quan_ao_asp.net_mvc.Models;
using PagedList;
using PagedList.Mvc;
using System.Dynamic;

namespace ban_quan_ao_asp.net_mvc.Controllers
{
    public class ProductController : Controller
    {
        QLShopBanHangOnlineEntities db = new QLShopBanHangOnlineEntities();

        // GET: Product
        public ActionResult Category(int? page, string maphanloai = "PL1")
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            var productListCategory = db.getProductsCategory(maphanloai).ToList();
            if (productListCategory.Count() == 0)
            {
                ViewBag.productListCategory = null;
            }
            else
            {
                ViewBag.maPhanLoai = maphanloai;
                ViewBag.productListCategory = db.getProductsCategory(maphanloai).ToList().ToPagedList(pageNumber, pageSize);
            }
            return View();
        }

        public ActionResult Detail(string masanpham = "SP1")
        {
            string colorFirst = db.getAllColorProduct(masanpham).FirstOrDefault().MaMau;
            string category = db.getCategoryProduct(masanpham).FirstOrDefault().MaPhanLoai;
            dynamic detailModel = new ExpandoObject();
            detailModel.colorFirst = colorFirst;
            detailModel.sizeFirst = db.getSizeProduct(masanpham, colorFirst).FirstOrDefault().KichCo;
            detailModel.product = db.SanPhams.Single(n => n.MaSanPham == masanpham);
            detailModel.productSizeList = db.getSizeProduct(masanpham, colorFirst);
            detailModel.productColorList = db.getAllColorProduct(masanpham).ToList();
            detailModel.productColorImageList = db.getAllImageColorProduct(masanpham, colorFirst).ToList();
            detailModel.productListCategory = db.getProductsCategory(category).ToList();
            return View(detailModel);
        }

        [HttpPost]
        public ActionResult Searching(FormCollection f, int? page)
        {
            string searchkey = f["txtKeyWord"].ToString();
            ViewBag.keyword = searchkey;
            List<getAllProductSearch_Result> productListSearch = db.getAllProductSearch(searchkey).ToList();
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            if (productListSearch.Count == 0)
            {
                ViewBag.productListSearch = null;
            }
            else
            {
                ViewBag.productListSearch = db.getAllProductSearch(searchkey).ToList().ToPagedList(pageNumber, pageSize);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Searching(int? page, string searchkey)
        {
            ViewBag.keyword = searchkey;
            List<getAllProductSearch_Result> productListSearch = db.getAllProductSearch(searchkey).ToList();
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            if (productListSearch.Count == 0)
            {
                ViewBag.productListSearch = null;
            }
            else
            {
                ViewBag.productListSearch = db.getAllProductSearch(searchkey).ToList().ToPagedList(pageNumber, pageSize);
            }
            return View();
        }
    }
}
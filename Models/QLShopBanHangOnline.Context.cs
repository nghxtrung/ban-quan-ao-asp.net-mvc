//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ban_quan_ao_asp.net_mvc.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class QLShopBanHangOnlineEntities : DbContext
    {
        public QLShopBanHangOnlineEntities()
            : base("name=QLShopBanHangOnlineEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Anh> Anhs { get; set; }
        public virtual DbSet<ChiTietDH> ChiTietDHs { get; set; }
        public virtual DbSet<ChiTietSP> ChiTietSPs { get; set; }
        public virtual DbSet<DonHang> DonHangs { get; set; }
        public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }
        public virtual DbSet<MauSac> MauSacs { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<PhanLoai> PhanLoais { get; set; }
        public virtual DbSet<PhanLoaiMSSP> PhanLoaiMSSPs { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<DanhGia> DanhGias { get; set; }
    
        public virtual ObjectResult<getAllColorProduct_Result> getAllColorProduct(string masanpham)
        {
            var masanphamParameter = masanpham != null ?
                new ObjectParameter("masanpham", masanpham) :
                new ObjectParameter("masanpham", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getAllColorProduct_Result>("getAllColorProduct", masanphamParameter);
        }
    
        public virtual ObjectResult<getAllImageColorProduct_Result> getAllImageColorProduct(string masanpham, string mamau)
        {
            var masanphamParameter = masanpham != null ?
                new ObjectParameter("masanpham", masanpham) :
                new ObjectParameter("masanpham", typeof(string));
    
            var mamauParameter = mamau != null ?
                new ObjectParameter("mamau", mamau) :
                new ObjectParameter("mamau", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getAllImageColorProduct_Result>("getAllImageColorProduct", masanphamParameter, mamauParameter);
        }
    
        public virtual ObjectResult<getSizeProduct_Result> getSizeProduct(string masanpham, string mamau)
        {
            var masanphamParameter = masanpham != null ?
                new ObjectParameter("masanpham", masanpham) :
                new ObjectParameter("masanpham", typeof(string));
    
            var mamauParameter = mamau != null ?
                new ObjectParameter("mamau", mamau) :
                new ObjectParameter("mamau", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getSizeProduct_Result>("getSizeProduct", masanphamParameter, mamauParameter);
        }
    
        public virtual int insertImageProduct(string mamssp, string maanh)
        {
            var mamsspParameter = mamssp != null ?
                new ObjectParameter("mamssp", mamssp) :
                new ObjectParameter("mamssp", typeof(string));
    
            var maanhParameter = maanh != null ?
                new ObjectParameter("maanh", maanh) :
                new ObjectParameter("maanh", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("insertImageProduct", mamsspParameter, maanhParameter);
        }
    
        public virtual ObjectResult<getCategoryProduct_Result> getCategoryProduct(string masanpham)
        {
            var masanphamParameter = masanpham != null ?
                new ObjectParameter("masanpham", masanpham) :
                new ObjectParameter("masanpham", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getCategoryProduct_Result>("getCategoryProduct", masanphamParameter);
        }
    
        public virtual ObjectResult<getProductsCategory_Result> getProductsCategory(string maphanloai)
        {
            var maphanloaiParameter = maphanloai != null ?
                new ObjectParameter("maphanloai", maphanloai) :
                new ObjectParameter("maphanloai", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getProductsCategory_Result>("getProductsCategory", maphanloaiParameter);
        }
    
        public virtual ObjectResult<getCategoryColorSize_Result> getCategoryColorSize(string masanpham, string mamau, string kichco)
        {
            var masanphamParameter = masanpham != null ?
                new ObjectParameter("masanpham", masanpham) :
                new ObjectParameter("masanpham", typeof(string));
    
            var mamauParameter = mamau != null ?
                new ObjectParameter("mamau", mamau) :
                new ObjectParameter("mamau", typeof(string));
    
            var kichcoParameter = kichco != null ?
                new ObjectParameter("kichco", kichco) :
                new ObjectParameter("kichco", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getCategoryColorSize_Result>("getCategoryColorSize", masanphamParameter, mamauParameter, kichcoParameter);
        }
    
        public virtual ObjectResult<getAllCategoryColorSize_Result> getAllCategoryColorSize(string masanpham)
        {
            var masanphamParameter = masanpham != null ?
                new ObjectParameter("masanpham", masanpham) :
                new ObjectParameter("masanpham", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getAllCategoryColorSize_Result>("getAllCategoryColorSize", masanphamParameter);
        }
    
        public virtual ObjectResult<getAllProduct_Result> getAllProduct()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getAllProduct_Result>("getAllProduct");
        }
    
        public virtual int deleteProduct(string masanpham)
        {
            var masanphamParameter = masanpham != null ?
                new ObjectParameter("masanpham", masanpham) :
                new ObjectParameter("masanpham", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("deleteProduct", masanphamParameter);
        }
    
        public virtual int updateStatusProduct(string masanpham, Nullable<bool> trangthai)
        {
            var masanphamParameter = masanpham != null ?
                new ObjectParameter("masanpham", masanpham) :
                new ObjectParameter("masanpham", typeof(string));
    
            var trangthaiParameter = trangthai.HasValue ?
                new ObjectParameter("trangthai", trangthai) :
                new ObjectParameter("trangthai", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("updateStatusProduct", masanphamParameter, trangthaiParameter);
        }
    
        public virtual int removeOrder(string madonhang)
        {
            var madonhangParameter = madonhang != null ?
                new ObjectParameter("madonhang", madonhang) :
                new ObjectParameter("madonhang", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("removeOrder", madonhangParameter);
        }
    
        public virtual int updateFeatureStatusProduct(string masanpham, Nullable<bool> noibat)
        {
            var masanphamParameter = masanpham != null ?
                new ObjectParameter("masanpham", masanpham) :
                new ObjectParameter("masanpham", typeof(string));
    
            var noibatParameter = noibat.HasValue ?
                new ObjectParameter("noibat", noibat) :
                new ObjectParameter("noibat", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("updateFeatureStatusProduct", masanphamParameter, noibatParameter);
        }
    
        public virtual ObjectResult<getAllDetailOrder_Result> getAllDetailOrder(string madonhang)
        {
            var madonhangParameter = madonhang != null ?
                new ObjectParameter("madonhang", madonhang) :
                new ObjectParameter("madonhang", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getAllDetailOrder_Result>("getAllDetailOrder", madonhangParameter);
        }
    
        public virtual ObjectResult<getAllProductSearch_Result> getAllProductSearch(string tensanpham)
        {
            var tensanphamParameter = tensanpham != null ?
                new ObjectParameter("tensanpham", tensanpham) :
                new ObjectParameter("tensanpham", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getAllProductSearch_Result>("getAllProductSearch", tensanphamParameter);
        }
    }
}

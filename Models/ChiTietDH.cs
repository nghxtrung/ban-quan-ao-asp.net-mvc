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
    using System.Collections.Generic;
    
    public partial class ChiTietDH
    {
        public string MaDonHang { get; set; }
        public string MaMSSP { get; set; }
        public string KichCo { get; set; }
        public int SoLuongMua { get; set; }
    
        public virtual ChiTietSP ChiTietSP { get; set; }
        public virtual DonHang DonHang { get; set; }
    }
}
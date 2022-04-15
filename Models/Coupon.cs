using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ban_quan_ao_asp.net_mvc.Models
{
    public class Coupon
    {
        public string couponCode { get; set; }
        public int? percentDecrease { get; set; }

        public Coupon(string couponCode, int? percentDecrease)
        {
            this.couponCode = couponCode;
            this.percentDecrease = percentDecrease;
        }
    }
}
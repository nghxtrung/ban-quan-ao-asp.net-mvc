using System.Web;
using System.Web.Mvc;

namespace ban_quan_ao_asp.net_mvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

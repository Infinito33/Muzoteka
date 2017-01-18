using Muzoteka.App_Start;
using System.Web;
using System.Web.Mvc;

namespace Muzoteka
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AuthorizeRolesAttribute(new[] {"ADMIN", "USER"}));
        }
    }
}

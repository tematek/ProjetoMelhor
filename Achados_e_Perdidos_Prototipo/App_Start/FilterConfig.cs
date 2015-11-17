using System.Web;
using System.Web.Mvc;

namespace Achados_e_Perdidos_Prototipo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
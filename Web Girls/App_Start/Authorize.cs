using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Web_Girls.App_Start
{
    public class UserAuthorize : AuthorizeAttribute
    {
        public int[] quyen { get; set; }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string ma = (string)HttpContext.Current.Session["Ma"];
            if (ma != null)
            {
                int quyentk = (int)HttpContext.Current.Session["Quyen"];
                foreach (var q in quyen)
                {
                    if (q == quyentk)
                    {
                        return;
                    }
                }
                filterContext.Result = new RedirectToRouteResult(new
                    System.Web.Routing.RouteValueDictionary(new { controller = "Account", action = "KhongCoQuyen" }));

            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
                    System.Web.Routing.RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }

        }
    }
}
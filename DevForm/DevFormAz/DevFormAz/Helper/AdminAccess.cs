using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevFormAz.Helper
{
    public class AdminAccess:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var AdminId = 0;
            try
            {
                AdminId = (int)httpContext.Session["AdminId"];
                if (AdminId > 0) return true;
            }
            catch
            {
                return false;
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/AdminPanel/AdminHome/Login");
        }
    }
}
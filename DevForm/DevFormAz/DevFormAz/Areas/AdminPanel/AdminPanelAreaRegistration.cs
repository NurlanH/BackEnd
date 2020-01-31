using System.Web.Mvc;

namespace DevFormAz.Areas.AdminPanel
{
    public class AdminPanelAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AdminPanel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {


            context.MapRoute(
                "AdminPanel_default",
                "AdminPanel/{controller}/{action}/{id}",
                new { area = "AdminPanel", controller ="AdminHome",action = "Index", id = UrlParameter.Optional }
            );
          
        }
    }
}
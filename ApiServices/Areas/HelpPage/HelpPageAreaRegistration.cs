using System.Web.Http;
using System.Web.Mvc;

namespace ApiServices.Areas.HelpPage
{
    public class HelpPageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "HelpPage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            //context.MapRoute(
            //    "HelpPage_Default",
            //    "Help/{action}/{apiId}",
            //    new { controller = "Help", action = "Index", apiId = UrlParameter.Optional });

            context.MapRoute(
                name:"HelpPage_Default",
                url:"Help/{action}/{apiId}",
                defaults : new { controller = "Help", action = "Index", apiId = UrlParameter.Optional }
                );


            //context.Routes.MapHttpRoute(
            //    name: "HelpPage_Default",
            //    routeTemplate: "Help/{action}/{apiId}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}
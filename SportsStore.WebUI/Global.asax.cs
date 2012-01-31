using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SportsStore.WebUI.Infrastructure;
using SportsStore.Services;
using Microsoft.ApplicationServer.Http.Activation;
using System.ServiceModel.Activation;

namespace SportsStore.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Product", action = "List", id = UrlParameter.Optional }, // Parameter defaults
                new { controller = new NotInValuesConstraint(new[] { "productService" }) }
            );

            routes.Add(new ServiceRoute("productService", new HttpServiceHostFactory(), typeof(ProductService)));
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);            

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }
    }

    public class NotInValuesConstraint : IRouteConstraint
    {
        public NotInValuesConstraint(params string[] values)
        {
            _values = values;
        }

        private string[] _values;

        public bool Match(HttpContextBase httpContext,
          Route route,
          string parameterName,
          RouteValueDictionary values,
          RouteDirection routeDirection)
        {
            string value = values[parameterName].ToString();
            return !_values.Contains(value, StringComparer.CurrentCultureIgnoreCase);
        }
    }
}
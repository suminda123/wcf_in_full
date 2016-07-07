using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using VehicleApp.Proxies;
using VehicleApp.Web;
using VehicleDataApp.Contracts;

namespace VehicleApp.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
			AutoFacContainerBuilder();
        }

	    private static void AutoFacContainerBuilder()
	    {
		    var builder = new ContainerBuilder();

		    // Get your HttpConfiguration.
		    var config = GlobalConfiguration.Configuration;

		    // Register your Web API controllers.
		    builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

		    builder.RegisterInstance(new VehicleClient()).As<IVehicleService>();

		    // OPTIONAL: Register the Autofac filter provider.
		    builder.RegisterWebApiFilterProvider(config);

		    // Set the dependency resolver to be Autofac.
		    var container = builder.Build();
		    config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
	    }
    }
}

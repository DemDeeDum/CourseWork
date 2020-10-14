using System;
using Ninject;
using Ninject.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Hotel.WEB.Infrastructure;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Hotel.BLL.Util;
using Hotel.BLL.BookingKiller;


namespace Hotel.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
          
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var ServiceReg = new ServiceReg();
            var UoWReg = new UoWReg();
            var kernel = new StandardKernel(UoWReg, ServiceReg);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            Mapper.Initialize(x => x.AddProfile<HotelMapper>());

            Destroyer destroyer = new Destroyer();

        }
    }
}

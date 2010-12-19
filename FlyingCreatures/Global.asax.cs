using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;
using StructureMap.Configuration;
using FlyingCreatures.Repository.Interface;
using FlyingCreatures.Repository.Concrete;

namespace FlyingCreatures
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
                new { controller = "Creature", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            // Initialize the Repository factory.
            ObjectFactory.Initialize(
                x =>
                {
                    x.For<IUnitOfWorkFactory>().Use<RavenUnitOfWorkFactory>();
                    x.For(typeof(IRepository<>)).Use(typeof(RavenRepository<>));
                }
            );

            // RavenDB initialization method.
            RavenUnitOfWorkFactory.SetObjectContext(
                () =>
                {
                    var documentStore = new Raven.Client.Document.DocumentStore { Url = "http://localhost:8080" };
                    documentStore.Conventions.IdentityPartsSeparator = "-";
                    documentStore.Initialize();

                    return documentStore;
                }
            );
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            // Close the UnitOfWork (database connection) if this is the final request. IIS will call this method for any file request (*.css, *.jpg, *.gif, etc).
            string path = Request.Url.AbsolutePath.ToLower();
            if (path.IndexOf(".css") == -1 && path.IndexOf(".js") == -1 && path.IndexOf(".gif") == -1 && path.IndexOf(".png") == -1 && path.IndexOf(".jpg") == -1)
            {
                UnitOfWork.Current.Dispose();
            }
        }
    }
}
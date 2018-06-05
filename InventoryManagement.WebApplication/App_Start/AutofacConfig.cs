using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using InventoryManagement.Commons.Infastructure;
using InventoryManagement.Repositories.Infastructure;
using InventoryManagement.Services.Infastructure;

namespace InventoryManagement.WebApplication
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterFilterProvider();

            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterModule(new Log4NetAutofacModule());

            builder.RegisterModule(new RepositoryAutofacModule());

            builder.RegisterModule(new ServiceAutofacModule());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
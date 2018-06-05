using Autofac;
using InventoryManagement.Repositories.Interface.Infastructure;
using InventoryManagement.Repositories.Interface.User;
using InventoryManagement.Repositories.User;

namespace InventoryManagement.Repositories.Infastructure
{
    public class RepositoryAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseContext>().As<IDatabaseContext>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerLifetimeScope().PropertiesAutowired();
        }
    }
}

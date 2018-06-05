using Autofac;
using InventoryManagement.Services.Interface;
using InventoryManagement.Services.Interface.Utility;
using InventoryManagement.Services.Utility;
using Microsoft.AspNet.Identity;

namespace InventoryManagement.Services.Infastructure
{
    public class ServiceAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PasswordHasher>().As<IPasswordHasher>().InstancePerLifetimeScope();
            builder.RegisterType<PasswordGeneratorService>().As<IPasswordGeneratorService>().InstancePerLifetimeScope();
            builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        }
    }
}

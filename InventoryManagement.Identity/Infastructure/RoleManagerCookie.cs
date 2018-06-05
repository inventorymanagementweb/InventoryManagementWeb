using System;
using InventoryManagement.Identity.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace InventoryManagement.Identity.Infastructure
{
    public class RoleManagerCookie : RoleManager<Role, Guid>
    {
        public RoleManagerCookie(IRoleStore<Role, Guid> roleStore) : base(roleStore)
        {
        }

        public static RoleManagerCookie Create(IdentityFactoryOptions<RoleManagerCookie> options, IOwinContext context)
        {
            var appRoleManager = new RoleManagerCookie(new IdentityRoleStore(context.Get<DbContext.IdentityDbContext>()));

            return appRoleManager;
        }
    }
}

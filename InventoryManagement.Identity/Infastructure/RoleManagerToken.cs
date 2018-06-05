using System;
using InventoryManagement.Identity.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace InventoryManagement.Identity.Infastructure
{
    public class RoleManagerToken : RoleManager<Role, Guid>
    {
        public RoleManagerToken(IRoleStore<Role, Guid> roleStore) : base(roleStore)
        {
        }

        public static RoleManagerToken Create(IdentityFactoryOptions<RoleManagerToken> options, IOwinContext context)
        {
            var appRoleManager = new RoleManagerToken(new IdentityRoleStore(context.Get<DbContext.IdentityDbContext>()));

            return appRoleManager;
        }
    }
}

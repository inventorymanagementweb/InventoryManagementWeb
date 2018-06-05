using System;
using InventoryManagement.Identity.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using IdentityDbContext = InventoryManagement.Identity.DbContext.IdentityDbContext;

namespace InventoryManagement.Identity.Infastructure
{
    public class IdentityRoleStore : RoleStore<Role, Guid, UserRole>
    {
        public IdentityRoleStore(IdentityDbContext context) : base(context)
        {
        }
    }
}

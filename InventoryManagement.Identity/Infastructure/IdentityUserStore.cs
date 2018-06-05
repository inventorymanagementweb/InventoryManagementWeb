using System;
using InventoryManagement.Identity.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using IdentityDbContext = InventoryManagement.Identity.DbContext.IdentityDbContext;

namespace InventoryManagement.Identity.Infastructure
{
    public class IdentityUserStore : UserStore<User, Role, Guid, UserLogin, UserRole, UserClaim>
    {
        public IdentityUserStore(IdentityDbContext context) : base(context)
        {
        }
    }
}

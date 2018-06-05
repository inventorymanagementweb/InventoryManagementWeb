using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InventoryManagement.Identity.Model
{
    public class Role : IdentityRole<Guid, UserRole>
    {
    }
}

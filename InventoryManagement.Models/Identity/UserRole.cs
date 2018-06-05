using System;

namespace InventoryManagement.Models.Identity
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}

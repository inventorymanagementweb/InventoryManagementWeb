using System;
using InventoryManagement.Models.Identity;
using InventoryManagement.Repositories.Infastructure;
using InventoryManagement.Repositories.Interface.Infastructure;
using InventoryManagement.Repositories.Interface.User;

namespace InventoryManagement.Repositories.User
{
    public class RoleRepository : BaseRepository<Role, Guid>, IRoleRepository
    {
        public RoleRepository(IDatabaseContext context) : base(context)
        {
        }
    }
}

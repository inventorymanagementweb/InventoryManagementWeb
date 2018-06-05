using System;
using InventoryManagement.Models.Identity;
using InventoryManagement.Repositories.Interface.Infastructure;

namespace InventoryManagement.Repositories.Interface.User
{
    public interface IRoleRepository : IBaseRepository<Role, Guid>
    {
    }
}

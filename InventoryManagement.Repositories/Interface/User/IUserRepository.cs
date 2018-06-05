using System;
using InventoryManagement.Repositories.Interface.Infastructure;

namespace InventoryManagement.Repositories.Interface.User
{
    public interface IUserRepository : IBaseRepository<Models.Identity.User, Guid>
    {
    }
}

using System;
using InventoryManagement.Repositories.Infastructure;
using InventoryManagement.Repositories.Interface.Infastructure;
using InventoryManagement.Repositories.Interface.User;

namespace InventoryManagement.Repositories.User
{
    public class UserRepository : BaseRepository<Models.Identity.User, Guid>, IUserRepository
    {
        public UserRepository(IDatabaseContext context) : base(context)
        {
        }
    }
}

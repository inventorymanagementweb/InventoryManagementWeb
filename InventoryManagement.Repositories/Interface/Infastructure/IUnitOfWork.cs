using InventoryManagement.Repositories.Interface.User;

namespace InventoryManagement.Repositories.Interface.Infastructure
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
    }
}

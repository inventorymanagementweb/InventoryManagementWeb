using InventoryManagement.Repositories.Interface.Infastructure;
using InventoryManagement.Repositories.Interface.User;

namespace InventoryManagement.Repositories.Infastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseContext _context;

        public UnitOfWork(IDatabaseContext context, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _context = context;
            UserRepository = userRepository;
            RoleRepository = roleRepository;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public IUserRepository UserRepository { get; }
        public IRoleRepository RoleRepository { get; }
    }
}

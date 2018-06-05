using InventoryManagement.Repositories.Interface.Infastructure;
using log4net;

namespace InventoryManagement.Services.Infastructure
{
    public abstract class BaseService
    {
        protected BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public ILog Logger { get; set; }

        public IUnitOfWork UnitOfWork { get; set; }
    }
}

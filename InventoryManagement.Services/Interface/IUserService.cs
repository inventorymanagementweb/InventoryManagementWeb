using System;
using System.Threading.Tasks;
using InventoryManagement.Models.Identity;

namespace InventoryManagement.Services.Interface
{
    public interface IUserService
    {
        string GetFullNameById(Guid userId);
        Task CreateUser(User user);
        string HashPassword(string password);
    }
}

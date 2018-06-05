using System;
using System.Threading.Tasks;
using InventoryManagement.Models.Identity;
using InventoryManagement.Repositories.Interface.Infastructure;
using InventoryManagement.Services.Infastructure;
using InventoryManagement.Services.Interface;
using InventoryManagement.Services.Interface.Utility;
using Microsoft.AspNet.Identity;

namespace InventoryManagement.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPasswordGeneratorService _passwordGeneratorService;
        private readonly IEmailService _emailService;

        public UserService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IPasswordGeneratorService passwordGeneratorService, IEmailService emailService) : base(unitOfWork)
        {
            _passwordHasher = passwordHasher;
            _passwordGeneratorService = passwordGeneratorService;
            _emailService = emailService;
        }

        public string GetFullNameById(Guid userId)
        {
            try
            {
                Logger.Info($"UserService.GetFullNameById(Guid userId); userId: {userId}");

                var user = UnitOfWork.UserRepository.GetById(userId);

                if (user != null) return user.FullName;

                Logger.Error($"UnitOfWork.UserRepository.GetById(userId) returned null. Parameter: userId= {userId}");

                return string.Empty;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return string.Empty;
            }
        }
        
        public Task CreateUser(User user)
        {
            try
            {
                Logger.Info($"UserService.CreateUser(User user); user: {user}");

                var password = _passwordGeneratorService.Random();
                var paswordHashed = _passwordHasher.HashPassword(password);

                user.PasswordHash = paswordHashed;

                UnitOfWork.UserRepository.Insert(user);
                UnitOfWork.SaveChanges();

                _emailService.SendSingleEmail(user.Email, "Create new user.",
                    $"Your new account: {user.UserName}, password: {password}");

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return Task.FromException(ex);
            }
        }

        public string HashPassword(string password)
        {
            try
            {
                Logger.Info($"UserService.HashPassword(string password); password: {password}");

                var passwordHashed = _passwordHasher.HashPassword(password);

                return passwordHashed;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }
    }
}

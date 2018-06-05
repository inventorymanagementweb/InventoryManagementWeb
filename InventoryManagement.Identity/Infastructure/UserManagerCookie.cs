using System;
using InventoryManagement.Identity.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace InventoryManagement.Identity.Infastructure
{
    public class UserManagerCookie : UserManager<User, Guid>
    {
        public UserManagerCookie(IUserStore<User, Guid> store) : base(store)
        {
        }

        public static UserManagerCookie Create(IdentityFactoryOptions<UserManagerCookie> options, IOwinContext context)
        {
            var manager = new UserManagerCookie(new IdentityUserStore(context.Get<DbContext.IdentityDbContext>()));

            manager.UserValidator = new UserValidator<User, Guid>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = Commons.Constants.PasswordMinLenght,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(Commons.Constants.AccountLockoutTimespanMinutes);
            manager.MaxFailedAccessAttemptsBeforeLockout = Commons.Constants.MaxFailedAcessAttemptsBeforeLockout;

            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<User, Guid>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is: {0}"
            });

            manager.EmailService = new EmailService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = (new DataProtectorTokenProvider<User, Guid>(dataProtectionProvider.Create(Commons.Constants.DataProtectorTokenProviderPurposes))
                {
                    TokenLifespan = TimeSpan.FromMinutes(Commons.Constants.TokenLifespanMinutes)
                });
            }
            return manager;
        }
    }
}

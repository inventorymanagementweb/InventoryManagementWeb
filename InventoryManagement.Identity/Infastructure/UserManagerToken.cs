using System;
using InventoryManagement.Identity.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace InventoryManagement.Identity.Infastructure
{
    public class UserManagerToken : UserManager<User, Guid>
    {
        public UserManagerToken(IUserStore<User, Guid> store) : base(store)
        {
        }

        public static UserManagerToken Create(IdentityFactoryOptions<UserManagerToken> options, IOwinContext context)
        {
            var appDbContext = context.Get<DbContext.IdentityDbContext>();
            var appUserManager = new UserManagerToken(new IdentityUserStore(appDbContext));

            appUserManager.UserValidator = new UserValidator<User, Guid>(appUserManager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            appUserManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = Commons.Constants.PasswordMinLenght,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            appUserManager.EmailService = new EmailService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                appUserManager.UserTokenProvider = new DataProtectorTokenProvider<User, Guid>(dataProtectionProvider.Create(Commons.Constants.DataProtectorTokenProviderPurposes))
                {
                    TokenLifespan = TimeSpan.FromMinutes(Commons.Constants.TokenLifespanMinutes)
                };
            }

            return appUserManager;
        }
    }
}

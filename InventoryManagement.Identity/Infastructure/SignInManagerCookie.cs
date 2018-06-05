using System;
using System.Security.Claims;
using System.Threading.Tasks;
using InventoryManagement.Identity.Model;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace InventoryManagement.Identity.Infastructure
{
    public class SignInManagerCookie : SignInManager<User, Guid>
    {
        public SignInManagerCookie(UserManagerCookie userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((UserManagerCookie)UserManager);
        }

        public static SignInManagerCookie Create(IdentityFactoryOptions<SignInManagerCookie> options, IOwinContext context)
        {
            return new SignInManagerCookie(context.GetUserManager<UserManagerCookie>(), context.Authentication);
        }
    }
}

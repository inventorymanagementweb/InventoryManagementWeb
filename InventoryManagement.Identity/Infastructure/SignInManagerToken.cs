using System;
using System.Security.Claims;
using System.Threading.Tasks;
using InventoryManagement.Identity.Model;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace InventoryManagement.Identity.Infastructure
{
    public class SignInManagerToken : SignInManager<User, Guid>
    {
        public SignInManagerToken(UserManagerToken userManager, IAuthenticationManager authenticationManager): base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((UserManagerToken) UserManager);
        }

        public static SignInManagerToken Create(IdentityFactoryOptions<SignInManagerToken> options, IOwinContext context)
        {
            return new SignInManagerToken(context.GetUserManager<UserManagerToken>(), context.Authentication);
        }
    }
}

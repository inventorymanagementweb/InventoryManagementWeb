using System.Threading.Tasks;
using InventoryManagement.Identity.Infastructure;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace InventoryManagement.Identity.Provider
{
    public class CustomOAuthProviderToken : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            const string allowedOrigin = Commons.Constants.AllowedOrigin;

            context.OwinContext.Response.Headers.Add(Commons.Constants.AccessControlAllowOrgin, new[] { allowedOrigin });

            var userManager = context.OwinContext.GetUserManager<UserManagerToken>();

            var user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError(Commons.Constants.ErrInvalidGrant, Commons.Constants.UsernamePassIncorrect);
                return;
            }

            var oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, Commons.Constants.AuthenticationType);

            var ticket = new AuthenticationTicket(oAuthIdentity, null);

            context.Validated(ticket);
        }
    }
}

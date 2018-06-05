using System;
using InventoryManagement.Identity.DbContext;
using InventoryManagement.Identity.Infastructure;
using InventoryManagement.Identity.Model;
using InventoryManagement.Identity.Provider;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace InventoryManagement.Identity
{
    public class IdentityStartup
    {
        public void ConfigureCookieAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(IdentityDbContext.Create);
            app.CreatePerOwinContext<UserManagerCookie>(UserManagerCookie.Create);
            app.CreatePerOwinContext<SignInManagerCookie>(SignInManagerCookie.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString(Commons.Constants.CookieLoginPath),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<UserManagerCookie, User, Guid>(TimeSpan.FromMinutes(30), (manager, user) => user.GenerateUserIdentityAsync(manager), claim => Guid.Parse(claim.GetUserId()))
                }
            });
        }

        public void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            app.CreatePerOwinContext(IdentityDbContext.Create);
            app.CreatePerOwinContext<UserManagerToken>(UserManagerToken.Create);
            app.CreatePerOwinContext<RoleManagerToken>(RoleManagerToken.Create);

            var oAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString(Commons.Constants.TokenLoginPath),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(Commons.Constants.TokenLifespanMinutes),
                Provider = new CustomOAuthProviderToken(),
                AccessTokenFormat = new CustomJwtFormat(Uri.UriSchemeHttp)
            };

            app.UseOAuthAuthorizationServer(oAuthServerOptions);
        }

        public void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {
            var issuer = Uri.UriSchemeHttp;
            const string audienceId = Commons.Constants.ConfigurationAudienceId;
            var audienceSecret = TextEncodings.Base64Url.Decode(Commons.Constants.ConfigurationAudienceSecret);

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audienceId },
                    IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[]
                    {
                        new SymmetricKeyIssuerSecurityKeyProvider (issuer, audienceSecret)
                    }
                });
        }
    }
}

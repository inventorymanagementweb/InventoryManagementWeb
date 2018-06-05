using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using InventoryManagement.Identity.Infastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InventoryManagement.Identity.Model
{
    public class User : IdentityUser<Guid, UserLogin, UserRole, UserClaim>
    {
        [Required]
        [Index("UserNameIndex", IsUnique = true)]
        [MaxLength(128)]
        public override string UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Your email looks incorrect. Please check and try again.")]
        [Index("EmailIndex", IsUnique = true)]
        [MaxLength(128)]
        public override string Email { get; set; }

        [Required]
        [MaxLength(128)]
        public string FullName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManagerCookie manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManagerToken manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ExternalBearer);
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManagerToken manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }
}

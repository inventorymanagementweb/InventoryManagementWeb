using InventoryManagement.Identity.Model;

namespace InventoryManagement.Identity.Infastructure
{
    public class ModelFactory
    {
        private UserManagerToken UserManagerToken { get; }

        public ModelFactory(UserManagerToken userManager)
        {
            UserManagerToken = userManager;
        }

        public UserReturn Create(User appUser)
        {
            return new UserReturn
            {
                Id = appUser.Id,
                Email = appUser.UserName,
                EmailConfirmed = appUser.EmailConfirmed,
                Roles = UserManagerToken.GetRolesAsync(appUser.Id).Result,
                Claims = UserManagerToken.GetClaimsAsync(appUser.Id).Result
            };
        }

        public RoleReturn Create(Role appRole)
        {
            return new RoleReturn
            {
                Id = appRole.Id,
                Name = appRole.Name
            };
        }
    }
}

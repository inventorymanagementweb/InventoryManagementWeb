using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using InventoryManagement.Commons;
using InventoryManagement.Models.Identity;
using InventoryManagement.Repositories.Interface.Infastructure;

namespace InventoryManagement.Repositories.Infastructure
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext() : base(Constants.EntityFrameworkConnectionString)
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Models.Identity.User>().HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<Models.Identity.User>().HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<Models.Identity.User>().HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.UserId);

            modelBuilder.Entity<Role>().HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId }).ToTable("UserRoles", "User");

            modelBuilder.Entity<UserLogin>().HasKey(ul => new { ul.LoginProvider, ul.ProviderKey, ul.UserId }).ToTable("UserLogins", "User");

            modelBuilder.Entity<UserClaim>().HasKey(uc => uc.Id).ToTable("UserClaims", "User");
        }

        public DbSet<Models.Identity.User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}

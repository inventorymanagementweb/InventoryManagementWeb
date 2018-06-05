using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagement.Models.Interface;

namespace InventoryManagement.Models.Identity
{
    public class User : IModel<Guid>
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Index("UserNameIndex", IsUnique = true)]
        [StringLength(128)]
        public string UserName { get; set; }

        [Required]
        [Index("EmailIndex", IsUnique = true)]
        [StringLength(128)]
        public string Email { get; set; }

        [Required]
        [StringLength(128)]
        public string FullName { get; set; }
        
        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }
        
        public virtual ICollection<UserClaim> Claims { get; set; }

        public virtual ICollection<UserLogin> Logins { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }
    }
}

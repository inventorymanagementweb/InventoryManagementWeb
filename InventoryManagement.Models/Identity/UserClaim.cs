using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models.Identity
{
    [Table("User.UserClaims")]
    public class UserClaim
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public virtual User User { get; set; }
    }
}

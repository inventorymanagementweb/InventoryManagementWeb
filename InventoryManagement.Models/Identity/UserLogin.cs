using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models.Identity
{
    [Table("User.UserLogins")]
    public class UserLogin
    {
        [Key]
        [Column(Order = 0)]
        public string LoginProvider { get; set; }

        [Key]
        [Column(Order = 1)]
        public string ProviderKey { get; set; }

        [Key, ForeignKey("User")]
        [Column(Order = 2)]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InventoryManagement.Models.Interface;

namespace InventoryManagement.Models.Identity
{
    public class Role : IModel<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserRole> Users { get; set; }
    }
}

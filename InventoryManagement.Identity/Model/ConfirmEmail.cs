using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Identity.Model
{
    public class ConfirmEmail
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Code { get; set; }
    }
}

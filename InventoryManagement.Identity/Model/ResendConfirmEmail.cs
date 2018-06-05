using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Identity.Model
{
    public class ResendConfirmEmail
    {
        [Required]
        [EmailAddress(ErrorMessage = "Your email looks incorrect. Please check and try again.")]
        [Display(Name = "Email")]
        [MaxLength(128)]
        public string Email { get; set; }
    }
}

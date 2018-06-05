using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Identity.Model
{
    public class ResetPassword
    {
        [Required]
        [EmailAddress(ErrorMessage = "Your email looks incorrect. Please check and try again.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}

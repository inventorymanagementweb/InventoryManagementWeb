using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Identity.Model
{
    public class LogIn
    {
        [Required]
        [Display(Name = "Username")]
        [MaxLength(128)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MaxLength(128)]
        public string Password { get; set; }
    }
}

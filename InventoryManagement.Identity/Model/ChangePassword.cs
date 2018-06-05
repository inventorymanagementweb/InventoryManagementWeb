using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Identity.Model
{
    public class ChangePassword
    {
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        [MaxLength(128)]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,100})", ErrorMessage = "Password must contain: minimum 6 characters, upper case letter and numberic value")]
        public string OldPassword { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        [MaxLength(128)]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,100})", ErrorMessage = "Password must contain: minimum 6 characters, upper case letter and numberic value")]
        public string NewPassword { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [MaxLength(128)]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,100})", ErrorMessage = "Password must contain: minimum 6 characters, upper case letter and numberic value")]
        public string ConfirmNewPassword { get; set; }
    }
}

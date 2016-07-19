using System.ComponentModel.DataAnnotations;

namespace MemomMvc52.Areas.UserAccount.Models
{
    public class RegisterInputModel
    {
        [ScaffoldColumn(false)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(150, ErrorMessage="String length should be <= 150 characters")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "String length should be <= 150 characters")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Display Name should be <= 50 characters")]
        public string DisplayName { get; set; }
        

        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength=6)]
        public string Password { get; set; }
        
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage="Password confirmation must match password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
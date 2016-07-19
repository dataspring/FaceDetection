using System.ComponentModel.DataAnnotations;

namespace WdaMvc52.Areas.UserAccount.Models
{
    public class ChangeUsernameInputModel
    {
        [Required]
        public string NewUsername { get; set; }
    }
}
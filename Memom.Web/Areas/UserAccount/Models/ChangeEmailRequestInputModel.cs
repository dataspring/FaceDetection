using System.ComponentModel.DataAnnotations;

namespace WdaMvc52.Areas.UserAccount.Models
{
    public class ChangeEmailRequestInputModel
    {
        //[Required]
        [EmailAddress]
        public string NewEmail { get; set; }
    }
}
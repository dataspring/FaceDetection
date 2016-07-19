using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace MemomMvc52.Areas.UserAccount.Models
{
    public class PasswordResetInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

}
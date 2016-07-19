using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MemomMvc52.Areas.UserApp.Models
{
    [Serializable]
    [DataContract(IsReference=true)]
    [Bind(Exclude = "Key")]
    public partial class UserAccountInput : Entity
    {

		[Required]
        public int UserKey { get; set; }
		[Required]
        [DataType(DataType.Text)]
        public string Username { get; set; }
		[Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
		[Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
		[Required]
        [DataType(DataType.Text)]
        public string DisplayName { get; set; }
        [Range(1,150)]
        public Nullable<int> Age { get; set; }
		[Required]
        public bool IsAccountClosed { get; set; }
		[Required]
        public bool IsLoginAllowed { get; set; }
		[Required]
        public bool RequiresPasswordReset { get; set; }
		[Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public bool IsAccountVerified { get; set; }

    }

    [Serializable]
    [DataContract(IsReference=true)]
    [Bind(Exclude = "Key")]
    public partial class UserAccountDisplay : Entity
    {

        [Required]
        public int UserKey { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]
        public System.DateTime Created { get; set; }
        [Required]
        public System.DateTime LastUpdated { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string DisplayName { get; set; }

        [Required]
        public bool IsAccountClosed { get; set; }
        [Required]
        public Nullable<System.DateTime> AccountClosed { get; set; }
        [Required]
        public bool IsLoginAllowed { get; set; }
        [Required]
        public Nullable<System.DateTime> LastLogin { get; set; }
        [Required]
        public Nullable<System.DateTime> LastFailedLogin { get; set; }
        [Required]
        public int FailedLoginCount { get; set; }
        [Required]
        public Nullable<System.DateTime> PasswordChanged { get; set; }
        [Required]
        public bool RequiresPasswordReset { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public bool IsAccountVerified { get; set; }
        [Required]
        public Nullable<System.DateTime> LastFailedPasswordReset { get; set; }
        [Required]
        public int FailedPasswordResetCount { get; set; }
    }
}

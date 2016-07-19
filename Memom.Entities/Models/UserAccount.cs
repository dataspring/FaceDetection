using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Memom.Entities.Models
{
	[Serializable]
	[DataContract]
    public partial class UserAccount : Entity
    {
        public UserAccount()
        {
            this.Albums = new List<Album>();
            this.Members = new List<Member>();
            this.UserGroups = new List<UserGroup>();
        }

		[DataMember]
        public int Key { get; set; }
		[DataMember]
        public System.Guid ID { get; set; }
		[DataMember]
        public string Tenant { get; set; }
		[DataMember]
        public string Username { get; set; }
		[DataMember]
        public System.DateTime Created { get; set; }
		[DataMember]
        public System.DateTime LastUpdated { get; set; }
		[DataMember]
        public string FirstName { get; set; }
		[DataMember]
        public string LastName { get; set; }
		[DataMember]
        public string DisplayName { get; set; }
		[DataMember]
        public Nullable<int> Age { get; set; }
		[DataMember]
        public bool IsAccountClosed { get; set; }
		[DataMember]
        public Nullable<System.DateTime> AccountClosed { get; set; }
		[DataMember]
        public bool IsLoginAllowed { get; set; }
		[DataMember]
        public Nullable<System.DateTime> LastLogin { get; set; }
		[DataMember]
        public Nullable<System.DateTime> LastFailedLogin { get; set; }
		[DataMember]
        public int FailedLoginCount { get; set; }
		[DataMember]
        public Nullable<System.DateTime> PasswordChanged { get; set; }
		[DataMember]
        public bool RequiresPasswordReset { get; set; }
		[DataMember]
        public string Email { get; set; }
		[DataMember]
        public bool IsAccountVerified { get; set; }
		[DataMember]
        public Nullable<System.DateTime> LastFailedPasswordReset { get; set; }
		[DataMember]
        public int FailedPasswordResetCount { get; set; }
		[DataMember]
        public string MobileCode { get; set; }
		[DataMember]
        public Nullable<System.DateTime> MobileCodeSent { get; set; }
		[DataMember]
        public string MobilePhoneNumber { get; set; }
		[DataMember]
        public Nullable<System.DateTime> MobilePhoneNumberChanged { get; set; }
		[DataMember]
        public int AccountTwoFactorAuthMode { get; set; }
		[DataMember]
        public int CurrentTwoFactorAuthStatus { get; set; }
		[DataMember]
        public string VerificationKey { get; set; }
		[DataMember]
        public Nullable<int> VerificationPurpose { get; set; }
		[DataMember]
        public Nullable<System.DateTime> VerificationKeySent { get; set; }
		[DataMember]
        public string VerificationStorage { get; set; }
		[DataMember]
        public string HashedPassword { get; set; }
		[DataMember]
        public byte[] HashedPasswordBinary { get; set; }
		[DataMember]
        public virtual ICollection<Album> Albums { get; set; }
		[DataMember]
        public virtual ICollection<Member> Members { get; set; }
		[DataMember]
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}

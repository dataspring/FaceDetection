using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Memom.Entities.Models.Mapping
{
    public class UserAccountMap : EntityTypeConfiguration<UserAccount>
    {
        public UserAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.Key);

            // Properties
            this.Property(t => t.Tenant)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(254);

            this.Property(t => t.FirstName)
                .HasMaxLength(254);

            this.Property(t => t.LastName)
                .HasMaxLength(254);

            this.Property(t => t.DisplayName)
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(254);

            this.Property(t => t.MobileCode)
                .HasMaxLength(100);

            this.Property(t => t.MobilePhoneNumber)
                .HasMaxLength(20);

            this.Property(t => t.VerificationKey)
                .HasMaxLength(100);

            this.Property(t => t.VerificationStorage)
                .HasMaxLength(100);

            this.Property(t => t.HashedPassword)
                .HasMaxLength(200);

            this.Property(t => t.HashedPasswordBinary)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("UserAccounts");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Tenant).HasColumnName("Tenant");
            this.Property(t => t.Username).HasColumnName("Username");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.LastUpdated).HasColumnName("LastUpdated");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
            this.Property(t => t.Age).HasColumnName("Age");
            this.Property(t => t.IsAccountClosed).HasColumnName("IsAccountClosed");
            this.Property(t => t.AccountClosed).HasColumnName("AccountClosed");
            this.Property(t => t.IsLoginAllowed).HasColumnName("IsLoginAllowed");
            this.Property(t => t.LastLogin).HasColumnName("LastLogin");
            this.Property(t => t.LastFailedLogin).HasColumnName("LastFailedLogin");
            this.Property(t => t.FailedLoginCount).HasColumnName("FailedLoginCount");
            this.Property(t => t.PasswordChanged).HasColumnName("PasswordChanged");
            this.Property(t => t.RequiresPasswordReset).HasColumnName("RequiresPasswordReset");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.IsAccountVerified).HasColumnName("IsAccountVerified");
            this.Property(t => t.LastFailedPasswordReset).HasColumnName("LastFailedPasswordReset");
            this.Property(t => t.FailedPasswordResetCount).HasColumnName("FailedPasswordResetCount");
            this.Property(t => t.MobileCode).HasColumnName("MobileCode");
            this.Property(t => t.MobileCodeSent).HasColumnName("MobileCodeSent");
            this.Property(t => t.MobilePhoneNumber).HasColumnName("MobilePhoneNumber");
            this.Property(t => t.MobilePhoneNumberChanged).HasColumnName("MobilePhoneNumberChanged");
            this.Property(t => t.AccountTwoFactorAuthMode).HasColumnName("AccountTwoFactorAuthMode");
            this.Property(t => t.CurrentTwoFactorAuthStatus).HasColumnName("CurrentTwoFactorAuthStatus");
            this.Property(t => t.VerificationKey).HasColumnName("VerificationKey");
            this.Property(t => t.VerificationPurpose).HasColumnName("VerificationPurpose");
            this.Property(t => t.VerificationKeySent).HasColumnName("VerificationKeySent");
            this.Property(t => t.VerificationStorage).HasColumnName("VerificationStorage");
            this.Property(t => t.HashedPassword).HasColumnName("HashedPassword");
            this.Property(t => t.HashedPasswordBinary).HasColumnName("HashedPasswordBinary");
        }
    }
}

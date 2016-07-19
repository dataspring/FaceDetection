using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Memom.Entities.Models.Mapping
{
    public class UserPasswordChangeMap : EntityTypeConfiguration<UserPasswordChange>
    {
        public UserPasswordChangeMap()
        {
            // Primary Key
            this.HasKey(t => t.Key);

            // Properties
            this.Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.OldHashedPasswordBinary)
                .HasMaxLength(200);

            this.Property(t => t.Remarks)
                .HasMaxLength(255);

            this.Property(t => t.IPAddress)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UserPasswordChanges");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.UserKey).HasColumnName("UserKey");
            this.Property(t => t.Username).HasColumnName("Username");
            this.Property(t => t.OldHashedPasswordBinary).HasColumnName("OldHashedPasswordBinary");
            this.Property(t => t.AttemptDate).HasColumnName("AttemptDate");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
        }
    }
}

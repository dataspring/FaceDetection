using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Memom.Entities.Models.Mapping
{
    public class UserLogInAttemptMap : EntityTypeConfiguration<UserLogInAttempt>
    {
        public UserLogInAttemptMap()
        {
            // Primary Key
            this.HasKey(t => t.Key);

            // Properties
            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Result)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IPAddress)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Remarks)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("UserLogInAttempts");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.UserKey).HasColumnName("UserKey");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.AttemptDate).HasColumnName("AttemptDate");
            this.Property(t => t.Result).HasColumnName("Result");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
        }
    }
}

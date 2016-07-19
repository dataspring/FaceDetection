using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Memom.Entities.Models.Mapping
{
    public class UserGroupMap : EntityTypeConfiguration<UserGroup>
    {
        public UserGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.Key);

            // Properties
            // Table & Column Mappings
            this.ToTable("UserGroups");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.GroupKey).HasColumnName("GroupKey");
            this.Property(t => t.UserAccountKey).HasColumnName("UserAccountKey");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.LastUpdated).HasColumnName("LastUpdated");

            // Relationships
            this.HasRequired(t => t.UserAccount)
                .WithMany(t => t.UserGroups)
                .HasForeignKey(d => d.UserAccountKey);

        }
    }
}

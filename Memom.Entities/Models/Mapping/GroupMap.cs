using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Memom.Entities.Models.Mapping
{
    public class GroupMap : EntityTypeConfiguration<Group>
    {
        public GroupMap()
        {
            // Primary Key
            this.HasKey(t => t.Key);

            // Properties
            this.Property(t => t.Tenant)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Description)
                .HasMaxLength(254);

            // Table & Column Mappings
            this.ToTable("Groups");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Tenant).HasColumnName("Tenant");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.LastUpdated).HasColumnName("LastUpdated");
        }
    }
}

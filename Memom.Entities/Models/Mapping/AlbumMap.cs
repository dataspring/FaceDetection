using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Memom.Entities.Models.Mapping
{
    public class AlbumMap : EntityTypeConfiguration<Album>
    {
        public AlbumMap()
        {
            // Primary Key
            this.HasKey(t => t.Key);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(255);

            this.Property(t => t.SetupEmail)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Albums");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.UserKey).HasColumnName("UserKey");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsAttached).HasColumnName("IsAttached");
            this.Property(t => t.DisplayOrder).HasColumnName("DisplayOrder");
            this.Property(t => t.SetupEmail).HasColumnName("SetupEmail");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.LastUpdated).HasColumnName("LastUpdated");

            // Relationships
            this.HasRequired(t => t.UserAccount)
                .WithMany(t => t.Albums)
                .HasForeignKey(d => d.UserKey);

        }
    }
}

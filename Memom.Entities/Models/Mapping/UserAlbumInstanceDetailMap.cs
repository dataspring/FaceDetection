using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Memom.Entities.Models.Mapping
{
    public class UserAlbumInstanceDetailMap : EntityTypeConfiguration<UserAlbumInstanceDetail>
    {
        public UserAlbumInstanceDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Key);

            // Properties
            this.Property(t => t.FaceImage)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.OpenCVMethod)
                .HasMaxLength(255);

            this.Property(t => t.FaceMatchFile)
                .HasMaxLength(255);

            this.Property(t => t.FolderPath)
                .HasMaxLength(255);

            this.Property(t => t.AbsolutePath)
                .HasMaxLength(255);

            this.Property(t => t.SetActiveRemarks)
                .HasMaxLength(255);

            this.Property(t => t.Remarks)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("UserAlbumInstanceDetails");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.UserAlbumInstanceKey).HasColumnName("UserAlbumInstanceKey");
            this.Property(t => t.MemberKey).HasColumnName("MemberKey");
            this.Property(t => t.FaceImage).HasColumnName("FaceImage");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.Processed).HasColumnName("Processed");
            this.Property(t => t.ProcessedOn).HasColumnName("ProcessedOn");
            this.Property(t => t.Width).HasColumnName("Width");
            this.Property(t => t.Height).HasColumnName("Height");
            this.Property(t => t.Xpos).HasColumnName("Xpos");
            this.Property(t => t.Ypos).HasColumnName("Ypos");
            this.Property(t => t.FaceFound).HasColumnName("FaceFound");
            this.Property(t => t.Inliers).HasColumnName("Inliers");
            this.Property(t => t.OpenCVMethod).HasColumnName("OpenCVMethod");
            this.Property(t => t.FaceMatchFile).HasColumnName("FaceMatchFile");
            this.Property(t => t.FolderPath).HasColumnName("FolderPath");
            this.Property(t => t.AbsolutePath).HasColumnName("AbsolutePath");
            this.Property(t => t.SetActiveRemarks).HasColumnName("SetActiveRemarks");
            this.Property(t => t.SearchTerms).HasColumnName("SearchTerms");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.LastUpdated).HasColumnName("LastUpdated");

            // Relationships
            this.HasRequired(t => t.UserAlbumInstance)
                .WithMany(t => t.UserAlbumInstanceDetails)
                .HasForeignKey(d => d.UserAlbumInstanceKey);

        }
    }
}

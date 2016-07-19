using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Memom.Entities.Models.Mapping
{
    public class UserAlbumInstanceMap : EntityTypeConfiguration<UserAlbumInstance>
    {
        public UserAlbumInstanceMap()
        {
            // Primary Key
            this.HasKey(t => t.Key);

            // Properties
            this.Property(t => t.PhotoFile)
                .IsRequired()
                .HasMaxLength(225);

            this.Property(t => t.OriginalFile)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.FolderPath)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.AbsolutePath)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.ThumbnailFile)
                .HasMaxLength(255);

            this.Property(t => t.SmallPhotoFile)
                .HasMaxLength(255);

            this.Property(t => t.MediumPhotoFile)
                .HasMaxLength(255);

            this.Property(t => t.LargePhotoFile)
                .HasMaxLength(255);

            this.Property(t => t.FileUploadStatus)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Resolution)
                .HasMaxLength(225);

            this.Property(t => t.ImageType)
                .HasMaxLength(225);

            this.Property(t => t.IpAddress)
                .HasMaxLength(50);

            this.Property(t => t.GpsCoordinates)
                .HasMaxLength(225);

            // Table & Column Mappings
            this.ToTable("UserAlbumInstances");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.AlbumKey).HasColumnName("AlbumKey");
            this.Property(t => t.PhotoId).HasColumnName("PhotoId");
            this.Property(t => t.PhotoFile).HasColumnName("PhotoFile");
            this.Property(t => t.OriginalFile).HasColumnName("OriginalFile");
            this.Property(t => t.FolderPath).HasColumnName("FolderPath");
            this.Property(t => t.AbsolutePath).HasColumnName("AbsolutePath");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.ThumbnailFile).HasColumnName("ThumbnailFile");
            this.Property(t => t.SmallPhotoFile).HasColumnName("SmallPhotoFile");
            this.Property(t => t.MediumPhotoFile).HasColumnName("MediumPhotoFile");
            this.Property(t => t.LargePhotoFile).HasColumnName("LargePhotoFile");
            this.Property(t => t.AnyFacesTagged).HasColumnName("AnyFacesTagged");
            this.Property(t => t.PhotosSized).HasColumnName("PhotosSized");
            this.Property(t => t.FacesDetected).HasColumnName("FacesDetected");
            this.Property(t => t.FileUploadStatus).HasColumnName("FileUploadStatus");
            this.Property(t => t.ShootDate).HasColumnName("ShootDate");
            this.Property(t => t.Resolution).HasColumnName("Resolution");
            this.Property(t => t.ImageType).HasColumnName("ImageType");
            this.Property(t => t.IpAddress).HasColumnName("IpAddress");
            this.Property(t => t.GpsCoordinates).HasColumnName("GpsCoordinates");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.LastUpdated).HasColumnName("LastUpdated");
        }
    }
}

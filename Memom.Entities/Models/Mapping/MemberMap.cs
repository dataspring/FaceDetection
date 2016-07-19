using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Memom.Entities.Models.Mapping
{
    public class MemberMap : EntityTypeConfiguration<Member>
    {
        public MemberMap()
        {
            // Primary Key
            this.HasKey(t => t.Key);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(255);

            this.Property(t => t.DisplayName)
                .HasMaxLength(25);

            this.Property(t => t.Sex)
                .HasMaxLength(15);

            this.Property(t => t.Relation)
                .HasMaxLength(50);

            this.Property(t => t.FaceImage)
                .HasMaxLength(255);

            this.Property(t => t.DetectedFaces)
                .HasMaxLength(255);

            this.Property(t => t.DetectedFaceImage)
                .HasMaxLength(255);

            this.Property(t => t.UnDetectedFaceImage)
                .HasMaxLength(255);

            this.Property(t => t.FolderPath)
                .HasMaxLength(255);

            this.Property(t => t.AbsoultePath)
                .HasMaxLength(255);

            this.Property(t => t.OriginalFaceFileName)
                .HasMaxLength(255);

            this.Property(t => t.FaceDetectionRemarks)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Members");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.UserKey).HasColumnName("UserKey");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
            this.Property(t => t.DateOfBirth).HasColumnName("DateOfBirth");
            this.Property(t => t.Sex).HasColumnName("Sex");
            this.Property(t => t.Relation).HasColumnName("Relation");
            this.Property(t => t.FaceImage).HasColumnName("FaceImage");
            this.Property(t => t.IsFaceDetected).HasColumnName("IsFaceDetected");
            this.Property(t => t.IsFaceTagged).HasColumnName("IsFaceTagged");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.DetectedFaceCount).HasColumnName("DetectedFaceCount");
            this.Property(t => t.DetectedFaces).HasColumnName("DetectedFaces");
            this.Property(t => t.DetectedFaceImage).HasColumnName("DetectedFaceImage");
            this.Property(t => t.UnDetectedFaceImage).HasColumnName("UnDetectedFaceImage");
            this.Property(t => t.FolderPath).HasColumnName("FolderPath");
            this.Property(t => t.AbsoultePath).HasColumnName("AbsoultePath");
            this.Property(t => t.AllDetectedFaceImages).HasColumnName("AllDetectedFaceImages");
            this.Property(t => t.OriginalFaceFileName).HasColumnName("OriginalFaceFileName");
            this.Property(t => t.FaceDetectedDate).HasColumnName("FaceDetectedDate");
            this.Property(t => t.FaceDetectionRemarks).HasColumnName("FaceDetectionRemarks");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.LastUpdated).HasColumnName("LastUpdated");

            // Relationships
            this.HasRequired(t => t.UserAccount)
                .WithMany(t => t.Members)
                .HasForeignKey(d => d.UserKey);

        }
    }
}

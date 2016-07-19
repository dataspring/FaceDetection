using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memom.Entities.Models
{

    public partial class Scores
    {
        public long FaceKey { get; set; }
        public int MemberKey { get; set; }
        public int UserKey { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string Relation { get; set; }
        public string FaceImage { get; set; }
        public Nullable<int> IsFaceDetected { get; set; }
        public Nullable<int> IsFaceTagged { get; set; }
        public Nullable<int> DetectedFaceCount { get; set; }
        public string DetectedFaceImage { get; set; }
        public Nullable<int> TotalFaceTags { get; set; }

    }

    public partial class AlbumScores
    {
        public int Key { get; set; }
        public int UserKey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAttached { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string SetupEmail { get; set; }
        public string FolderPath { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
        public string RandomDisplayPhoto { get; set; }
        public string MemberTagDetails { get; set; }
        public int TotalPhotos { get; set; }
        public int TotalTagged { get; set; }

    }


    public partial class MemberDetails
    {
        public int Key { get; set; }
        public int UserKey { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string Relation { get; set; }
        public string FaceImage { get; set; }
        public Nullable<bool> IsFaceDetected { get; set; }
        public bool IsFaceTagged { get; set; }
        public bool IsActive { get; set; }
        public int DetectedFaceCount { get; set; }
        public string DetectedFaces { get; set; }
        public string DetectedFaceImage { get; set; }
        public string UnDetectedFaceImage { get; set; }
        public string FolderPath { get; set; }
        public string AllDetectedFaceImages { get; set; }
        public string OriginalFaceFileName { get; set; }
        public Nullable<System.DateTime> FaceDetectedDate { get; set; }
        public string FaceDetectionRemarks { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
        public int TotalFaceTags { get; set; }
        public int AlbumTags { get; set; }
        public int Age { get; set; }

    }


    public partial class UpdateAlbumDownloadResult
    {
        public int IsAlbumDownloadUpdated { get; set; }
        public string Remarks { get; set; }
    }


}

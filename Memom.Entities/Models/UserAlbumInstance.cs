using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Memom.Entities.Models
{
	[Serializable]
	[DataContract]
    public partial class UserAlbumInstance : Entity
    {
        public UserAlbumInstance()
        {
            this.UserAlbumInstanceDetails = new List<UserAlbumInstanceDetail>();
        }

		[DataMember]
        public int Key { get; set; }
		[DataMember]
        public int AlbumKey { get; set; }
		[DataMember]
        public System.Guid PhotoId { get; set; }
		[DataMember]
        public string PhotoFile { get; set; }
		[DataMember]
        public string OriginalFile { get; set; }
		[DataMember]
        public string FolderPath { get; set; }
		[DataMember]
        public string AbsolutePath { get; set; }
		[DataMember]
        public bool IsActive { get; set; }
		[DataMember]
        public string ThumbnailFile { get; set; }
		[DataMember]
        public string SmallPhotoFile { get; set; }
		[DataMember]
        public string MediumPhotoFile { get; set; }
		[DataMember]
        public string LargePhotoFile { get; set; }
		[DataMember]
        public bool AnyFacesTagged { get; set; }
		[DataMember]
        public bool PhotosSized { get; set; }
		[DataMember]
        public int FacesDetected { get; set; }
		[DataMember]
        public string FileUploadStatus { get; set; }
		[DataMember]
        public Nullable<System.DateTime> ShootDate { get; set; }
		[DataMember]
        public string Resolution { get; set; }
		[DataMember]
        public string ImageType { get; set; }
		[DataMember]
        public string IpAddress { get; set; }
		[DataMember]
        public string GpsCoordinates { get; set; }
		[DataMember]
        public string Remarks { get; set; }
		[DataMember]
        public System.DateTime Created { get; set; }
		[DataMember]
        public Nullable<System.DateTime> LastUpdated { get; set; }
		[DataMember]
        public virtual ICollection<UserAlbumInstanceDetail> UserAlbumInstanceDetails { get; set; }
    }
}

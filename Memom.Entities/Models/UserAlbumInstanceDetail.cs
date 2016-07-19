using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Memom.Entities.Models
{
	[Serializable]
	[DataContract]
    public partial class UserAlbumInstanceDetail : Entity
    {
		[DataMember]
        public int Key { get; set; }
		[DataMember]
        public int UserAlbumInstanceKey { get; set; }
		[DataMember]
        public int MemberKey { get; set; }
		[DataMember]
        public string FaceImage { get; set; }
		[DataMember]
        public bool Active { get; set; }
		[DataMember]
        public bool Processed { get; set; }
		[DataMember]
        public Nullable<System.DateTime> ProcessedOn { get; set; }
		[DataMember]
        public Nullable<int> Width { get; set; }
		[DataMember]
        public Nullable<int> Height { get; set; }
		[DataMember]
        public Nullable<int> Xpos { get; set; }
		[DataMember]
        public Nullable<int> Ypos { get; set; }
		[DataMember]
        public bool FaceFound { get; set; }
		[DataMember]
        public Nullable<int> Inliers { get; set; }
		[DataMember]
        public string OpenCVMethod { get; set; }
		[DataMember]
        public string FaceMatchFile { get; set; }
		[DataMember]
        public string FolderPath { get; set; }
		[DataMember]
        public string AbsolutePath { get; set; }
		[DataMember]
        public string SetActiveRemarks { get; set; }
		[DataMember]
        public string SearchTerms { get; set; }
		[DataMember]
        public string Remarks { get; set; }
		[DataMember]
        public System.DateTime Created { get; set; }
		[DataMember]
        public Nullable<System.DateTime> LastUpdated { get; set; }
		[DataMember]
        public virtual UserAlbumInstance UserAlbumInstance { get; set; }
    }
}

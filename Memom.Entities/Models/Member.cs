using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Memom.Entities.Models
{
	[Serializable]
	[DataContract]
    public partial class Member : Entity
    {
		[DataMember]
        public int Key { get; set; }
		[DataMember]
        public int UserKey { get; set; }
		[DataMember]
        public string Name { get; set; }
		[DataMember]
        public string DisplayName { get; set; }
		[DataMember]
        public System.DateTime DateOfBirth { get; set; }
		[DataMember]
        public string Sex { get; set; }
		[DataMember]
        public string Relation { get; set; }
		[DataMember]
        public string FaceImage { get; set; }
		[DataMember]
        public Nullable<bool> IsFaceDetected { get; set; }
		[DataMember]
        public bool IsFaceTagged { get; set; }
		[DataMember]
        public Nullable<bool> IsActive { get; set; }
		[DataMember]
        public int DetectedFaceCount { get; set; }
		[DataMember]
        public string DetectedFaces { get; set; }
		[DataMember]
        public string DetectedFaceImage { get; set; }
		[DataMember]
        public string UnDetectedFaceImage { get; set; }
		[DataMember]
        public string FolderPath { get; set; }
		[DataMember]
        public string AbsoultePath { get; set; }
		[DataMember]
        public string AllDetectedFaceImages { get; set; }
		[DataMember]
        public string OriginalFaceFileName { get; set; }
		[DataMember]
        public Nullable<System.DateTime> FaceDetectedDate { get; set; }
		[DataMember]
        public string FaceDetectionRemarks { get; set; }
		[DataMember]
        public System.DateTime Created { get; set; }
		[DataMember]
        public Nullable<System.DateTime> LastUpdated { get; set; }
		[DataMember]
        public virtual UserAccount UserAccount { get; set; }
    }
}

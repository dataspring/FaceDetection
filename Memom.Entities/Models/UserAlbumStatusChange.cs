using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Memom.Entities.Models
{
	[Serializable]
	[DataContract]
    public partial class UserAlbumStatusChange : Entity
    {
		[DataMember]
        public int Key { get; set; }
		[DataMember]
        public int UserKey { get; set; }
		[DataMember]
        public string Email { get; set; }
		[DataMember]
        public int AlbumKey { get; set; }
		[DataMember]
        public string AlbumName { get; set; }
		[DataMember]
        public System.DateTime AttemptDate { get; set; }
		[DataMember]
        public string Result { get; set; }
		[DataMember]
        public string IPAddress { get; set; }
		[DataMember]
        public string Remarks { get; set; }
    }
}

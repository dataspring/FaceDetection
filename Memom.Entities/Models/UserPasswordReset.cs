using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Memom.Entities.Models
{
	[Serializable]
	[DataContract]
    public partial class UserPasswordReset : Entity
    {
		[DataMember]
        public int Key { get; set; }
		[DataMember]
        public Nullable<int> UserKey { get; set; }
		[DataMember]
        public string Username { get; set; }
		[DataMember]
        public byte[] OldHashedPasswordBinary { get; set; }
		[DataMember]
        public System.DateTime AttemptDate { get; set; }
		[DataMember]
        public string Remarks { get; set; }
		[DataMember]
        public string IPAddress { get; set; }
    }
}

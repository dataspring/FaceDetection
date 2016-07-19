using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Memom.Entities.Models
{
	[Serializable]
	[DataContract]
    public partial class UserGroup : Entity
    {
		[DataMember]
        public int Key { get; set; }
		[DataMember]
        public int GroupKey { get; set; }
		[DataMember]
        public int UserAccountKey { get; set; }
		[DataMember]
        public System.DateTime Created { get; set; }
		[DataMember]
        public System.DateTime LastUpdated { get; set; }
		[DataMember]
        public virtual UserAccount UserAccount { get; set; }
    }
}

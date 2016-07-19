using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Memom.Entities.Models
{
	[Serializable]
	[DataContract]
    public partial class Album : Entity
    {
		[DataMember]
        public int Key { get; set; }
		[DataMember]
        public int UserKey { get; set; }
		[DataMember]
        public string Name { get; set; }
		[DataMember]
        public string Description { get; set; }
		[DataMember]
        public bool IsAttached { get; set; }
		[DataMember]
        public Nullable<int> DisplayOrder { get; set; }
		[DataMember]
        public string SetupEmail { get; set; }
		[DataMember]
        public string Remarks { get; set; }
		[DataMember]
        public Nullable<System.DateTime> Created { get; set; }
		[DataMember]
        public Nullable<System.DateTime> LastUpdated { get; set; }
		[DataMember]
        public virtual UserAccount UserAccount { get; set; }
    }
}

using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Memom.Entities.Models
{
	[Serializable]
	[DataContract]
    public partial class Group : Entity
    {
		[DataMember]
        public int Key { get; set; }
		[DataMember]
        public System.Guid ID { get; set; }
		[DataMember]
        public string Tenant { get; set; }
		[DataMember]
        public string Name { get; set; }
		[DataMember]
        public string Description { get; set; }
		[DataMember]
        public System.DateTime Created { get; set; }
		[DataMember]
        public System.DateTime LastUpdated { get; set; }
    }
}

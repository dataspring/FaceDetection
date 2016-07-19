using System.ComponentModel.DataAnnotations.Schema;
using Repository.Pattern.Infrastructure;
using System.Runtime.Serialization;
using System;

namespace Repository.Pattern.Ef6
{
    [Serializable]
    [DataContract(IsReference=true)]
    public abstract class Entity : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
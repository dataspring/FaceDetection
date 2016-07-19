using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MemomMvc52.Areas.UserApp.Models
{
    [Serializable]
    [DataContract(IsReference=true)]
    [Bind(Exclude = "Key")]
    public partial class UserSearch : Entity
    {
        public bool SearchByEmail { get; set; }

        public bool SearchByName { get; set; }

        [DataType(DataType.Text)]
        public string SearchBy { get; set; }
        
        [DataType(DataType.Text)]
        public string SearchOn { get; set; }
    }
}
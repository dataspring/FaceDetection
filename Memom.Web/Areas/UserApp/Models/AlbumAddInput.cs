using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MemomMvc52.Areas.UserApp.Models
{
    [Serializable]
    [DataContract(IsReference = true)]
    [Bind(Exclude = "Key")]
    public partial class AlbumAddInput 
    {

        [ScaffoldColumn(true)]
        [DataType(DataType.Text)]
        public string UserKey { get; set; }

        [ScaffoldColumn(true)]
        [DataType(DataType.Text)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string DisplayOrder { get; set; }

    }
}
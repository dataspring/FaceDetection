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
    public partial class AlbumInput : Entity
    {
        public AlbumInput()
        {

        }

        [ScaffoldColumn(false)]
        public int AlbumKey { get; set; }

        [Required]
        public int UserKey { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        public bool IsPlayable { get; set; }

        [Range(1, 20, ErrorMessage="Please enter a valid integer number")]
        public Nullable<int> DisplayOrder { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Level1Name { get; set; }
       
        public string Level1Description { get; set; }
       
                [Required]
        [DataType(DataType.Text)]
        public string Level2Name { get; set; }
       
        public string Level2Description { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks1 { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks2 { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks3 { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks4 { get; set; }

        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> Created { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> LastUpdated { get; set; }
    }
}

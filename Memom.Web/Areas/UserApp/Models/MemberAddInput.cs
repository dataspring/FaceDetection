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
    public partial class MemberAddInput 
    {

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string DisplayName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Relation { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public string DateOfBirth { get; set; }


        [DataType(DataType.Text)]
        public string FacePhoto { get; set; }


    }
}
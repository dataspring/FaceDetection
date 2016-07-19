
using System;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations.Schema;


namespace MemomMvc52.Areas.UserAccount
{

    // this shows the extensibility point of being able to use a custom database/dbcontext
    public class SomeOtherEntity
    {
        public int ID { get; set; }
        public string Val1 { get; set; }
        public string Val2 { get; set; }
    }

    public class AuthenticationAudit
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Activity { get; set; }
        public string Detail { get; set; }
        public string ClientIP { get; set; }
    }

    public class PasswordHistory
    {
        public int ID { get; set; }
        public Guid UserID { get; set; }
        public DateTime DateChanged { get; set; }
        [Required]
        public string PasswordHash { get; set; }
    }



}
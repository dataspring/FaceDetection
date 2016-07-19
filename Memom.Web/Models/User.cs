using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemomMvc52.Models
{
    public class User
    {
        public User(string userName)
        {
            this.UserName = userName;
        }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }
}
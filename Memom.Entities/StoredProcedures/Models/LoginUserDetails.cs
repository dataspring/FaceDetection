using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memom.Entities.Models
{
    class LoginUserDetails
    {
        public int Key { get; set; }
        public System.Guid ID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public bool IsAccountClosed { get; set; }
        public bool IsLoginAllowed { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public bool RequiresPasswordReset { get; set; }
        public string Email { get; set; }
        public bool IsAccountVerified { get; set; }

    }
}

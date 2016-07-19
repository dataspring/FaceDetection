using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memom.Entities.Models
{
    public partial class AuthResult
    {
        public int AuthOutCome { get; set; }
        public int PasswordReset { get; set; }
    }

    public partial class RegisterResult
    {
        public int IsRegistered { get; set; }
        public string Remarks { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
    }

    public partial class ChangePwdResult
    {
        public int IsPasswordChanged { get; set; }
        public string Remarks { get; set; }
    }

    public partial class ResetResult
    {
        public int IsPasswordReset { get; set; }
        public string Remarks { get; set; }
    }

    public partial class AuthorizeCheckResult
    {
        public int IsAuthorized { get; set; }
        public string Remarks { get; set; }
    }

    public partial class RoleBoolResult
    {
        public int IsPresent { get; set; }
        public string Remarks { get; set; }
    }

}

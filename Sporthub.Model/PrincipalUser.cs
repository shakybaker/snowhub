using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Web;


namespace Sporthub.Model
{
    public class PrincipalUser : IPrincipal
    {
        public PrincipalUser(IIdentity identity)
        {
            this.Identity = identity;
        }

        public IIdentity Identity { get; set; }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}

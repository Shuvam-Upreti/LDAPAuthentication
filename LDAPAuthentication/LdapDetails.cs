using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDAPAuthentication
{
    public class LdapDetails
    {
        public string Domain { get; set; }
        public string Path { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDAPAuthentication
{
    public class Ldap
    {
        public LdapDetails GetLdapDetails(string domain, string path, string username, string password)
        {

            LdapDetails details = new LdapDetails()
            {
                Domain = domain,
                Path = path,
                UserName = username,
                Password = password
            };
            return details;
        }

      
        public bool ValidUserDescriptions(string domain, string path, string username, string password)
        {

            string ldapHost = domain; 
            int ldapPort = 389; 

            using (var ldapConnection = new LdapConnection())
            {
                try
                {
                    ldapConnection.Connect(ldapHost, ldapPort);
                    ldapConnection.Bind(@$"{username}@{domain}", password);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"LDAP Exception: {ex.Message}");
                    return false;
                }
            }
        }


        public string ReturnDescription(string domain,string path, string username,string password)
        {
            string ldapHost = domain; // LDAP server hostname or IP
            int ldapPort = 389;

            using (var ldapConnection = new LdapConnection())
            {
                ldapConnection.Connect(ldapHost, ldapPort);
                ldapConnection.Bind(@$"{username}@{domain}", password);
                string searchBase = path; // Update this as needed
                string searchFilter = $"(sAMAccountName={username})";

                var searchResults = ldapConnection.Search(
                    searchBase,
                    LdapConnection.ScopeSub,
                    searchFilter,
                    new[] { "description" },
                    false
                );

                Task.Delay( 500 ).Wait();  

                if (searchResults.Count > 0)
                {
                    var entry = searchResults.Next();
                    var description = entry.GetAttribute("description")?.StringValue;


                    return description.Length > 0 ? description : null;

                }
            }
            return null;

        }
    }
}

////string userDn = $"cn={username},{path}";

////ldapConnection.Bind(userDn,password);

//string searchBase = path; // Update this as needed
//string searchFilter = $"(sAMAccountName={username})";

//var searchResults = ldapConnection.Search(
//    searchBase,
//    LdapConnection.ScopeSub,
//    searchFilter,
//    new[] { "description" },
//    false
//);

//if (searchResults.Count > 0)
//{
//    return searchResults.ToString();
//    //var entry = searchResults.Next();
//    //var description = entry.GetAttribute("description")?.StringValue;

//    //if (description != null)
//    //{
//    //    return description.Length > 0 ? description : null;
//    //}
//}
//            }

//        }

//    }
//}

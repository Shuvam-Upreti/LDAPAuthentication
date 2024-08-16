using LDAPAuthentication;
using System.IO;

namespace LDAPAuthentication
{
    public class LDAPAuthentication
    {
        public static void Main(string[] args)
        {
            do
            {
                Ldap ldap = new Ldap();

                Console.Write("Enter Domain: ");
                string domain = Console.ReadLine();

                Console.Write("Enter Path: ");
                string path = Console.ReadLine();

                Console.Write("Enter Username: ");
                string username = Console.ReadLine();

                Console.Write("Enter Password: ");
                string password = Console.ReadLine();

                LdapDetails result = ldap.GetLdapDetails(domain, path, username, password);
                Console.WriteLine($"Domain: {result.Domain}, Path: {result.Path}, Username: {result.UserName}, Password: {result.Password}");

                var connect = ldap.ValidUserDescriptions(result.Domain, result.Path, result.UserName, result.Password);
                Console.WriteLine(connect.ToString(), $"Retrying......");
                if (connect)
                {
                    var desc = ldap.ReturnDescription(domain, path, username, password);
                    Console.WriteLine(desc);
                }
            } while (true);

        }

    }
}

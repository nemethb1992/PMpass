using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PMpass.Utility
{
    public class Password
    {
         protected static byte[] GetPasswordData(string password)
         {

             string formattedPassword;

             formattedPassword = String.Format("\"{0}\"", password);

             return (Encoding.Unicode.GetBytes(formattedPassword));

         }
        public string Reset(string Username, string OldPassword, string NewPassword1, string NewPassword2)
        {
            string returnInfo = "";
            string server = "192.168.144.21:636";
            string domain = "pmhu.local";
            const int ldapErrorInvalidCredentials = 0x31;

            try
            {
                var ldapConnection = new LdapConnection(server);
                var networkCredential = new NetworkCredential(Username, OldPassword, domain);
                ldapConnection.SessionOptions.SecureSocketLayer = true;
                ldapConnection.AuthType = AuthType.Negotiate;
                ldapConnection.Bind(networkCredential);

                returnInfo = "A felhasználó hitelesített!";

                DirectoryEntry entry = new DirectoryEntry("LDAP://" + server, Username, OldPassword);
                DirectorySearcher Dsearch = new DirectorySearcher(entry);
                Dsearch.Filter = "(&(objectClass=user)(samaccountname=" + Username + "))";
                SearchResult result = Dsearch.FindOne();

                if (result != null)
                {
                    DirectoryEntry userEntry = result.GetDirectoryEntry();
                    string Name = userEntry.Properties["name"].Value.ToString();
                    string userDN = userEntry.Properties["distinguishedName"].Value.ToString();
                    if ((NewPassword1 != "") && (NewPassword2 != "") && (NewPassword1 == NewPassword2))
                    {
                        returnInfo = "A jelszavak egyeznek!";
                        try
                        {
                            DirectoryAttributeModification deleteMod = new DirectoryAttributeModification();
                            deleteMod.Name = "unicodePwd";
                            deleteMod.Add(GetPasswordData(OldPassword));
                            deleteMod.Operation = DirectoryAttributeOperation.Delete;
                            
                            DirectoryAttributeModification addMod = new DirectoryAttributeModification();
                            addMod.Name = "unicodePwd";
                            addMod.Add(GetPasswordData(NewPassword1));
                            addMod.Operation = DirectoryAttributeOperation.Add;
                            
                            ModifyRequest request = new ModifyRequest(userDN, deleteMod, addMod);
                            DirectoryResponse response = ldapConnection.SendRequest(request);
                            returnInfo = "A jelszó módosítása sikerült!";
                        }
                        catch (Exception excep)
                        {
                            returnInfo = "A jelszó módosítása nem sikerült!";
                            returnInfo += excep.Message;
                        }
                    }
                    else
                    {
                        returnInfo = "A jelszavaknak egyezni kell és nem lehet üres!";
                    }
                }
            }
            catch (LdapException ldapException)
            {
                // Invalid credentials throw an exception with a specific error code
                if (ldapException.ErrorCode.Equals(ldapErrorInvalidCredentials))
                {
                    //return false;
                    returnInfo = "A felhasználói adatok nem megfelelőek!";
                }
                //returnInfo = ldapException.Message;
            }
            return returnInfo;
        }
        
    }
}

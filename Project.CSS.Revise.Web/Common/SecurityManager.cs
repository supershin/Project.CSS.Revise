using System.Security.Cryptography;
using System.Text;

namespace Project.CSS.Revise.Web.Common
{
    public static class SecurityManager
    {
        public static string EnCryptPassword(string password)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(password);
            string encryptPassword = System.Convert.ToBase64String(toEncodeAsBytes);
            return encryptPassword;
        }
        public static string DecodeFrom64(string encryptData)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encryptData);
            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }
    }
}

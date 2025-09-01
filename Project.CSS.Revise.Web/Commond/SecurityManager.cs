using System.Text;

namespace Project.CSS.Revise.Web.Commond
{
    public static class SecurityManager
    {
        public static string EnCryptPassword(string password)
        {
            byte[] toEncodeAsBytes = Encoding.UTF8.GetBytes(password);
            string encryptPassword = Convert.ToBase64String(toEncodeAsBytes);
            return encryptPassword;
        }
        public static string DecodeFrom64(string encryptData)
        {
            byte[] encodedDataAsBytes = Convert.FromBase64String(encryptData);
            string returnValue = Encoding.UTF8.GetString(encodedDataAsBytes);
            return returnValue;
        }
        public static string EnCryptText(this string text)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(text);
            string encryptPassword = System.Convert.ToBase64String(toEncodeAsBytes);
            return encryptPassword;
        }

        public static string DecodeFrom64Text(this string encryptData)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encryptData);
            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }
    }
}

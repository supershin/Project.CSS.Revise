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

        public static string TryDecodeFrom64(string encryptData)
        {
            if (string.IsNullOrWhiteSpace(encryptData))
                return string.Empty;

            try
            {
                byte[] encodedDataAsBytes = Convert.FromBase64String(encryptData);
                return Encoding.UTF8.GetString(encodedDataAsBytes);
            }
            catch (FormatException)
            {
                // ถ้าไม่ใช่ Base64 ก็คืนค่าเดิม (คิดว่ามันเป็น plain text)
                return encryptData;
            }
        }

    }
}

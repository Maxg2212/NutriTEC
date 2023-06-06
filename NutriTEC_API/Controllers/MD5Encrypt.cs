using System.Security.Cryptography;
using System.Text;

namespace NutriTEC_API.Controllers
{
    public class MD5Encrypt
    {
        public static string EncryptPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2")); // Convert each byte to a hexadecimal string representation
                }

                return builder.ToString();
            }
        }
    }
}

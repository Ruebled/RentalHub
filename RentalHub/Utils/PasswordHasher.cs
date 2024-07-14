using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace RentalHub
{
    public class PasswordHasher
    {
        public static string HashPassword(string input)
        {
            using SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public static string HashSecureString(SecureString input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convert SecureString to byte array
                byte[] bytes = ConvertSecureStringToByteArray(input);

                // Compute hash
                byte[] hashBytes = sha256Hash.ComputeHash(bytes);

                // Convert hash to hexadecimal string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private static byte[] ConvertSecureStringToByteArray(SecureString secureString)
        {
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(secureString);
            char[] chars = null;
            try
            {
                // Copy the SecureString characters into a managed char array
                chars = new char[secureString.Length];
                System.Runtime.InteropServices.Marshal.Copy(ptr, chars, 0, secureString.Length);

                // Convert char array to UTF-8 encoded byte array
                return Encoding.UTF8.GetBytes(chars);
            }
            finally
            {
                // Clear the sensitive data from memory
                if (chars != null)
                {
                    Array.Clear(chars, 0, chars.Length);
                }
                System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(ptr);
            }
        }
    }
}

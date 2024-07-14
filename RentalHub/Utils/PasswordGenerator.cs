using System.Security.Cryptography;
using System.Text;

namespace RentalHub.Utils
{
    public class PasswordGenerator
    {
        public static string GeneratePassword(int length)
        {
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string specialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?";

            if (length < 8)
            {
                throw new ArgumentException("Password length must be at least 8 characters.");
            }

            var allChars = uppercase + lowercase + digits + specialChars;
            var password = new StringBuilder();
            var random = new RNGCryptoServiceProvider();
            var buffer = new byte[4];

            // Ensure at least one character from each character set is included
            password.Append(uppercase[GetRandomInt(random, buffer, uppercase.Length)]);
            password.Append(lowercase[GetRandomInt(random, buffer, lowercase.Length)]);
            password.Append(digits[GetRandomInt(random, buffer, digits.Length)]);
            password.Append(specialChars[GetRandomInt(random, buffer, specialChars.Length)]);

            // Fill the rest of the password length with random characters from all sets
            for (int i = 4; i < length; i++)
            {
                password.Append(allChars[GetRandomInt(random, buffer, allChars.Length)]);
            }

            // Shuffle the password to ensure the first four characters are not predictable
            return new string(password.ToString().OrderBy(c => GetRandomInt(random, buffer, password.Length)).ToArray());
        }

        private static int GetRandomInt(RNGCryptoServiceProvider random, byte[] buffer, int maxValue)
        {
            random.GetBytes(buffer);
            return (int)(BitConverter.ToUInt32(buffer, 0) % maxValue);
        }
    }
}
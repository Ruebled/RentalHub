using System;
using System.Security.Cryptography;
using System.Text;

using Microsoft.Win32;

public static class CredentialManager
{
    private const string CREDENTIALS_KEY = "Software\\RentalHub\\Credentials";

    public static void SaveCredentials(string username, string password)
    {
        try
        {
            var encryptedPassword = Encrypt(password);
            var key = Registry.CurrentUser.CreateSubKey(CREDENTIALS_KEY);
            if (key != null)
            {
                key.SetValue("Username", username);
                key.SetValue("Password", encryptedPassword);
                key.Close();
            }
            else
            {
                throw new Exception("Unable to create or open registry key.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save credentials: {ex.Message}");
            // You may want to log the exception or handle it appropriately
        }
    }

    public static (string Username, string Password) RetrieveCredentials()
    {
        try
        {
            var key = Registry.CurrentUser.OpenSubKey(CREDENTIALS_KEY);
            if (key != null)
            {
                var username = key.GetValue("Username") as string;
                var encryptedPassword = key.GetValue("Password") as byte[];
                key.Close();

                if (encryptedPassword != null)
                {
                    var password = Decrypt(encryptedPassword);
                    return (username, password);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to retrieve credentials: {ex.Message}");
            // You may want to log the exception or handle it appropriately
        }

        return (null, null); // Return null if credentials not found or error occurred
    }

    public static void ClearSavedCredentials()
    {
        try
        {
            var key = Registry.CurrentUser.OpenSubKey(CREDENTIALS_KEY, true);
            if (key != null)
            {
                key.DeleteValue("Username", false);
                key.DeleteValue("Password", false);
                key.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to clear saved credentials: {ex.Message}");
            // You may want to log the exception or handle it appropriately
        }
    }

    private static byte[] Encrypt(string plainText)
    {
        try
        {
            var bytes = Encoding.UTF8.GetBytes(plainText);
            return ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Encryption failed: {ex.Message}");
            throw; // Rethrow the exception or handle it according to your application's needs
        }
    }

    private static string Decrypt(byte[] encryptedData)
    {
        try
        {
            if (encryptedData == null)
            {
                return null; // Handle case where no encrypted data is found
            }

            var bytes = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(bytes);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Decryption failed: {ex.Message}");
            throw; // Rethrow the exception or handle it according to your application's needs
        }
    }
}

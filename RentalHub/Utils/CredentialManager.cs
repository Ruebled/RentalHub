using System;
using System.Security.Cryptography;
using System.Text;

using Microsoft.Win32;

public static class CredentialManager
{
    private const string CREDENTIALS_KEY = "Software\\YourAppName\\Credentials";

    public static void SaveCredentials(string username, string password)
    {
        var encryptedPassword = Encrypt(password);
        var key = Registry.CurrentUser.CreateSubKey(CREDENTIALS_KEY);
        key.SetValue("Username", username);
        key.SetValue("Password", encryptedPassword);
    }

    public static (string Username, string Password) RetrieveCredentials()
    {
        var key = Registry.CurrentUser.OpenSubKey(CREDENTIALS_KEY);
        if (key == null) return (null, null);

        var username = key.GetValue("Username") as string;
        var encryptedPassword = key.GetValue("Password") as byte[];
        var password = Decrypt(encryptedPassword);

        return (username, password);
    }

    public static void ClearSavedCredentials()
    {
        var key = Registry.CurrentUser.OpenSubKey(CREDENTIALS_KEY, true);
        if (key != null)
        {
            key.DeleteValue("Username", false);
            key.DeleteValue("Password", false);
        }
    }

    private static byte[] Encrypt(string plainText)
    {
        var bytes = Encoding.UTF8.GetBytes(plainText);
        return ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);
    }

    private static string Decrypt(byte[] encryptedData)
    {
        var bytes = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
        return Encoding.UTF8.GetString(bytes);
    }
}

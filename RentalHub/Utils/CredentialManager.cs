using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;

using Microsoft.Win32;

public static class CredentialManager
{
    private const string CREDENTIALS_KEY = "Software\\RentalHub\\Credentials";

    public static void SaveCredentials(string username, SecureString password)
    {
        try
        {
            byte[] encryptedPassword = EncryptSecureString(password);
            var key = Registry.CurrentUser.CreateSubKey(CREDENTIALS_KEY);
            key.SetValue("Username", username);
            key.SetValue("Password", encryptedPassword);
        }
        catch (Exception ex)
        {
            // Log the exception (implement a logging mechanism if necessary)
            Console.WriteLine("Error saving credentials: " + ex.Message);
        }
    }

    public static (string Username, SecureString Password) RetrieveCredentials()
    {
        try
        {
            var key = Registry.CurrentUser.OpenSubKey(CREDENTIALS_KEY);
            if (key == null) return (null, null);

            var username = key.GetValue("Username") as string;
            var encryptedPassword = key.GetValue("Password") as byte[];
            if (username == null || encryptedPassword == null)
            {
                return (null, null);
            }

            var password = DecryptToSecureString(encryptedPassword);
            return (username, password);
        }
        catch (Exception ex)
        {
            // Log the exception (implement a logging mechanism if necessary)
            Console.WriteLine("Error retrieving credentials: " + ex.Message);
            return (null, null);
        }
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
            }
        }
        catch (Exception ex)
        {
            // Log the exception (implement a logging mechanism if necessary)
            Console.WriteLine("Error clearing credentials: " + ex.Message);
        }
    }

    private static byte[] EncryptSecureString(SecureString secureString)
    {
        IntPtr ptr = IntPtr.Zero;
        try
        {
            ptr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
            byte[] bytes = new byte[secureString.Length * 2];
            Marshal.Copy(ptr, bytes, 0, bytes.Length);
            return ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);
        }
        finally
        {
            if (ptr != IntPtr.Zero)
            {
                Marshal.ZeroFreeGlobalAllocUnicode(ptr);
            }
        }
    }

    private static SecureString DecryptToSecureString(byte[] encryptedData)
    {
        try
        {
            byte[] bytes = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
            SecureString secureString = new SecureString();
            for (int i = 0; i < bytes.Length / 2; i++)
            {
                char c = BitConverter.ToChar(bytes, i * 2);
                secureString.AppendChar(c);
            }
            secureString.MakeReadOnly();
            return secureString;
        }
        catch (Exception ex)
        {
            // Log the exception (implement a logging mechanism if necessary)
            Console.WriteLine("Error decrypting secure string: " + ex.Message);
            return null;
        }
    }
}

using System.Text.RegularExpressions;

namespace RentalHub.Utils
{
    public class PhoneNumberValidator
    {
        // Dictionary to hold country code or name as key and regex pattern as value
        private static readonly Dictionary<string, string> CountryRegexPatterns = new Dictionary<string, string>
    {
        { "RO", @"^(?:(?:\+|00)40(?:7\d{8}|2\d{8,9}|3\d{8}|4\d{8}|5\d{8}|6\d{8}))$" }, // Romania
        { "US", @"^(?:(?:\+|00)1)?(?:\(\d{3}\)|\d{3})(?:[ -]?\d{3}){2}$" }, // United States
        { "GB", @"^(?:(?:\+|00)44)?(?:\(\d{4}\)|\d{4})(?:[ -]?\d{3}){2}$" }, // United Kingdom
        { "DE", @"^(?:(?:\+|00)49)?(?:\(\d{5}\)|\d{5})(?:[ -]?\d{5,6})$" }, // Germany
        // Add more countries as needed
    };

        public static bool ValidatePhoneNumber(string phoneNumber, string countryCode, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrEmpty(phoneNumber))
            {
                errorMessage = "Phone number cannot be empty.";
                return false;
            }

            if (!CountryRegexPatterns.ContainsKey(countryCode))
            {
                errorMessage = "Country code not found or not supported.";
                return false;
            }

            string regexPattern = CountryRegexPatterns[countryCode];
            if (!Regex.IsMatch(phoneNumber, regexPattern))
            {
                errorMessage = $"Invalid phone number format for {countryCode}.";
                return false;
            }

            return true;
        }
    }
}
namespace EmployeeManagementSystem.Server.Helper
{
    public class PasswordHelper
    {
        private static readonly Random Random = new();

        public static string GenerateRandomPassword()
        {
            const string upperCaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerCaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "1234567890";
            const string specialChars = "!@#$%^&*()_+[]{}|;:',.<>?/\\\"";
            const string alphaNumericChars = upperCaseChars + lowerCaseChars + digits;

            // Ensure at least one of each required character type
            char upperCase = upperCaseChars[Random.Next(upperCaseChars.Length)];
            char lowerCase = lowerCaseChars[Random.Next(lowerCaseChars.Length)];
            char digit = digits[Random.Next(digits.Length)];
            char specialChar = specialChars[Random.Next(specialChars.Length)];

            // Remaining 4 characters are random alphanumeric
            string otherChars = new string(Enumerable.Repeat(alphaNumericChars, 4)
                .Select(s => s[Random.Next(s.Length)]).ToArray());

            // Combine all characters
            char[] passwordChars = (upperCase + lowerCase + digit + specialChar + otherChars).ToCharArray();

            // Shuffle the password to randomize the order
            return Shuffle(passwordChars);
        }

        private static string Shuffle(char[] array)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = Random.Next(i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }
            return new string(array);
        }
    }
}

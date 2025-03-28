using System;
using System.Linq;

namespace RsaIdNumbers
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "S101:Rename class 'RSAIdValidator' to match pascal case naming rules, consider using 'RsaIdValidator'.")]
    public static class RSAIdValidator
    {
        public static bool IsValidSAID(string idNumber)
        {
            // Check length
            if (idNumber.Length != 13)
                return false;

            // Check if all characters are digits
            if (!idNumber.All(char.IsDigit))
                return false;

            // Extract and validate date of birth
            var yy = int.Parse(idNumber.AsSpan(0, 2));
            var mm = int.Parse(idNumber.AsSpan(2, 2));
            var dd = int.Parse(idNumber.AsSpan(4, 2));

            // Handle Y2K issue - assume 00-30 is 2000s, and 22-99 is 1900s
            int year = yy <= 25 ? 2000 + yy : 1900 + yy;

            if (!IsValidDate(year, mm, dd))
                return false;

            // Citizenship validation
            char citizenshipDigit = idNumber[10];
            if (citizenshipDigit != '0' && citizenshipDigit != '1')
                return false;

            // Checksum validation
            return IsValidChecksum(idNumber);
        }

        private static bool IsValidDate(int year, int month, int day)
        {
            var cultureInfo = System.Globalization.CultureInfo.InvariantCulture;
            return DateTime.TryParseExact($"{year:0000}-{month:00}-{day:00}", "yyyy-MM-dd", cultureInfo, System.Globalization.DateTimeStyles.None, out _);
        }

        private static bool IsValidChecksum(string id)
        {
            int sum = 0;

            for (int i = 0; i < 12; i++)
            {
                int value = id[i] - '0';

                if ((11 - i) % 2 == 0) // Check if it's an even position from the right (0-based index)
                {
                    value *= 2;
                    if (value > 9)
                    {
                        value -= 9;
                    }
                }

                sum += value;
            }

            int checksumDigit = 10 - (sum % 10);
            if (checksumDigit == 10) checksumDigit = 0;

            return checksumDigit == id[12] - '0';
        }
    }
}

using System;
using System.Globalization;

namespace RsaIdNumbers
{
    public static class RSAIdValidator
    {
        public static bool IsValidSAID(string idNumber)
        {
            if (idNumber.Length != 13)
            {
                return false;
            }

            // Parse the date of birth from the first 6 digits
            string dateOfBirthString = idNumber.Substring(0, 6);
            DateTime dateOfBirth;
            if (!DateTime.TryParseExact(dateOfBirthString, "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth))
            {
                return false;
            }

            // Parse the gender from the next 4 digits
            string genderString = idNumber.Substring(6, 4);
            int gender;
            if (!int.TryParse(genderString, out gender))
            {
                return false;
            }

            // Parse the citizenship status from the next digit
            string citizenshipString = idNumber.Substring(10, 1);
            int citizenship;
            if (!int.TryParse(citizenshipString, out citizenship))
            {
                return false;
            }

            // Parse the checksum from the last digit
            string checksumString = idNumber.Substring(12, 1);
            int checksum;
            if (!int.TryParse(checksumString, out checksum))
            {
                return false;
            }

            // Check that the gender is valid
            if (gender < 0 || gender > 9999)
            {
                return false;
            }

            // Check that the citizenship status is valid
            if (citizenship != 0 && citizenship != 1)
            {
                return false;
            }

            // Calculate the Luhn checksum

            int[] luhnFactors = new int[] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 };
            int[] digits = new int[13];
            for (int i = 0; i < 12; i++)
            {
                if (!int.TryParse(idNumber.Substring(i, 1), out digits[i]))
                {
                    return false;
                }
            }
            int luhnSum = 0;
            for (int i = 0; i < 13; i++)
            {
                int luhnProduct = digits[i] * luhnFactors[i];
                if (luhnProduct > 9)
                {
                    luhnProduct -= 9;
                }
                luhnSum += luhnProduct;
            }
            int calculatedChecksum = (10 - (luhnSum % 10)) % 10;

            // Compare the calculated checksum to the actual checksum
            return checksum == calculatedChecksum;
        }

    }
}

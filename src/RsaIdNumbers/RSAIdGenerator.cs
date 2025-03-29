using System;
using System.Text;

namespace RsaIdNumbers
{
    public static class RSAIdGenerator
    {
        private static readonly Random random = new Random();

        public static string GenerateValidSAID()
        {
            return GenerateValidSAID(DateTime.Now);
        }

        public static string GenerateValidSAID(DateTime maxBirthDate)
        {
            DateTime minDate = new DateTime(1900, 1, 1);
            DateTime birthDate = GenerateRandomDate(minDate, maxBirthDate);
            return GenerateValidSAID(birthDate, GetRandomGender(), GetRandomCitizenship());
        }

        public static string GenerateValidSAID(DateTime birthDate, bool isFemale, bool isCitizen)
        {
            StringBuilder idBuilder = new StringBuilder();

            idBuilder.Append(birthDate.ToString("yyMMdd"));

            int genderAndSequence = isFemale 
                ? random.Next(0, 5000) 
                : random.Next(5000, 10000);
            idBuilder.Append(genderAndSequence.ToString("D4"));

            idBuilder.Append(isCitizen ? "0" : "1");

            idBuilder.Append("8");

            string idWithoutChecksum = idBuilder.ToString();
            int checksumDigit = CalculateChecksumDigit(idWithoutChecksum);
            idBuilder.Append(checksumDigit);

            return idBuilder.ToString();
        }

        private static DateTime GenerateRandomDate(DateTime start, DateTime end)
        {
            int range = (end - start).Days;
            return start.AddDays(random.Next(range));
        }

        private static bool GetRandomGender()
        {
            return random.Next(2) == 0;
        }

        private static bool GetRandomCitizenship()
        {
            return random.Next(10) < 9;
        }

        private static int CalculateChecksumDigit(string idWithoutChecksum)
        {
            int sum = 0;

            for (int i = 0; i < 12; i++)
            {
                int value = idWithoutChecksum[i] - '0';

                if ((11 - i) % 2 == 0)
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
            return checksumDigit == 10 ? 0 : checksumDigit;
        }
    }
}
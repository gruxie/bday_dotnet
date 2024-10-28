using System;
using System.Globalization;

namespace BirthdayCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your birthday (MMDDYYYY): ");
            string input = Console.ReadLine();

            if (!DateTime.TryParseExact(input, "MMddyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime birthday))
            {
                Console.WriteLine("Invalid format. Please enter your birthday in MMDDYYYY format.");
                return;
            }

            DateTime now = DateTime.Now;
            DateTime nextBirthday = GetNextBirthday(birthday, now);

            TimeSpan difference = nextBirthday - now;
            double daysUntil = difference.TotalDays;

            // Handle cases where birthday is today
            if (daysUntil <= 0)
            {
                daysUntil = 0;
            }

            // Round up to the nearest whole day
            double daysUntilRounded = Math.Ceiling(daysUntil);

            Console.WriteLine($"Days until your next birthday: {daysUntilRounded}");
        }

        /// <summary>
        /// Calculates the next birthday based on the current date.
        /// Handles leap years, especially for birthdays on February 29th.
        /// </summary>
        /// <param name="birthday">The user's birthday as a DateTime.</param>
        /// <param name="currentDate">The current date and time.</param>
        /// <returns>The DateTime of the next birthday.</returns>
        static DateTime GetNextBirthday(DateTime birthday, DateTime currentDate)
        {
            int year = currentDate.Year;
            DateTime nextBirthday;

            // Attempt to set the birthday year to the current year
            if (IsValidDate(birthday.Month, birthday.Day, year))
            {
                nextBirthday = new DateTime(year, birthday.Month, birthday.Day, birthday.Hour, birthday.Minute, birthday.Second);
            }
            else
            {
                // Handle February 29th on non-leap years by setting to February 28th
                nextBirthday = new DateTime(year, 2, 28, birthday.Hour, birthday.Minute, birthday.Second);
            }

            // If the birthday has already occurred this year, set to next year
            if (nextBirthday < currentDate)
            {
                year += 1;
                if (IsValidDate(birthday.Month, birthday.Day, year))
                {
                    nextBirthday = new DateTime(year, birthday.Month, birthday.Day, birthday.Hour, birthday.Minute, birthday.Second);
                }
                else
                {
                    // Handle February 29th on non-leap years by setting to February 28th
                    nextBirthday = new DateTime(year, 2, 28, birthday.Hour, birthday.Minute, birthday.Second);
                }
            }

            return nextBirthday;
        }

        /// <summary>
        /// Checks if a given date is valid (handles leap years).
        /// </summary>
        /// <param name="month">Month component.</param>
        /// <param name="day">Day component.</param>
        /// <param name="year">Year component.</param>
        /// <returns>True if the date is valid; otherwise, false.</returns>
        static bool IsValidDate(int month, int day, int year)
        {
            try
            {
                DateTime testDate = new DateTime(year, month, day);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

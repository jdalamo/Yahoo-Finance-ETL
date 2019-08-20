using System;

namespace stock_update
{
    public class DateRange
    {
        public Tuple<DateTime, DateTime> GetDates(string[] CLArgs)
        {
            DateTime startDate;
            DateTime endDate;
            int startYear;
            int startMonth;
            int startDay;
            int endYear;
            int endMonth;
            int endDay;

            // Checking if a date range is specified--if not, last month's data is downloaded
            if (CLArgs.Length > 3)
            {
                var startArgs = CLArgs[3].Split('/');
                startMonth = Int32.Parse(startArgs[0]);
                startDay = Int32.Parse(startArgs[1]);
                startYear = Int32.Parse(startArgs[2]);

                startDate = new DateTime(startYear, startMonth, startDay, 0, 0, 0, 0, 0);

                var endArgs = CLArgs[4].Split('/');
                endMonth = Int32.Parse(endArgs[0]);
                endDay = Int32.Parse(endArgs[1]);
                endYear = Int32.Parse(endArgs[2]);

                endDate = new DateTime(endYear, endMonth, endDay, 0, 0, 0, 0, 0);
                Console.WriteLine($"Downloading stock price data from Yahoo Finance for period {startMonth}/{startDay}/{startYear} - {endMonth}/{endDay}/{endYear}");
            }
            else
            {
                var year = Calendar.Year;
                var month = Calendar.LastMonth;
                var numDays = (int)Calendar.DayCount[month];

                startDate = new DateTime(year, month, 1, 0, 0, 0, 0, 0);
                endDate = new DateTime(year, month, numDays, 0, 0, 0, 0, 0);
                Console.WriteLine($"Downloading stock price data from Yahoo Finance for period {month}/{1}/{year} - {month}/{numDays}/{year}");
            }

            return Tuple.Create(startDate, endDate);
        }
    }
}
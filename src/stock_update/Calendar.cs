using System;
using System.Collections;

namespace stock_update
{
    public class Calendar
    {
        static DateTime Now = DateTime.Now;

        // If it is currently January, last year and month need adjustment
        public static int Year
        {
            get
            {
                if (Now.Month == 1)
                {
                    return Now.Year - 1;
                }
                else
                {
                    return Now.Year;
                }
            }
        }
        public static int LastMonth
        {
            get
            {
                if (Now.Month == 1)
                {
                    return 12;
                }
                    else
                {
                    return Now.Month - 1;
                }
            }
        }

        public static int NumDays
        {
            get
            {
                // Checking for leap year to set correct number of days for February
                if (DateTime.IsLeapYear(Year))
                {
                    return 29;
                }
                else
                {
                    return 28;
                }
            }
        }

        // Maps month to number of days it has
        public static Hashtable DayCount = new Hashtable()
                {
                    {1, 31},
                    {2, NumDays},
                    {3, 31},
                    {4, 30},
                    {5, 31},
                    {6, 30},
                    {7, 31},
                    {8, 31},
                    {9, 30},
                    {10, 31},
                    {11, 30},
                    {12, 31},
                };
    }
}
using System;

namespace stock_update
{
    public class DailyPriceRecord
    {
        private DateTime date;
        private double high;
        private double low;
        private double open;
        private double close;
        private double volume;
        private double adj;
        private int row;

        public DailyPriceRecord(DateTime price_date, double price_high, double price_low, double price_open, double price_close, double stock_volume, double adj_close, int row_num)
        {
            date = price_date.AddHours(1); // Necessary because Orcale doesn't like times of 00:00
            high = price_high;
            low = price_low;
            open = price_open;
            close = price_close;
            volume = stock_volume;
            adj = adj_close;
            row = row_num;

        }

        public string Symbol {get {return "???";}}  //TODO:  replace ??? with stock symbol
        public string Date
        {
            get
            {
                return date.ToString("yyyy-MM-dd hh:mm:ss");  // Necessary because in SQL statement date must be passed as string to TO_DATE() then inserted
            }
        }
        public double High {get {return high;}}
        public double Low {get {return low;}}
        public double Open {get {return open;}}
        public double Close {get {return close;}}
        public double Volume {get {return volume;}}
        public double Adj_Close {get {return adj;}}
        public int Row {get {return row;}}
        public string Period
        {
            get
            {
                return date.ToString("yyyy-MM");
            }
        }
        public string Adm_Comment {get {return "Loaded from script";}}
        public int Active {get {return -1;}}
        public string Created_By {get {return Environment.UserName.ToUpper();}}
        // public string Changed_Uid {get {return "Null";}}
        // public string Changed_Date {get {return "Null";}}
    }
}
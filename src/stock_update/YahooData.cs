using System;
using System.Collections.Generic;
using YahooFinanceApi;

namespace stock_update
{
    public class YahooData
    {
        public async System.Threading.Tasks.Task<IReadOnlyList<Candle>> GetDataAsync(Tuple<DateTime, DateTime> dates)
        {
            var ticker = "???";  // TODO:  enter stock ticker symbol
            var frequency = Period.Daily;
            var startDate = dates.Item1;
            var endDate = dates.Item2;

            var data = await Yahoo.GetHistoricalAsync(ticker, startDate, endDate, frequency);

            return data;
        }
    }
}
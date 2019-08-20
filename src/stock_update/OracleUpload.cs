using System;
using System.Collections.Generic;
using YahooFinanceApi;
using Oracle.ManagedDataAccess.Client;

namespace stock_update
{
    public class OracleUpload
    {
        public void UploadData(IReadOnlyList<Candle> data, OracleConnection con)
        {
            var rowCount = 1;
            foreach (var row in data)
            {
                rowCount ++;
                var date = row.DateTime.Date;
                var high = (double) row.High;
                var low = (double) row.Low;
                var open = (double) row.Open;
                var close = (double) row.Close;
                var volume = (double) row.Volume;
                var adjClose = (double) row.AdjustedClose;

                DailyPriceRecord dpr = new DailyPriceRecord(date, high, low, open, close, volume, adjClose, rowCount);

                InsertRow(dpr, con);
            }
        }

        static void InsertRow(DailyPriceRecord row, OracleConnection conn)
        {
            using (OracleCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var storedProc = "???";  // TODO:  enter stored procedure
                cmd.CommandText = storedProc;

                cmd.Parameters.Add("p_symbol", row.Symbol).Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add("p_price_date", row.Date).Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add("p_price_open", row.Open).Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add("p_price_high", row.High).Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add("p_price_low", row.Low).Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add("p_price_close", row.Close).Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add("p_price_volume", row.Volume).Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add("p_price_adj_close", row.Adj_Close).Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add("p_row", row.Row).Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add("p_period", row.Period).Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add("p_adm_comment", row.Adm_Comment).Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add("p_active", row.Active).Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add("p_created_by", row.Created_By).Direction = System.Data.ParameterDirection.Input;

                cmd.ExecuteNonQuery();
            }
        }
    }
}
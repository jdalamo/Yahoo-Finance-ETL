using System;
using System.IO;
using System.Collections;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using Oracle.ManagedDataAccess.Client;
using YahooFinanceApi;


namespace stock_update
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var oracleNetDescription = GetLDAPOracleNetDescription(args[0]);
            Oracle.ManagedDataAccess.Client.OracleConfiguration.OracleDataSources.Add("db", oracleNetDescription);

            var connectString = $"DATA SOURCE=db;USER ID={args[1]};PASSWORD={args[2]}";
            
            OracleConnection oConn;
            try 
            {
                oConn = new OracleConnection(connectString);
                oConn.Open();
            }
            catch (Exception err)
            {
                var message = "Possible error while connecting to Oracle";
                ErrLog(err.Message, message);
                throw new Exception();
            }

            // Getting start and end dates for data download
            var dateRange = new DateRange();
            // Returns tuple of start date and end date
            var dates = dateRange.GetDates(args);

            var yahooData = new YahooData();
            System.Collections.Generic.IReadOnlyList<Candle> data;
            try
            {
                data = await yahooData.GetDataAsync(dates);
            }
            catch (Exception err)
            {
                var message = "Possible error with YahooFinanceApi";
                ErrLog(err.Message, message);
                throw new Exception();
            }

            Console.WriteLine("Uploading to Oracle");

            var uploader = new OracleUpload();
            try
            {
                uploader.UploadData(data, oConn);
            }
            catch (Exception err)
            {
                Console.WriteLine("Error uploading to Oracle");
                var message = "Possible error processing SQL Insert\r\n";
                ErrLog(err.Message, message);
                throw new Exception();
            }

            Console.WriteLine("Done uploading--closing connection");
            oConn.Close();
        }

        static void ErrLog(string error, string output="")
        {
            var configPath = @"..\config.txt";  // TODO:  need config path
            
            var paths = File.ReadAllLines(configPath);
            var line = paths[1];
            var errPath = line.Split('=')[1];

            using (StreamWriter sw = File.AppendText(errPath))
            {
                sw.WriteLine(DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss") + "\n");
                sw.WriteLine(output + "\n");
                sw.WriteLine(error + "\n");
            }
        }

        public static string GetLDAPOracleNetDescription(string directoryString)
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain))
            {
                string directoryServer = pc.ConnectedServer;
                string ldapAddress = $"LDAP://{directoryServer}";
                DirectoryEntry directoryEntry = new DirectoryEntry(ldapAddress);

                string query = $"(&(objectclass=orclNetService)(cn={directoryString}))";
                string orclnetdescstring = "orclnetdescstring";

                DirectorySearcher directorySearcher = new DirectorySearcher(directoryEntry, query, new[] { orclnetdescstring },
                    SearchScope.Subtree);

                SearchResult searchResult = directorySearcher.FindOne();
                if (searchResult == null) return null;

                string descriptor = searchResult.Properties[orclnetdescstring][0].ToString();

                return descriptor;
            }
        }
    }
}

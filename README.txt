How to use:

Can be run automatically as a job from the .bat file--in this case, without arguments passed in, it downloads the previous
month's stock price data and uploads it to Oracle.  Afterwards, it runs send_notification_email.vbs and a notification email is sent out.

Example:
\> stock_ETL.bat

Can also be run manually from command line:
.\src\stock_update> dotnet stock_update.dll

If no arguments are passed, the previous month's stock price is uploaded to Oracle.
If a different period is required, enter the start and end dates in this format:

dotnet stock_update.dll 5/1/2019 6/17/2019

Optional third argument: "replace"--replaces the selected period's data
Example:

dotnet stock_update.dll 6/1/2019 6/17/2019 replace


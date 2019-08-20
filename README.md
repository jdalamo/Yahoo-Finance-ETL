# Yahoo-Finance-ETL
ETL procedure written in C# to load stock price data from Yahoo finance into a local Oracle database

## Use case
---
This script was designed to be able to be run either as a job or manually.  If run as a job through the batch file, the intended use case is keeping a local Oracle database up to date with the company's stock price.  In this scenario, the script is run once a month.  It downloads the previous month's stock price and uploads it to the database.  Following that, a notification email is sent to the specified recipient in send_notification_email.vbs.  In the case that it is run manually, parameters can be passed in to specify what date range to ETL.  If that data already exists in the database, the "replace" parameter can be added to update this data.
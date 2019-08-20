@ECHO OFF 
cd update\src\stock_update\bin\Release-Test\netcoreapp2.2\win10-x64
dotnet stock_update.dll REM need database userid and pw
if NOT ["%errorlevel%"]==["0"] ECHO Upload not successful--fix error and try again & PAUSE & exit /b
cd ..\..\..\..\..\..\
ECHO Sending notification email
REM send_notification_email.vbs
if NOT ["%errorlevel%"]==["0"] ECHO Error: Email not sent & PAUSE & exit /b
ECHO Email sent
PAUSE
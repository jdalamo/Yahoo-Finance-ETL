
SendEmail()
Sub SendEmail()

    Set olApp = CreateObject("Outlook.Application")
    Set olMail = olApp.CreateItem(o)

    olMail.to = "desired_recipient@email.com"
    olMail.Subject = "Stock Price Update"
    olMail.Body = "The stock prices have been updated in the database." _
                   & vbNewLine & vbNewLine & "Best," & vbNewLine & "Your Name"

    olMail.Send

End Sub
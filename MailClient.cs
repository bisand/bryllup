using System.Net.Mail;

public class MailClient
{
    private string _host;

    public MailClient(string host)
    {
        _host = host;
    }

    public void Send(string body)
    {
        // Command-line argument must be the SMTP host.
        SmtpClient client = new SmtpClient(_host);
        // Specify the email sender.
        // Create a mailing address that includes a UTF8 character
        // in the display name.
        MailAddress from = new MailAddress("bryllup@biseth.net", "Bryllup 2022", System.Text.Encoding.UTF8);
        // Set destinations for the email message.
        MailAddress to = new MailAddress("andre@biseth.net");
        // Specify the message content.
        using (MailMessage message = new MailMessage(from, to))
        {
            message.Body = body;
            // Include some non-ASCII characters in body and subject.
            string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
            message.Body += Environment.NewLine + someArrows;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "test message 1" + someArrows;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            // Set the method that is called back when the send operation ends.
            // client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            // The userState can be any object that allows your callback
            // method to identify this send operation.
            // For this example, the userToken is a string constant.
            client.Send(message);
            // If the user canceled the send, and mail hasn't been sent yet,
            // then cancel the pending operation.
            // Clean up.
        }

        Console.WriteLine("Goodbye.");
    }
}

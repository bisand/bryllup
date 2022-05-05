using System.Collections;
using System.Net;
using System.Net.Mail;

public class MailClient
{
    private string? _host;
    private string? _username;
    private string? _password;
    private int _port;

    public MailClient()
    {
        _host = Environment.GetEnvironmentVariable("HOST");
        if (!int.TryParse(Environment.GetEnvironmentVariable("PORT"), System.Globalization.NumberStyles.Integer, null, out _port))
            _port = 587;
        _username = Environment.GetEnvironmentVariable("USERNAME");
        _password = Environment.GetEnvironmentVariable("PASSWORD");
    }

    public void Send(string? sender, string? body)
    {
        // Command-line argument must be the SMTP host.
        SmtpClient client = new SmtpClient(_host, _port);
        client.Credentials = new System.Net.NetworkCredential(_username, _password);
        // Specify the email sender.
        // Create a mailing address that includes a UTF8 character
        // in the display name.
        MailAddress from = new MailAddress(!string.IsNullOrWhiteSpace(sender) ? sender : "bryllup@biseth.net", "Bryllup 2022", System.Text.Encoding.UTF8);
        // Set destinations for the email message.
        MailAddress to = new MailAddress("bryllup@biseth.net");
        // Specify the message content.
        using (MailMessage message = new MailMessage(from, to))
        {
            message.Body = body;
            // Include some non-ASCII characters in body and subject.
            message.Body += Environment.NewLine;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "Bryllup RSVP";
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

using System;
using System.IO;
using System.Net;
using System.Net.Mail;

public class EmailSender
{
    private readonly string _smtpServer = "smtp.gmail.com";
    private readonly int _smtpPort = 587; // Gmail SMTP port for TLS
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;

    public EmailSender(string smtpUsername, string smtpPassword)
    {
        _smtpUsername = smtpUsername;
        _smtpPassword = smtpPassword;
    }

    public void SendPasswordResetEmail(string toEmail, string newPassword)
    {
        string subject = "Password Reset for RentalHub Account";
        string body = $@"
            <html>
            <body>
                <h2>Password Reset for RentalHub</h2>
                <p>Dear user,</p>
                <p>Your password for RentalHub has been reset successfully. Below is your new password:</p>
                <p><strong>{newPassword}</strong></p>
                <p>If you did not request this change, please contact support immediately.</p>
                <br>
                <p>Best regards,<br>RentalHub Team</p>
                <p>Disclaimer: This email was sent automatically. Please do not reply.</p>
            </body>
            </html>
        ";

        SendEmail(toEmail, subject, body, null, true);
    }

    private void SendEmail(string toEmail, string subject, string body, string fromEmail = null, bool isHtml = true, string[] attachments = null, MailPriority priority = MailPriority.Normal)
    {
        try
        {
            using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
            {
                smtpClient.EnableSsl = true; // Enable SSL/TLS
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);

                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(fromEmail ?? _smtpUsername);
                    message.To.Add(new MailAddress(toEmail));
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = isHtml;
                    message.Priority = priority;

                    // Attachments
                    if (attachments != null)
                    {
                        foreach (var attachmentPath in attachments)
                        {
                            if (File.Exists(attachmentPath))
                            {
                                var attachment = new Attachment(attachmentPath);
                                message.Attachments.Add(attachment);
                            }
                            else
                            {
                                Console.WriteLine($"Attachment file not found: {attachmentPath}");
                            }
                        }
                    }

                    smtpClient.Send(message);
                    Console.WriteLine("Email sent successfully.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
        }
    }
}

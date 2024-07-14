using Microsoft.Extensions.Configuration;

using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace RentalHub.Utils
{
    public class EmailSender
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;

        public static EmailSender Instance { get; private set; } = new EmailSender();

        private EmailSender()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = configBuilder.Build();

            _smtpUsername = configuration["SmtpSettings:SmtpUsername"] ?? "";
            _smtpPassword = configuration["SmtpSettings:SmtpPassword"] ?? "";
        }

        public bool SendPasswordResetEmail(string toEmail, string newPassword)
        {
            if (string.IsNullOrEmpty(_smtpUsername) || string.IsNullOrEmpty(_smtpPassword))
            {
                return false;
            }

            string subject = "Password Reset for RentalHub Account";
            string body = $@"
            <html>
            <body style='font-family: Arial, sans-serif; background-color: #d6d6b1; color: #201e1f;'>
                <div style='max-width: 600px; margin: auto; padding: 20px; border: 1px solid #2176ae; border-radius: 10px; background-color: #ffffff;'>
                    <div style='text-align: center; margin-bottom: 20px;'>
                        <img src='cid:RentalHubLogo' alt='RentalHub Logo' style='max-width: 150px;' />
                    </div>
                    <h2 style='color: #2176ae;'>Password Reset for RentalHub</h2>
                    <p>Dear user,</p>
                    <p>Your password for RentalHub has been reset successfully. Below is your new password:</p>
                    <div style='background-color: #f1f1f1; padding: 15px; text-align: center; border-radius: 5px; margin: 20px 0;'>
                        <p style='font-size: 24px; font-weight: bold; color: #333;'>{newPassword}</p>
                    </div>
                    <p>If you did not request this change, please contact support immediately.</p>
                    <br>
                    <p>Best regards,<br>RentalHub Team</p>
                    <p style='font-size: 12px; color: #888;'>Disclaimer: This email was sent automatically. Please do not reply.</p>
                </div>
            </body>
            </html>
        ";

            string logoPath = ".\\Logo\\Logo_100x100.png";
            SendEmail(toEmail, subject, body, attachments: null, imagePath: logoPath, contentId: "RentalHubLogo");

            return true;
        }


        public bool SendValidationCodeEmail(string toEmail, string validationCode)
        {
            if (string.IsNullOrEmpty(_smtpUsername) || string.IsNullOrEmpty(_smtpPassword))
            {
                return false;
            }

            string subject = "RentalHub Settings Change Confirmation";
            string body = $@"
                        <html>
                        <body style='font-family: Arial, sans-serif; background-color: #d6d6b1; color: #201e1f;'>
                            <div style='max-width: 600px; margin: auto; padding: 20px; border: 1px solid #2176ae; border-radius: 10px; background-color: #ffffff;'>
                                <div style='text-align: center; margin-bottom: 20px;'>
                                    <img src='cid:RentalHubLogo' alt='RentalHub Logo' style='max-width: 150px;' />
                                </div>
                                <h2 style='color: #2176ae;'>Confirm Your Settings Change</h2>
                                <p>Dear user,</p>
                                <p>We have received a request to update your settings on RentalHub. To ensure the security of your account, please enter the validation code below to confirm the changes:</p>
                                <div style='background-color: #f1f1f1; padding: 15px; text-align: center; border-radius: 5px; margin: 20px 0;'>
                                    <p style='font-size: 24px; font-weight: bold; color: #333;'>{validationCode}</p>
                                </div>
                                <p>If you did not make this request, please disregard this email or contact our support team immediately.</p>
                                <br>
                                <p>Best regards,<br>RentalHub Team</p>
                                <p style='font-size: 12px; color: #888;'>Disclaimer: This email was sent automatically. Please do not reply.</p>
                            </div>
                        </body>
                        </html>
                        ";

            string logoPath = ".\\Logo\\Logo_100x100.png";
            SendEmail(toEmail, subject, body, attachments: null, imagePath: logoPath, contentId: "RentalHubLogo");

            return true;
        }

        public bool SendDeletionConfirmationEmail(string toEmail, string validationCode)
        {
            if (string.IsNullOrEmpty(_smtpUsername) || string.IsNullOrEmpty(_smtpPassword))
            {
                return false;
            }

            string subject = "RentalHub Account Deletion Confirmation";
            string body = $@"
                <html>
                <body style='font-family: Arial, sans-serif; background-color: #d6d6b1; color: #201e1f;'>
                    <div style='max-width: 600px; margin: auto; padding: 20px; border: 1px solid #2176ae; border-radius: 10px; background-color: #ffffff;'>
                        <div style='text-align: center; margin-bottom: 20px;'>
                            <img src='cid:RentalHubLogo' alt='RentalHub Logo' style='max-width: 150px;' />
                        </div>
                        <h2 style='color: #2176ae;'>Confirm Your Account Deletion</h2>
                        <p>Dear user,</p>
                        <p>We have received a request to delete your account on RentalHub. To ensure the security of your account, please enter the validation code below to confirm the deletion:</p>
                        <div style='background-color: #f1f1f1; padding: 15px; text-align: center; border-radius: 5px; margin: 20px 0;'>
                            <p style='font-size: 24px; font-weight: bold; color: #333;'>{validationCode}</p>
                        </div>
                        <p>If you did not make this request, please disregard this email or contact our support team immediately.</p>
                        <br>
                        <p>Best regards,<br>RentalHub Team</p>
                        <p style='font-size: 12px; color: #888;'>Disclaimer: This email was sent automatically. Please do not reply.</p>
                    </div>
                </body>
                </html>
                ";

            string logoPath = ".\\Logo\\Logo_100x100.png";
            SendEmail(toEmail, subject, body, attachments: null, imagePath: logoPath, contentId: "RentalHubLogo");

            return true;
        }


        private void SendEmail(string toEmail, string subject, string body, string fromEmail = null, bool isHtml = true, string[] attachments = null, string imagePath = null, string contentId = null, MailPriority priority = MailPriority.Normal)
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

                        // Embedded images
                        if (!string.IsNullOrEmpty(imagePath) && !string.IsNullOrEmpty(contentId))
                        {
                            var inlineLogo = new LinkedResource(imagePath, "image/png")
                            {
                                ContentId = contentId
                            };

                            // Create alternate view with embedded image
                            var altView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                            altView.LinkedResources.Add(inlineLogo);
                            message.AlternateViews.Add(altView);
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
}

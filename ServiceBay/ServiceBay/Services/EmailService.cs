using ServiceBay.Models;
using System;
using System.Net;
using System.Net.Mail;

namespace ServiceBay.Services
{
    public class EmailService
    {
        public void SendUpdateEmail(Bid bid, String email)
        {
            String FROM = "1086340@ucn.dk";
            String FROMNAME = "Marton Vadasz";
            String TO = email;
            String SMTP_USERNAME = "AKIAUIXC57BTVX32VWYC";
            String SMTP_PASSWORD = "BNAm86ueYlyebB6M2fpcD0Iw9n0UkwZvwUoZGU46JxGr";
            String HOST = "email-smtp.eu-west-2.amazonaws.com";
            int PORT = 587;
            String SUBJECT = "Bid Notification";
            String BODY =
                "<h1>ServiceBay</h1>" +
                "<h2>Notification regading your bid on Auction: " + bid.Auction.AuctionName.ToString() + " DKK.</h2>"+
                "<p>You got outbid on the auction. The new price is " + bid.Price + ".</p>";

            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(FROM, FROMNAME);
            message.To.Add(new MailAddress(TO));
            message.Subject = SUBJECT;
            message.Body = BODY;

            using (var client = new SmtpClient(HOST, PORT))
            {
                client.Credentials = new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);
                client.EnableSsl = true;
                try
                {
                    System.Diagnostics.Debug.WriteLine("Attempting to send email...");
                    client.Send(message);
                    System.Diagnostics.Debug.WriteLine("Email sent!");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("The email was not sent.");
                    System.Diagnostics.Debug.WriteLine("Error message: " + ex.Message);
                }
            }
        }

        public void SendSellerEmail(Bid bid, String email)
        {
            String FROM = "1086340@ucn.dk";
            String FROMNAME = "Marton Vadasz";
            String TO = email;
            String SMTP_USERNAME = "AKIAUIXC57BTVX32VWYC";
            String SMTP_PASSWORD = "BNAm86ueYlyebB6M2fpcD0Iw9n0UkwZvwUoZGU46JxGr";
            String HOST = "email-smtp.eu-west-2.amazonaws.com";
            int PORT = 587;
            String SUBJECT = "Bid Notification";
            String BODY =
                "<h1>ServiceBay</h1>" +
                "<h2>Notification regading your Auction: " + bid.Auction.AuctionName.ToString() + ".</h2>" +
                "<p>There was a new bid on your auction. The new price is " + bid.Price + " DKK.</p>";

            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(FROM, FROMNAME);
            message.To.Add(new MailAddress(TO));
            message.Subject = SUBJECT;
            message.Body = BODY;

            using (var client = new SmtpClient(HOST, PORT))
            {
                client.Credentials = new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);
                client.EnableSsl = true;
                try
                {
                    System.Diagnostics.Debug.WriteLine("Attempting to send email...");
                    client.Send(message);
                    System.Diagnostics.Debug.WriteLine("Email sent!");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("The email was not sent.");
                    System.Diagnostics.Debug.WriteLine("Error message: " + ex.Message);
                }
            }
        }
    }
}

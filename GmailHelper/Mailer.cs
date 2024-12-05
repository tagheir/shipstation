using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Runtime.InteropServices;
using Generics.HelperModels;
using Google.Apis.Gmail.v1;

namespace GmailHelper
{
    public class OutOfStockManager
    {
        public string Sku { get; set; }
        public string Category { get; set; }
        public DateTime LastMail { get; set; }
    }
    public class Mailer
    {
        public static string AttachmentKey = "attachement";
        public static void SendItTwo(GmailService gmail, Dictionary<string, string> dict, string emailAddresses = null)
        {
            MailMessage mailmsg = new MailMessage();
            {
                mailmsg.Subject = dict["subject"];
                mailmsg.Body = dict["body"];
                mailmsg.From = new MailAddress(dict["from"]);
                mailmsg.To.Add(new MailAddress(dict["to"]));

                mailmsg.IsBodyHtml = true;
               
            }
            if (emailAddresses != null)
            {
                mailmsg.To.Add(emailAddresses);
            }

            ////add attachment if specified
            if (dict.ContainsKey(AttachmentKey))
            {
                if (File.Exists(dict[AttachmentKey]))
                {
                    Attachment data = new Attachment(dict[AttachmentKey]);
                    mailmsg.Attachments.Add(data);

                }
                else
                {
                    Console.WriteLine("Error: Invalid Attachemnt");
                }
            }
            //Make mail message a Mime message
            MimeKit.MimeMessage mimemessage = MimeKit.MimeMessage.CreateFromMailMessage(mailmsg);
            Google.Apis.Gmail.v1.Data.Message finalmessage = new Google.Apis.Gmail.v1.Data.Message
            {
                Raw = Base64UrlEncode(mimemessage.ToString())
            };
            var result = gmail.Users.Messages.Send(finalmessage, "me").Execute();
        }
        public static void SendItTwo(GmailService gmail, Dictionary<string, string> dict, List<string> emailAddresses)
        {
            MailMessage mailmsg = new MailMessage();
            {
                mailmsg.Subject = dict["subject"];
                mailmsg.Body = dict["body"];
                mailmsg.From = new MailAddress(dict["from"]);
                mailmsg.To.Add(new MailAddress(dict["to"]));

                mailmsg.IsBodyHtml = true;
            }
            if (emailAddresses != null)
            {
                emailAddresses.ForEach(emailAddress => mailmsg.To.Add(emailAddress));
               
            }

            ////add attachment if specified
            if (dict.ContainsKey(AttachmentKey))
            {
                if (File.Exists(dict[AttachmentKey]))
                {
                    Attachment data = new Attachment(dict[AttachmentKey]);
                    mailmsg.Attachments.Add(data);

                }
                else
                {
                    Console.WriteLine("Error: Invalid Attachemnt");
                }
            }
            //Make mail message a Mime message
            MimeKit.MimeMessage mimemessage = MimeKit.MimeMessage.CreateFromMailMessage(mailmsg);
            Google.Apis.Gmail.v1.Data.Message finalmessage = new Google.Apis.Gmail.v1.Data.Message
            {
                Raw = Base64UrlEncode(mimemessage.ToString())
            };
            var result = gmail.Users.Messages.Send(finalmessage, "me").Execute();
        }
        public static string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes).Replace("+", "-").Replace("/", "_").Replace("=", "");
        }

        public static bool OutOfStockNotifier(InventoryStatusDto inventory)
        {
            
            var subject = $"Restock Alert {inventory.Category} - {inventory.Name} :";
            var message = $"- Item {inventory.Name}\n - Item {inventory.Sku} \n - {inventory.Quantity} Available \n Shopimo.com";


            TimeZoneInfo easternZone;
            TimeZoneInfo easternStandardTime = null;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                easternStandardTime = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                easternStandardTime = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                easternStandardTime = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");
            }
            easternZone = easternStandardTime;
          //  DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);

            var dic = new Dictionary<string, string>();
            dic.Add("subject", subject);
            dic.Add("from", GoogleBase.StatusOfficialEmail);
            dic.Add("to", "lior@shopimo.co");
            dic.Add("body", message);
            SendItTwo(GoogleBase.GetMailService(GoogleBase.StatusOfficialEmail), dic,"daria@shopimo.co");

            return true;

        }

        
        public static bool SendEmail(string subject, string message, string toEmail, 
            List<string> CC = null, string Fromemail = GmailHelper.GoogleBase.StatusOfficialEmail)
        {
            var dic = new Dictionary<string, string>
            {
                { "subject", subject },
                { "from", Fromemail },
                { "to", toEmail },
                { "body", message }
            };
            SendItTwo(GoogleBase.GetMailService(Fromemail), dic,CC);
            return true;
        }
        public static bool SendEmail(string subject, string message, string toEmail,
            List<string> CC , string Fromemail = GmailHelper.GoogleBase.StatusOfficialEmail,string attachment = null)
        {
            var dic = new Dictionary<string, string>
            {
                { "subject", subject },
                { "from", Fromemail },
                { "to", toEmail },
                { "body", message }
            };
            if(attachment != null)
            {
                dic.Add(AttachmentKey, attachment);
            }
            SendItTwo(GoogleBase.GetMailService(Fromemail), dic, CC);
            return true;
        }
    }
}

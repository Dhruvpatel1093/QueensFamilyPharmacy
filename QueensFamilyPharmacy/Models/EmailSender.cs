﻿using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace QueensFamilyPharmacy.Models
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        public EmailSender(IConfiguration config)
        {
            _config = config;
        }
        public void SendEmail(string toEmail, string subject, QuickRefill quickRefill)
        {
            try
            {
                EmailCredentials credential = new EmailCredentials();
                credential.UserName = _config.GetValue<string>("EmailCredentials:UserName");
                credential.Pwd = _config.GetValue<string>("EmailCredentials:Pwd");

                if (toEmail == null)
                {
                    toEmail = credential.UserName;
                }
                else
                {
                    //toEmail = toEmail + ";queensfamilyrx@gmail.com";
                }
                // Set up SMTP client
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(credential.UserName, credential.Pwd);

                // Create email message
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(credential.UserName);
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                foreach (var ToEmail in toEmail.Split(';'))
                {
                    mailMessage.To.Add(ToEmail);
                }
                StringBuilder mailBody = new StringBuilder();
                if (subject == "Queen's Quick Refill")
                {
                    mailBody.AppendFormat("<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"><style type=\"text/css\"></style></head>");
                    mailBody.AppendFormat("<body bgcolor=\"#ffffff\" style=\"margin:0;padding:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none\" yahoo=\"fix\"><img width=\"1\" height=\"1\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td><div align=\"center\"><table width=\"600\" class=\"container\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"font-family:Helvetica,Arial,sans-serif\"><tr><td class=\"mobile-hidden\"><table class=\"container\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\"><tr><td valign=\"middle\" height=\"30\" bgcolor=\"#f2f2f2\" style=\"padding-left:30px;font-family:Helvetica,Arial,sans-serif;font-size:10px\" align=\"left\"><a href=\"#\" target=\"_blank\" style=\"text-decoration:none;color:#000001\"></a></td><td valign=\"middle\" height=\"30\" bgcolor=\"#f2f2f2\" style=\"padding-right:30px;font-family:Helvetica,Arial,sans-serif;font-size:10px\" align=\"right\"><a href=\"#\" target=\"_blank\" style=\"color:#000001;text-decoration:underline\"></a></td></tr></table></td></tr><tr><td><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"container\"><tr><td style=\"font-size:0\"><img src=\"cid:filename\" width=\"1\" height=\"15\" border=\"0\" style=\"display:block\"></td></tr></table></td></tr>");
                    mailBody.AppendFormat("<tr><td><table class=\"container\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\"><tr><td style=\"padding-left:15px;padding-right:20px\"><a href=\"#\" target=\"_blank\"><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"178\" height=\"59\"><rect width=\"100%\" height=\"100%\" fill=\"white\"/></a></td></tr></table></td></tr> ");
                    mailBody.AppendFormat("<tr><td class=\"mobile-only\"><table class=\"container\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"line-height:3px;font-size:3px\" bgcolor=\"#ff724a\">&nbsp;</td></tr></table></td></tr><!--[if !mso]>\r\n<!-- --><tr><td align=\"center\" class=\"mobile-only\"><div class=\"mobile-only\" style=\"font-size:0;max-height:0;overflow:hidden;display:none\"><table class=\"container\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr></tr></table></div></td></tr><!--<![endif]--><tr><td align=\"center\"><table class=\"container\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding-left:20px;padding-right:20px;padding-top:20px;padding-bottom:20px;font-size:24px;font-family:Helvetica,Arial,sans-serif;color:#fff;background-color:#ff724a;line-height:28px\" align=\"center\" class=\"pad20\">We Are Always Ready to Help You & Your Family</td></tr></table></td></tr><tr><td bgcolor=\"#f2f2f2\" align=\"center\" style=\"padding-bottom:20px\"><table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\" class=\"container\"><tr><td style=\"padding-left:20px;padding-right:20px;padding-top:20px;padding-bottom:20px\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\" class=\"container\"><tr><td bgcolor=\"#f2f2f2\" style=\"padding-top:10px;padding-bottom:10px;font-size:20px;font-family:Helvetica,Arial,sans-serif;color:#000\" align=\"left\">Queen's Quick Refill</td></tr></table></td></tr><tr><td align=\"center\" class=\"mobile-hidden\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"right\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">Name :</td><td align=\"left\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">" + quickRefill.QName+"</td></tr><tr><td align=\"right\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">Contact Info :</td><td align=\"left\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">"+quickRefill.QPhone+" , "+quickRefill.QEmail+ "</td></tr><tr><td align=\"right\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">Shipping Method :</td><td align=\"left\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">" + quickRefill.QDeliveryPickup + " , " + quickRefill.QDeliveryPickupDate + "</td></tr><tr><td align=\"right\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">Prescription Numbers :</td><td align=\"left\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">" + quickRefill.QFirst + " , " + quickRefill.QSecond + " , " + quickRefill.QThird + " , " + quickRefill.QFourth + " , " + quickRefill.QFifth + " , " + quickRefill.QSixth + "</td></tr><tr><td align=\"right\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">NOTE :</td><td align=\"left\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">"+quickRefill.QNOTE+"</td></tr></table></td></tr></table></td></tr><tr><td class=\"mobile-hidden\" style=\"padding-top:2px\"><table width=\"600\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" bgcolor=\"#ff724a\"><tr><td bgcolor=\"#ff724a\" style=\"font-size:18px;color:#fff;font-family:Arial,sans-serif;text-decoration:none;padding-top:30px;padding-bottom:10px;padding-left:20px;padding-right:20px\" align=\"left\"><strong>Need Help? Contact Us.</strong><a href=\"\" style=\"color:#fff;text-decoration:none;border:none\">Ph:- +1 705-458-0003</a></td></tr><tr><td bgcolor=\"#ff724a\" style=\"font-size:18px;color:#fff;font-family:Arial,sans-serif;text-decoration:none;padding-top:10px;padding-bottom:10px;padding-left:20px;padding-right:20px\" align=\"left\"><strong>Email:-</strong><a href=\"\" style=\"color:#fff;text-decoration:none;border:none\">queensfamilyrx@gmail.com</a></td></tr><tr><td bgcolor=\"#ff724a\" style=\"font-size:18px;color:#fff;font-family:Arial,sans-serif;text-decoration:none;padding-top:10px;padding-bottom:10px;padding-left:20px;padding-right:20px\" align=\"left\"><strong>Fax:-</strong><a href=\"\" style=\"color:#fff;text-decoration:none;border:none\">705-458-0009</a></td></tr><tr><td bgcolor=\"#ff724a\" style=\"font-size:18px;color:#fff;font-family:Arial,sans-serif;text-decoration:none;padding-top:10px;padding-bottom:30px;padding-left:20px;padding-right:20px\" align=\"left\"><strong>Address:-</strong><a href=\"https://www.google.com/maps?q=1200+Queen%27s+Family+Pharmacy\" style=\"color:#fff;text-decoration:none;border:none\">238 Barrie Street, Thornton,ON L0L 2N0, Canada</a></td></tr></table></td></tr></table></td></tr><tr><td class=\"mobile-hidden\"><table class=\"container\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\"><tr><td valign=\"middle\" height=\"30\" bgcolor=\"#f2f2f2\" style=\"padding-left:30px;font-family:Helvetica,Arial,sans-serif;font-size:10px\" align=\"left\"><a href=\"#\" target=\"_blank\" style=\"text-decoration:none;color:#000001\"></a></td><td valign=\"middle\" height=\"30\" bgcolor=\"#f2f2f2\" style=\"padding-right:30px;font-family:Helvetica,Arial,sans-serif;font-size:10px\" align=\"right\"><a href=\"#\" target=\"_blank\" style=\"color:#000001;text-decoration:underline\"></a></td></tr></table></td></tr></body></html>");


                    //mailBody.AppendFormat("<h2> We Are Always Ready to Help You & Your Family</h2><br />");
                    //mailBody.AppendFormat("<h4> Name -" + quickRefill.QName + "</h4> <br />");
                    //mailBody.AppendFormat("<h4> Contact Information - " + quickRefill.QPhone + " , " + quickRefill.QEmail + "</h4> <br />");
                    //mailBody.AppendFormat("<h4> Shipping Method Information - " + quickRefill.QDeliveryPickup + " , " + quickRefill.QDeliveryPickupDate + "</h4> <br />");
                    //mailBody.AppendFormat("<h4> Prescription Numbers - " + quickRefill.QFirst + " , " + quickRefill.QSecond + " , " + quickRefill.QThird + " , " + quickRefill.QFourth + " , " + quickRefill.QFifth + " , " + quickRefill.QSixth + "</h4> <br />");
                    //mailBody.AppendFormat("<h4> NOTE - " + quickRefill.QNOTE + "</h4> <br />");
                    //mailBody.AppendFormat("<p> Thank you For Quick Refill Request </p>");
                }
                else if (subject == "Request to Transfer Prescription into Queen's Family Pharmacy")
                {
                    mailBody.AppendFormat("<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"><style type=\"text/css\"></style></head>");
                    mailBody.AppendFormat("<body bgcolor=\"#ffffff\" style=\"margin:0;padding:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none\" yahoo=\"fix\"><img width=\"1\" height=\"1\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td><div align=\"center\"><table width=\"600\" class=\"container\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"font-family:Helvetica,Arial,sans-serif\"><tr><td class=\"mobile-hidden\"><table class=\"container\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\"><tr><td valign=\"middle\" height=\"30\" bgcolor=\"#f2f2f2\" style=\"padding-left:30px;font-family:Helvetica,Arial,sans-serif;font-size:10px\" align=\"left\"><a href=\"#\" target=\"_blank\" style=\"text-decoration:none;color:#000001\"></a></td><td valign=\"middle\" height=\"30\" bgcolor=\"#f2f2f2\" style=\"padding-right:30px;font-family:Helvetica,Arial,sans-serif;font-size:10px\" align=\"right\"><a href=\"#\" target=\"_blank\" style=\"color:#000001;text-decoration:underline\"></a></td></tr></table></td></tr><tr><td><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"container\"><tr><td style=\"font-size:0\"><img alt=\"\" width=\"1\" height=\"15\" border=\"0\" style=\"display:block\"></td></tr></table></td></tr>");
                    mailBody.AppendFormat("<tr><td><table class=\"container\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\"><tr><td style=\"padding-left:15px;padding-right:20px\"><a href=\"#\" target=\"_blank\"><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"178\" height=\"59\"><rect width=\"100%\" height=\"100%\" fill=\"white\"/></a></td></tr></table></td></tr> ");
                    mailBody.AppendFormat("<tr><td class=\"mobile-only\"><table class=\"container\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"line-height:3px;font-size:3px\" bgcolor=\"#ff724a\">&nbsp;</td></tr></table></td></tr><!--[if !mso]>\r\n<!-- --><tr><td align=\"center\" class=\"mobile-only\"><div class=\"mobile-only\" style=\"font-size:0;max-height:0;overflow:hidden;display:none\"><table class=\"container\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr></tr></table></div></td></tr><!--<![endif]--><tr><td align=\"center\"><table class=\"container\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding-left:20px;padding-right:20px;padding-top:20px;padding-bottom:20px;font-size:24px;font-family:Helvetica,Arial,sans-serif;color:#fff;background-color:#ff724a;line-height:28px\" align=\"center\" class=\"pad20\">We Are Always Ready to Help You & Your Family</td></tr></table></td></tr><tr><td bgcolor=\"#f2f2f2\" align=\"center\" style=\"padding-bottom:20px\"><table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\" class=\"container\"><tr><td style=\"padding-left:20px;padding-right:20px;padding-top:20px;padding-bottom:20px\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\" class=\"container\"><tr><td bgcolor=\"#f2f2f2\" style=\"padding-top:10px;padding-bottom:10px;font-size:20px;font-family:Helvetica,Arial,sans-serif;color:#000\" align=\"left\">Transfer Prescription To Queen's Family Pharmacy</td></tr></table></td></tr><tr><td align=\"center\" class=\"mobile-hidden\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"right\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">Patient Name :</td><td align=\"left\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">" + quickRefill.QName + ", Date of Birth : " + quickRefill.QDeliveryPickupDate + "</td></tr><tr><td align=\"right\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">Contact Information :</td><td align=\"left\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">" + quickRefill.QPhone + ", " + quickRefill.QEmail + "</td></tr><tr><td align=\"right\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">Current Pharmacy Information :</td><td align=\"left\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">" + quickRefill.TCurrentPharmacyInfo + "</td></tr><tr><td align=\"right\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">Prescription Numbers :</td><td align=\"left\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">" + quickRefill.QFirst + " , " + quickRefill.QSecond + " , " + quickRefill.QThird + " , " + quickRefill.QFourth + " , " + quickRefill.QFifth + " , " + quickRefill.QSixth + "</td></tr><tr><td align=\"right\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">NOTE :</td><td align=\"left\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">" + quickRefill.QNOTE + "</td></tr></table></td></tr></table></td></tr><tr><td class=\"mobile-hidden\" style=\"padding-top:2px\"><table width=\"600\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" bgcolor=\"#ff724a\"><tr><td bgcolor=\"#ff724a\" style=\"font-size:18px;color:#fff;font-family:Arial,sans-serif;text-decoration:none;padding-top:30px;padding-bottom:10px;padding-left:20px;padding-right:20px\" align=\"left\"><strong>Need Help? Contact Us.</strong><a href=\"\" style=\"color:#fff;text-decoration:none;border:none\">Ph:- +1 705-458-0003</a></td></tr><tr><td bgcolor=\"#ff724a\" style=\"font-size:18px;color:#fff;font-family:Arial,sans-serif;text-decoration:none;padding-top:10px;padding-bottom:10px;padding-left:20px;padding-right:20px\" align=\"left\"><strong>Email:-</strong><a href=\"\" style=\"color:#fff;text-decoration:none;border:none\">queensfamilyrx@gmail.com</a></td></tr><tr><td bgcolor=\"#ff724a\" style=\"font-size:18px;color:#fff;font-family:Arial,sans-serif;text-decoration:none;padding-top:10px;padding-bottom:10px;padding-left:20px;padding-right:20px\" align=\"left\"><strong>Fax:-</strong><a href=\"\" style=\"color:#fff;text-decoration:none;border:none\">705-458-0009</a></td></tr><tr><td bgcolor=\"#ff724a\" style=\"font-size:18px;color:#fff;font-family:Arial,sans-serif;text-decoration:none;padding-top:10px;padding-bottom:30px;padding-left:20px;padding-right:20px\" align=\"left\"><strong>Address:-</strong><a href=\"https://www.google.com/maps?q=1200+Queen%27s+Family+Pharmacy\" style=\"color:#fff;text-decoration:none;border:none\">238 Barrie Street, Thornton,ON L0L 2N0, Canada</a></td></tr></table></td></tr></table></td></tr><tr><td class=\"mobile-hidden\"><table class=\"container\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\"><tr><td valign=\"middle\" height=\"30\" bgcolor=\"#f2f2f2\" style=\"padding-left:30px;font-family:Helvetica,Arial,sans-serif;font-size:10px\" align=\"left\"><a href=\"#\" target=\"_blank\" style=\"text-decoration:none;color:#000001\"></a></td><td valign=\"middle\" height=\"30\" bgcolor=\"#f2f2f2\" style=\"padding-right:30px;font-family:Helvetica,Arial,sans-serif;font-size:10px\" align=\"right\"><a href=\"#\" target=\"_blank\" style=\"color:#000001;text-decoration:underline\"></a></td></tr></table></td></tr></body></html>");


                    //mailBody.AppendFormat("<h2> We Are Always Ready to Help You & Your Family</h2><br />");
                    //mailBody.AppendFormat("<h4> Patient Name - " + quickRefill.QName + " ,Date of Birth = " + quickRefill.QDeliveryPickupDate + "</h4> <br />");
                    //mailBody.AppendFormat("<h4> Contact Information - " + quickRefill.QPhone + " , " + quickRefill.QEmail + "</h4> <br />");
                    //mailBody.AppendFormat("<h4> Current Pharmacy Information - " + quickRefill.TCurrentPharmacyInfo + "</h4> <br />");
                    //mailBody.AppendFormat("<h4> Prescription Numbers - " + quickRefill.QFirst + " , " + quickRefill.QSecond + " , " + quickRefill.QThird + " , " + quickRefill.QFourth + " , " + quickRefill.QFifth + " , " + quickRefill.QSixth + "</h4> <br />");
                    //mailBody.AppendFormat("<h4> NOTE - " + quickRefill.QNOTE + "</h4> <br />");
                    //mailBody.AppendFormat("<p> Thank you Requesting to Transfer Prescription into Queen's Family Pharmacy. </p>");
                }
                else if (subject == "Contact With Us")
                {
                    mailBody.AppendFormat("<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"><style type=\"text/css\"></style></head>");
                    mailBody.AppendFormat("<body bgcolor=\"#ffffff\" style=\"margin:0;padding:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none\" yahoo=\"fix\"><img width=\"1\" height=\"1\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td><div align=\"center\"><table width=\"600\" class=\"container\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"font-family:Helvetica,Arial,sans-serif\"><tr><td class=\"mobile-hidden\"><table class=\"container\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\"><tr><td valign=\"middle\" height=\"30\" bgcolor=\"#f2f2f2\" style=\"padding-left:30px;font-family:Helvetica,Arial,sans-serif;font-size:10px\" align=\"left\"><a href=\"#\" target=\"_blank\" style=\"text-decoration:none;color:#000001\"></a></td><td valign=\"middle\" height=\"30\" bgcolor=\"#f2f2f2\" style=\"padding-right:30px;font-family:Helvetica,Arial,sans-serif;font-size:10px\" align=\"right\"><a href=\"#\" target=\"_blank\" style=\"color:#000001;text-decoration:underline\"></a></td></tr></table></td></tr><tr><td><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"container\"><tr><td style=\"font-size:0\"><img alt=\"\" width=\"1\" height=\"15\" border=\"0\" style=\"display:block\"></td></tr></table></td></tr>");
                    mailBody.AppendFormat("<tr><td><table class=\"container\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\"><tr><td style=\"padding-left:15px;padding-right:20px\"><a href=\"#\" target=\"_blank\"><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"178\" height=\"59\"><rect width=\"100%\" height=\"100%\" fill=\"white\"/></a></td></tr></table></td></tr> ");
                    mailBody.AppendFormat("<tr><td class=\"mobile-only\"><table class=\"container\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"line-height:3px;font-size:3px\" bgcolor=\"#ff724a\">&nbsp;</td></tr></table></td></tr><!--[if !mso]>\r\n<!-- --><tr><td align=\"center\" class=\"mobile-only\"><div class=\"mobile-only\" style=\"font-size:0;max-height:0;overflow:hidden;display:none\"><table class=\"container\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr></tr></table></div></td></tr><!--<![endif]--><tr><td align=\"center\"><table class=\"container\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding-left:20px;padding-right:20px;padding-top:20px;padding-bottom:20px;font-size:24px;font-family:Helvetica,Arial,sans-serif;color:#fff;background-color:#ff724a;line-height:28px\" align=\"center\" class=\"pad20\">We Are Always Ready to Help You & Your Family</td></tr></table></td></tr><tr><td bgcolor=\"#f2f2f2\" align=\"center\" style=\"padding-bottom:20px\"><table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\" class=\"container\"><tr><td style=\"padding-left:20px;padding-right:20px;padding-top:20px;padding-bottom:20px\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\" class=\"container\"><tr><td bgcolor=\"#f2f2f2\" style=\"padding-top:10px;padding-bottom:10px;font-size:20px;font-family:Helvetica,Arial,sans-serif;color:#000\" align=\"left\">Thank you for Contacting Us</td></tr></table></td></tr><tr><td align=\"center\" class=\"mobile-hidden\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"center\" class=\"mobile-hidden\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"right\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">Subject :</td><td align=\"left\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">" + quickRefill.QPharmacyLocation + "</td></tr><tr><td align=\"right\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">Name :</td><td align=\"left\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">" + quickRefill.QName + "</td></tr><tr><td align=\"right\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">Contact Information :</td><td align=\"left\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">" + quickRefill.QPhone + ", " + quickRefill.QEmail + "</td></tr><tr><td align=\"right\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">NOTE :</td><td align=\"left\" valign=\"top\" style=\"padding-left:36px;padding-bottom:5px\">" + quickRefill.QNOTE + "</td></tr></table></td></tr></table></td></tr><tr><td class=\"mobile-hidden\" style=\"padding-top:2px\"><table width=\"600\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" bgcolor=\"#ff724a\"><tr><td bgcolor=\"#ff724a\" style=\"font-size:18px;color:#fff;font-family:Arial,sans-serif;text-decoration:none;padding-top:30px;padding-bottom:10px;padding-left:20px;padding-right:20px\" align=\"left\"><strong>Need Help? Contact Us.</strong><a href=\"\" style=\"color:#fff;text-decoration:none;border:none\">Ph:- +1 705-458-0003</a></td></tr><tr><td bgcolor=\"#ff724a\" style=\"font-size:18px;color:#fff;font-family:Arial,sans-serif;text-decoration:none;padding-top:10px;padding-bottom:10px;padding-left:20px;padding-right:20px\" align=\"left\"><strong>Email:-</strong><a href=\"\" style=\"color:#fff;text-decoration:none;border:none\">queensfamilyrx@gmail.com</a></td></tr><tr><td bgcolor=\"#ff724a\" style=\"font-size:18px;color:#fff;font-family:Arial,sans-serif;text-decoration:none;padding-top:10px;padding-bottom:10px;padding-left:20px;padding-right:20px\" align=\"left\"><strong>Fax:-</strong><a href=\"\" style=\"color:#fff;text-decoration:none;border:none\">705-458-0009</a></td></tr><tr><td bgcolor=\"#ff724a\" style=\"font-size:18px;color:#fff;font-family:Arial,sans-serif;text-decoration:none;padding-top:10px;padding-bottom:30px;padding-left:20px;padding-right:20px\" align=\"left\"><strong>Address:-</strong><a href=\"https://www.google.com/maps?q=1200+Queen%27s+Family+Pharmacy\" style=\"color:#fff;text-decoration:none;border:none\">238 Barrie Street, Thornton,ON L0L 2N0, Canada</a></td></tr></table></td></tr></table></td></tr><tr><td class=\"mobile-hidden\"><table class=\"container\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\"><tr><td valign=\"middle\" height=\"30\" bgcolor=\"#f2f2f2\" style=\"padding-left:30px;font-family:Helvetica,Arial,sans-serif;font-size:10px\" align=\"left\"><a href=\"#\" target=\"_blank\" style=\"text-decoration:none;color:#000001\"></a></td><td valign=\"middle\" height=\"30\" bgcolor=\"#f2f2f2\" style=\"padding-right:30px;font-family:Helvetica,Arial,sans-serif;font-size:10px\" align=\"right\"><a href=\"#\" target=\"_blank\" style=\"color:#000001;text-decoration:underline\"></a></td></tr></table></td></tr></body></html>");

                    //mailBody.AppendFormat("<h2> We Are Always Ready to Help You & Your Family</h2><br />");
                    //mailBody.AppendFormat("<h3> Subject - " + quickRefill.QPharmacyLocation + "</h3> <br />");
                    //mailBody.AppendFormat("<h4> Name - " + quickRefill.QName + "</h4> <br />");
                    //mailBody.AppendFormat("<h4> Contact Information - " + quickRefill.QPhone + " , " + quickRefill.QEmail + "</h4> <br />");
                    //mailBody.AppendFormat("<h4> NOTE - " + quickRefill.QNOTE + "</h4> <br />");
                    //mailBody.AppendFormat("<p> Thank you for Contacting Us (Queen's Family Pharmacy) </p>");
                }

                mailMessage.Body = mailBody.ToString();

                // Send email
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

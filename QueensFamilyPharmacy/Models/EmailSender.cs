using System.Net.Mail;
using System.Net;
using System.Text;

namespace QueensFamilyPharmacy.Models
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(string toEmail, string subject, QuickRefill quickRefill)
        {
            // Set up SMTP client
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("dhruvpatel1093@gmail.com", "mdji znwy hkbf cmpx");

            // Create email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("dhruvpatel1093@gmail.com");            
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            foreach (var ToEmail in toEmail.Split(';'))
            {
                mailMessage.To.Add(ToEmail);
            }
            StringBuilder mailBody = new StringBuilder();
            if (subject == "Queen's Quick Refill") { 
            
            mailBody.AppendFormat("<h2> We Are Always Ready to Help You & Your Family</h2><br />");                        
            mailBody.AppendFormat("<h4> Name - " + quickRefill.QName+ "</h4> <br />");
            mailBody.AppendFormat("<h4> Contact Information - " + quickRefill.QPhone + " , " + quickRefill.QEmail + "</h4> <br />");
            mailBody.AppendFormat("<h4> Shipping Method Information - " + quickRefill.QDeliveryPickup + " , " + quickRefill.QDeliveryPickupDate + "</h4> <br />");
            mailBody.AppendFormat("<h4> Prescription Numbers - " + quickRefill.QFirst + " , " + quickRefill.QSecond + " , " + quickRefill.QThird + " , " + quickRefill.QFourth + " , " + quickRefill.QFifth + " , " + quickRefill.QSixth + "</h4> <br />");
            mailBody.AppendFormat("<h4> NOTE - " + quickRefill.QNOTE + "</h4> <br />");
            mailBody.AppendFormat("<p> Thank you For Quick Refill Request </p>");
            }
            else if (subject == "Request to Transfer Prescription into Queen's Family Pharmacy")
            {
                mailBody.AppendFormat("<h2> We Are Always Ready to Help You & Your Family</h2><br />");
                mailBody.AppendFormat("<h4> Patient Name - " + quickRefill.QName + " , Date of Birth = "+ quickRefill.QDeliveryPickupDate + "</h4> <br />");
                mailBody.AppendFormat("<h4> Contact Information - " + quickRefill.QPhone + " , " + quickRefill.QEmail + "</h4> <br />");
                mailBody.AppendFormat("<h4> Current Pharmacy Information - " + quickRefill.TCurrentPharmacyInfo +"</h4> <br />");
                mailBody.AppendFormat("<h4> Prescription Numbers - " + quickRefill.QFirst + " , " + quickRefill.QSecond + " , " + quickRefill.QThird + " , " + quickRefill.QFourth + " , " + quickRefill.QFifth + " , " + quickRefill.QSixth + "</h4> <br />");
                mailBody.AppendFormat("<h4> NOTE - " + quickRefill.QNOTE + "</h4> <br />");
                mailBody.AppendFormat("<p> Thank you Requesting to Transfer Prescription into Queen's Family Pharmacy. </p>");
            }
            else if (subject == "Contact With Us")
            {
                mailBody.AppendFormat("<h2> We Are Always Ready to Help You & Your Family</h2><br />");
                mailBody.AppendFormat("<h3> Subject - " + quickRefill.QPharmacyLocation + "</h3> <br />");
                mailBody.AppendFormat("<h4> Name - " + quickRefill.QName + "</h4> <br />");
                mailBody.AppendFormat("<h4> Contact Information - " + quickRefill.QPhone + " , " + quickRefill.QEmail + "</h4> <br />");
                mailBody.AppendFormat("<h4> NOTE - " + quickRefill.QNOTE + "</h4> <br />");
                mailBody.AppendFormat("<p> Thank you for Contacting With Us (Queen's Family Pharmacy) </p>");
            }

            mailMessage.Body = mailBody.ToString();

            // Send email
            client.Send(mailMessage);
        }
    }
}

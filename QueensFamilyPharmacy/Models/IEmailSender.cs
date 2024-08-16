namespace QueensFamilyPharmacy.Models
{
    public interface IEmailSender
    {
        void SendEmail(string toEmail, string subject, QuickRefill quickRefill);

    }
}

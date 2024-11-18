namespace QueensFamilyPharmacy.Models
{
    public class EmailCredentials
    {        
        public string UserName { get; set; } = String.Empty;
        public string Pwd { get; set; } = String.Empty;
        public string FromEmail { get; set; } = String.Empty;
        public int Port { get; set; }
        public string SmtpServer { get; set; } = string.Empty;
    }
}

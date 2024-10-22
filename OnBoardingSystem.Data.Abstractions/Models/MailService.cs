
namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class MailService
    {
        public string Username { get; set; }

        public bool IsEnableSSL { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }
        public string BaseImageUrL { get; set; }
    }
}
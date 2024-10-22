

using Azure.Core;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class UserRole
    {
        public string? username { get; set; }

        public string? userrole { get; set; }

        public string? DocContent { get; set; }

        public string? RequestNo { get; set; }

        public string? userId { get; set; }

        public string? authenticationType { get; set; }

        public string? securityQuestionId { get; set; }

        public string? securityAnswer { get; set; }

        public string? designation { get; set; }

        public string? emailId { get; set; }

        public string? mobileNo { get; set; }

        public string? Password { get; set; }
    }
}

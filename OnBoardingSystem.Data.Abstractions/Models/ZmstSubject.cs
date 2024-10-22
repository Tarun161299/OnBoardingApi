
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class ZmstSubject
    {
        public string qualificationId { get; set; } = null!;

        public string subjectId { get; set; } = null!;

        public string subjectName { get; set; } = null!;

        public string? alternateNames { get; set; }

        public string QuestionName { get; set; }
    }
}

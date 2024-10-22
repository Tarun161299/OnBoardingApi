using Microsoft.EntityFrameworkCore;

namespace OnBoardingSystem.Common.Models
{

    [Keyless]
    public class MDStatus
    {
        public string? StatusId { get; set; }

        public string? Status { get; set; }
    }
}

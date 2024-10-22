using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class Check_captcha
    {
        public string? key { get; set; }

        public string? hash { get; set; }

    }
}

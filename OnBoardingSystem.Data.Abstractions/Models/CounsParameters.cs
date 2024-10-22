using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class CounsParameters 
    {
        
            private const string _securekey = "EEE7E043A29F6442AA4DA5490EAA28CB";
            private const string _nchmKey = "NCHM2019";

            public static string SecrateKey
            {
                get { return _securekey; }
            }
            public static string NCHMKey
            {
                get { return _nchmKey; }
            }

            public int? ELG { get; set; }

            public int? REG { get; set; }

            public int? TCH { get; set; }

            public int? LCK { get; set; }

            public int? PIS { get; set; }

            public int? TST { get; set; }
    }
}

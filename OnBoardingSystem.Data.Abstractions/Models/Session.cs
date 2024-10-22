using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Data.Abstractions.Models
{
    public class Session
    {
        public List<AppAdminLoginDetails> adminLogIn { get; set; }

        public List<AppUserRoleMapping> AppUserRoleMappingList { get; set; }

       // public List<AppRoleModulePermission> AppRoleModulePermissionList { get; set; }
        public Token token { get; set; }
 
    }
}

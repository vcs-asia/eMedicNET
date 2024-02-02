using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace eMedicEntityModel.Models.v1
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser? User { get; set; }
        public virtual ApplicationRole? Role { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace eMedicEntityModel.Models.v1
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ApplicationUserRole>? UserRoles { get; set; }
    }
}

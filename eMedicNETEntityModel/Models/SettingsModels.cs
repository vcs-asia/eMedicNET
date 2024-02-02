using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;


namespace eMedicNETEntityModel.Models
{
    public class AppIdentitySettings
    {
        public UserSettings User { get; set; } = null!;
        public PasswordSettings Password { get; set; } = null!;
        public LockoutSettings Lockout { get; set; } = null!;
    }

    public class UserSettings
    {
        public bool RequireUniqueEmail { get; set; }
    }

    public class PasswordSettings
    {
        public int RequiredLength { get; set; }
        public bool RequireLowerCase { get; set; }
        public bool RequireUpperCase { get; set; }
        public bool RequireDigit { get; set; }
        public bool RequireNonAlphaNumeric { get; set; }
    }

    public class LockoutSettings
    {
        public bool AllowedForNewUsers { get; set; }
        public int DefaultLockoutTimeSpanInMins { get; set; }
        public int MaxFailedAccessAttempts { get; set; }
    }

    public class SMTPSettings
    {
        public string Host { get; set; } = null!;
        public string User { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Port { get; set; }
        public bool Flag { get; set; }
    }
}

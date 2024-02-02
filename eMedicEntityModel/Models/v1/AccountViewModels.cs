using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eMedicEntityModel.Models.v1
{
    public class ExternalLoginViewModel
    {
        
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }

    public class ForgotPasswordViewModel
    {
        
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }

    public class LoginViewModel
    {
        
        [Display(Name = "Email/Username")]
        public string Email { get; set; } = string.Empty;

        
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }
    }

    public class Login
    {
        
        [Display(Name = "Email/Username")]
        public string Email { get; set; } = string.Empty;

        
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }

    public class LoginWith2faViewModel
    {
        
        [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Authenticator code")]
        public string TwoFactorCode { get; set; } = string.Empty;

        [Display(Name = "Remember this machine")]
        public bool RememberMachine { get; set; }

        public bool RememberMe { get; set; }
    }

    public class LoginWithRecoveryCodeViewModel
    {
        
        [DataType(DataType.Text)]
        [Display(Name = "Recovery Code")]
        public string RecoveryCode { get; set; } = string.Empty;
    }

    public class RegisterViewModel
    {
        
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        
        [Display(Name = "Username")]
        public string Username { get; set; } = string.Empty;

        
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirm password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Range(typeof(bool), "true", "true", ErrorMessage = "Please accept the terms and conditions to proceed")]
        [Display(Name = "Terms Action")]
        public bool AcceptTerms { get; set; }

    }

    public class ResetPasswordViewModel
    {
        
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirm password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;
    }

    public class UserViewModel
    {
        public string UsrAutid { get; set; } = string.Empty;
        public string UsrEmail { get; set; } = string.Empty;
        public string UsrUname { get; set; } = string.Empty;
        public string UsrPaswd { get; set; } = string.Empty;
    }
}

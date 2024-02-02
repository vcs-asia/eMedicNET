using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eMedicEntityModel.Models.v1
{
    public class ChangePasswordViewModel
    {
        
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; } = string.Empty;

        
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string StatusMessage { get; set; } = string.Empty;
    }

    public class EnableAuthenticatorViewModel
    {
        
        [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Verification Code")]
        public string Code { get; set; } = string.Empty;

        public string SharedKey { get; set; } = string.Empty;

        public string AuthenticatorUri { get; set; } = string.Empty;
    }

    public class IndexViewModel
    {
        public string Username { get; set; } = string.Empty;
        public bool IsEmailConfirmed { get; set; }

        
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        public string StatusMessage { get; set; } = string.Empty;
    }
    public class RemoveLoginViewModel
    {
        public string LoginProvider { get; set; } = string.Empty;
        public string ProviderKey { get; set; } = string.Empty;
    }
    public class SetPasswordViewModel
    {
        
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string StatusMessage { get; set; } = string.Empty;
    }

    public class ShowRecoveryCodesViewModel
    {
        public string[] RecoveryCodes { get; set; } = new string[0];
    }

    public class TwoFactorAuthenticationViewModel
    {
        public bool HasAuthenticator { get; set; }
        public int RecoveryCodesLeft { get; set; }
        public bool Is2faEnabled { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace JjOnlineStore.Common.ViewModels.Account
{
    public class CredentialsModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
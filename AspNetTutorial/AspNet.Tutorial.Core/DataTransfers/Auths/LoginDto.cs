using System.ComponentModel.DataAnnotations;

namespace AspNet.Tutorial.Core.DataTransfers.Auths
{
    public class LoginDto
    {
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)] public string Password { get; set; }
    }
}

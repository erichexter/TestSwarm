using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace nTestSwarm.Controllers
{
    public class LoginInput
    {
        [Email]
        [Required]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DataAnnotationsExtensions;

namespace nTestSwarm.Controllers
{
    public class NewUserInput
    {
        [Required]
        [Email]
        public string Username { get; set; }
        
        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name="Repeat Password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare( "Password",ErrorMessage = "Value must match the Password field.")]
        public string PasswordRepeated { get; set; }

   
    }
}
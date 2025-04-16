using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class RegModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

    }
}

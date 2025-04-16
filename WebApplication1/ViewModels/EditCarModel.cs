using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class EditCarModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Mark { get; set; }

        [Required]
        public string Year { get; set; }
    }
}

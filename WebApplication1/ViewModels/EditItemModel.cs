using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class EditItemModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int CarId { get; set; }
        [Required]
        public string Seller { get; set; }
    }
}

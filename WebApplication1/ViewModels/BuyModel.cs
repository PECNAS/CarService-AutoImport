using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
	public class BuyModel
	{
		[Required]
		public int ItemId { get; set; }
		[Required]
		public int Counter { get; set; }
	}
}

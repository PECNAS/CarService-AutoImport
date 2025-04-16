using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace WebApplication1.ViewModels
{
	public class PlaceOrderModel
	{
		[Required]
		public int PickPointId { get; set; }
		[Required]
		public double Price { get; set; }

		[Required]
		public string MyAdress { get; set; }
	}
}

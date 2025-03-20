using ParkingService.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace ParkingService.Application.Common.DTO.Slot
{
	public class AddSlotDTO
	{
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Type is required")]
		public string Type { get; set; }
	}
}

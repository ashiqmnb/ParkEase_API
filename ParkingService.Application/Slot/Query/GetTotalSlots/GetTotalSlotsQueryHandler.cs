using MediatR;
using ParkingService.Domain.Repository;

namespace ParkingService.Application.Slot.Query.GetTotalSlots
{
	public class GetTotalSlotsQueryHandler : IRequestHandler<GetTotalSlotsQuery, int>
	{
		private readonly ISlotRepo? _slotRepo;

		public GetTotalSlotsQueryHandler(ISlotRepo? slotRepo)
		{
			_slotRepo = slotRepo;
		}

		public async Task<int> Handle(GetTotalSlotsQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var slots = await _slotRepo.GetAllSlots();
				slots = slots.Where(s => s.IsDeleted == false).ToList();
				return slots.Count;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}

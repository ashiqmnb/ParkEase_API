using MediatR;
using ParkingService.Application.Common.DTO.Slot;
using ParkingService.Domain.Entity;
using ParkingService.Domain.Repository;

namespace ParkingService.Application.Slot.Query.GetSlotsByCompanyId
{
	public class GetSlotsByCompanyIdCommandHandler : IRequestHandler<GetSlotsByCompanyIdCommand, SlotResDetailsDTO>
	{
		private readonly ISlotRepo _slotRepo;

		public GetSlotsByCompanyIdCommandHandler(ISlotRepo slotRepo)
		{
			_slotRepo = slotRepo;
		}

		public async Task<SlotResDetailsDTO> Handle(GetSlotsByCompanyIdCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var companySlots = await _slotRepo.GetSlotsByCompanyId(request.CompanyId);
				if (companySlots.Count < 1) throw new Exception("No listed slots");

				var res = new SlotResDetailsDTO
				{
					Total = companySlots.Count,
					TwoWheeler = companySlots.Count(s => s.Type == SlotType.TwoWheeler),
					FourWheeler = companySlots.Count(s => s.Type == SlotType.FourWheeler),

					Available = companySlots.Count(s => s.Status == SlotStatus.Available),
					Reserved = companySlots.Count(s => s.Status == SlotStatus.Reserved),
					Parked = companySlots.Count(s => s.Status == SlotStatus.Parked),
					Slots = companySlots.Select(s => new SlotResDTO
					{
						Id = s.Id,
						Name = s.Name,
						Status = s.Status.ToString(),
						Type = s.Type.ToString(),
						UserId = s.UserId,
						VehicleNumber = s.VehicleNumber
					}).ToList()
				};

				return res;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}

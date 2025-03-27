using MediatR;
using ParkingService.Application.Common.DTO.Slot;
using ParkingService.Domain.Repository;

namespace ParkingService.Application.Slot.Query.GetSlotsCountByCompanyId
{
	public class GetSlotsCountByCompanyIdCommandHandler : IRequestHandler<GetSlotsCountByCompanyIdCommand, SlotsCountResDTO>
	{
		private readonly ISlotRepo _slotRepo;

		public GetSlotsCountByCompanyIdCommandHandler(ISlotRepo slotRepo)
		{
			_slotRepo = slotRepo;
		}

		public async Task<SlotsCountResDTO> Handle(GetSlotsCountByCompanyIdCommand request, CancellationToken cancellationToken)
		{
			try
			{
				if (request.CompanyId == null) throw new Exception("Company Id not found");

				var slots = await _slotRepo.GetSlotsByCompanyId(request.CompanyId);

				if (slots == null) throw new Exception("No slots listed");

				var res = new SlotsCountResDTO
				{
					Total = slots.Count(),
					TwoWheeler = slots.Where(s => s.Type == Domain.Entity.SlotType.TwoWheeler).Count(),
					FourWheeler = slots.Where(s => s.Type == Domain.Entity.SlotType.FourWheeler).Count()
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

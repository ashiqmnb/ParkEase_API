using MassTransit.Transports;
using MediatR;
using ParkingService.Application.Common.DTO.Slot;
using ParkingService.Domain.Repository;

namespace ParkingService.Application.Slot.Query.GetReservationsParkings
{
	public class GetReservationsParkingsCommandHandler : IRequestHandler<GetReservationsParkingsCommand, ReservParkResDTO>
	{
		private readonly ISlotRepo _slotRepo;

		public GetReservationsParkingsCommandHandler(ISlotRepo slotRepo)
		{
			_slotRepo = slotRepo;
		}


		public async Task<ReservParkResDTO> Handle(GetReservationsParkingsCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var slots = await _slotRepo.GetSlotByUserId(request.UserId);

				var reservations = slots
					.Where(s => s.Status == Domain.Entity.SlotStatus.Reserved)
					.Select(s=> new ReservationList
					{
						SlotId = s.Id.ToString(),
						Name = s.Name,
						Status = s.Status.ToString(),
						Type = s.Type.ToString(),
						VehicleNumber = s.VehicleNumber
					})
					.ToList() ?? new List<ReservationList> ();

				var parkings = slots
					.Where(s => s.Status == Domain.Entity.SlotStatus.Parked)
					.Select(s => new ParkingList
					{
						SlotId = s.Id.ToString(),
						Name = s.Name,
						Status = s.Status.ToString(),
						Type = s.Type.ToString(),
						HistoryId = s.CurrentHistoryId,
						VehicleNumber = s.VehicleNumber
					})
					.ToList() ?? new List<ParkingList> ();


				var res = new ReservParkResDTO
				{
					Parkings = parkings.Count > 0 ? parkings : new List<ParkingList>(),
					Reservations = reservations.Count > 0 ? reservations : new List<ReservationList>()
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

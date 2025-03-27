using MediatR;
using ParkingService.Domain.Entity;
using ParkingService.Domain.Repository;


namespace ParkingService.Application.Slot.Command.AddSlot
{
    public class AddSlotCommandHandler : IRequestHandler<AddSlotCommand, bool>
    {
        private readonly ISlotRepo _slotRepo;

        public AddSlotCommandHandler(ISlotRepo slotRepo)
        {
            _slotRepo = slotRepo;
        }

        public async Task<bool> Handle(AddSlotCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AddSlotDto == null)
                {
                    throw new Exception("Slot details cannot to be null");
                }

                var companySlots = await _slotRepo.GetSlotsByCompanyId(request.CompanyId);
                var existingName = companySlots.FirstOrDefault(s => s.Name == request.AddSlotDto.Name);

                if (existingName != null) throw new Exception("Name already taken");

                SlotType type;
                if (request.AddSlotDto.Type == "TwoWheeler")
                {
                    type = SlotType.TwoWheeler;
                }
                else
                {
                    type = SlotType.FourWheeler;
                }

                var newSlot = new Domain.Entity.Slot
                {
                    Name = request.AddSlotDto.Name,
                    CompanyId = request.CompanyId,
                    Status = SlotStatus.Available,
                    Type = type,
                    CreatedBy = request.CompanyId,
                    CreatedOn = DateTime.UtcNow
                };

                await _slotRepo.AddSlot(newSlot);
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}

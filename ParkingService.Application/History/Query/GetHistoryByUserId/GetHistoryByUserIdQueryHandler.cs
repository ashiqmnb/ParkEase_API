using MediatR;
using ParkingService.Application.Common.DTO.History;
using ParkingService.Domain.Repository;

namespace ParkingService.Application.History.Query.GetHistoryByUserId
{
	public class GetHistoryByUserIdQueryHandler : IRequestHandler<GetHistoryByUserIdQuery, UserHistoryPageResDTO>
	{
		private readonly IHistoryRepo _historyRepo;

		public GetHistoryByUserIdQueryHandler(IHistoryRepo historyRepo)
		{
			_historyRepo = historyRepo;
		}

		public async Task<UserHistoryPageResDTO> Handle(GetHistoryByUserIdQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var histories = await _historyRepo.GetHistoryByUserId(request.UserID);

				var res = histories.
					Where(h => h.CheckOut != null)
					.Select(h => new UserHistoryResDTO
					{
						SlotName = h.SlotName,
						CheckIn = h.CheckIn,
						CheckOut = h.CheckOut,
						CompanyId = h.CompanyId,
						VehicleNumber = h.VehicleNumber,
					}).ToList() ?? new List<UserHistoryResDTO>();

				int totalPages = (int)Math.Ceiling((double)res.Count / request.PageSize);

				res = res.
					Skip((request.PageNumber - 1) * (request.PageSize))
					.Take(request.PageSize)
					.ToList();

				var historiesRes = new UserHistoryPageResDTO
				{
					CurrentPage = request.PageNumber,
					TotalPages = totalPages,
					Histories = res
				};

				return historiesRes;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}

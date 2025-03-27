using MediatR;
using ParkingService.Application.Common.DTO.History;

namespace ParkingService.Application.History.Query.GetHistoryByUserId
{
	public class GetHistoryByUserIdQuery : IRequest<UserHistoryPageResDTO>
	{
		public string UserID { get; set; }
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 10;
	}
}

using MediatR;

namespace UserService.Application.Coin.Query.GetCurrentCoins
{
	public class GetCurrentCoinsCommand : IRequest<int>
	{
		public string Id { get; set; }
	}
}

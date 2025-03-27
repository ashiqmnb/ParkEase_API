using MediatR;
using UserService.Application.Common.DTOs.Address;

namespace UserService.Application.Coin.Query.GetCoinStatus
{
    public class GetCoinStatusCommand : IRequest<CoinResDTO>
    {
    }
}

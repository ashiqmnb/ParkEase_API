using MediatR;
using UserService.Application.Common.DTOs.Address;
using UserService.Domain.Repository;

namespace UserService.Application.Coin.Query.GetCoinStatus
{
    public class GetCoinStatusCommandHandler : IRequestHandler<GetCoinStatusCommand, CoinResDTO>
    {
        private readonly IUserRepo _userRepo;
        private readonly ICompanyRepo _companyRepo;

        public GetCoinStatusCommandHandler(IUserRepo userRepo, ICompanyRepo companyRepo)
        {
            _userRepo = userRepo;
            _companyRepo = companyRepo;
        }

        public async Task<CoinResDTO> Handle(GetCoinStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _userRepo.GetAllUsers();
                var admins = await _userRepo.GetAllAdmins();
                var companies = await _companyRepo.GetAllCompanies();

                int adminCoins = admins.Sum(a => a.Coins);
                int userCoins = users.Sum(a => a.Coins);
                int companyCoins = companies.Sum(a => a.Coins);


                var res = new CoinResDTO
                {
                    Admin = adminCoins,
                    Company = companyCoins,
                    User = userCoins,
                    total = adminCoins + userCoins + companyCoins,
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

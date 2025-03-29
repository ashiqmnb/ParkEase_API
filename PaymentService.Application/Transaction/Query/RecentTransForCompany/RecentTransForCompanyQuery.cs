using MediatR;
using PaymentService.Application.Common.DTOs.Transaction;

namespace PaymentService.Application.Transaction.Query.RecentPaymentsForCompany
{
    public class RecentTransForCompanyQuery : IRequest<List<CompanyTransResDTO>>
    {
        public string CompanyId { get; set; }
    }
}

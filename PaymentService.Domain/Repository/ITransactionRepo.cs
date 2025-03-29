using PaymentService.Domain.Entity;

namespace PaymentService.Domain.Repository
{
	public interface ITransactionRepo
	{
		Task<int>AddTransaction (Transaction transaction);
		Task<int> SaveChangesAsyncCustom();
		Task<List<Transaction>> GetTransactionByCompanyId(string companyId);
		Task<List<Transaction>> GetAllTransactions();
		Task<List<Transaction>> GetTransMadeByCompanyId(string companyId);
	}
}

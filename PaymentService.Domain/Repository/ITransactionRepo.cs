using PaymentService.Domain.Entity;

namespace PaymentService.Domain.Repository
{
	public interface ITransactionRepo
	{
		Task<int>AddTransaction (Transaction transaction);
		Task<int> SaveChangesAsyncCustom();
	}
}

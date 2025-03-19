using PaymentService.Domain.Entity;

namespace PaymentService.Domain.Repository
{
	public interface IPaymentRepo
	{
		Task<int> AddPayment(Payment payment);
		Task<Payment> GetPaymentByTransId(string transId);
		Task<int> SaveChangesAsyncCustom();
	}
}

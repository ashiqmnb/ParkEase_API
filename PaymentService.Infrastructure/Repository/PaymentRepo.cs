using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Entity;
using PaymentService.Domain.Repository;
using PaymentService.Infrastructure.Data;

namespace PaymentService.Infrastructure.Repository
{
	public class PaymentRepo : IPaymentRepo
	{
		private readonly PaymentDbContext _paymentDbContext;

		public PaymentRepo(PaymentDbContext paymentDbContext)
		{
			_paymentDbContext = paymentDbContext;
		}

		public async Task<int> AddPayment(Payment payment)
		{
			try
			{
				await _paymentDbContext.AddAsync(payment);
				return await _paymentDbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<List<Payment>> GetAllPayments()
		{
			try
			{
				var payments = await _paymentDbContext.Payments
					.Where(p => p.IsDeleted == false)
					.OrderByDescending(p => p.CreatedOn)
					.ToListAsync();

				return payments;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<Payment> GetPaymentByTransId(string transId)
		{
			try
			{
				var payment = await _paymentDbContext.Payments
					.FirstOrDefaultAsync(p => p.TransactionId == transId);
				return payment;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<int> SaveChangesAsyncCustom()
		{
			try
			{
				return await _paymentDbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}

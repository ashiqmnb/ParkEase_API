using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Entity;
using PaymentService.Domain.Repository;
using PaymentService.Infrastructure.Data;

namespace PaymentService.Infrastructure.Repository
{
	public class TransactionRepo : ITransactionRepo
	{
		private readonly PaymentDbContext _paymentDbContext;

		public TransactionRepo(PaymentDbContext paymentDbContext)
		{
			_paymentDbContext = paymentDbContext;
		}

		public async Task<int> AddTransaction(Transaction transaction)
		{
			try
			{
				await _paymentDbContext.Transactions.AddAsync(transaction);
				return await _paymentDbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException.Message ?? ex.Message);
			}
		}

		public async Task<List<Transaction>> GetTransactionByCompanyId(string companyId)
		{
			try
			{
				var trnsactions = await _paymentDbContext.Transactions
					.Where(t => t.ReceiverId.ToString() == companyId)
					.OrderByDescending(p => p.CreatedOn)
					.ToListAsync();
				return trnsactions;
			}
			catch(Exception ex)
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

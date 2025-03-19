using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Entity;

namespace PaymentService.Infrastructure.Data
{
	public class PaymentDbContext : DbContext
	{

		public PaymentDbContext(DbContextOptions<PaymentDbContext> dbContextOptions)
			: base(dbContextOptions)
		{
		}

		public DbSet<Transaction> Transactions { get; set; }
		public DbSet<Payment> Payments { get; set; }


	}
}

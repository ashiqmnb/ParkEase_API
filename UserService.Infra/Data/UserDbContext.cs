using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entity;

namespace UserService.Infrastructure.Data
{
	public class UserDbContext : DbContext
	{
		public DbSet<Admin> Admins { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<VerifyUser> VerifyUsers { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Company>()
				.HasOne(c => c.Address)
				.WithOne(a => a.Company)
				.HasForeignKey<Company>(c => c.AddressId);

			base.OnModelCreating(modelBuilder);
		}


		public UserDbContext(DbContextOptions<UserDbContext> dbContextOptions)
			: base(dbContextOptions)
		{
		}
	}
}

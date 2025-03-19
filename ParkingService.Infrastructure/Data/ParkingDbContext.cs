using Microsoft.EntityFrameworkCore;
using ParkingService.Domain.Entity;

namespace ParkingService.Infrastructure.Data
{
	public class ParkingDbContext : DbContext
	{

		public DbSet<Slot> Slots { get; set; }
		public DbSet<History> Histories { get; set; }



		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<History>()
				.HasOne(h => h.Slot)
				.WithMany(s => s.Histories)
				.HasForeignKey(h => h.SlotId);

			base.OnModelCreating(modelBuilder);
		}

		public ParkingDbContext(DbContextOptions<ParkingDbContext> dbContextOptions)
			: base(dbContextOptions)
		{
		}
	}
}

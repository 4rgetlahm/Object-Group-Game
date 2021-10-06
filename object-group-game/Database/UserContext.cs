using Microsoft.EntityFrameworkCore;

namespace object_group_game.Database
{
	public class UserContext : DbContext
	{
		public DbSet<Login> Login { get; set; }

		protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
		{
			// SQL server connection string
			// Set user and password values appropriate to your server settings
			optionsBuilder.UseMySQL("server=localhost;database=object-group;user=;password=");
		}

		protected override void OnModelCreating (ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Login>(entity => {
				entity.HasKey(e => e.Username);
				entity.Property(e => e.Password).IsRequired();
			});
		}
	}
}

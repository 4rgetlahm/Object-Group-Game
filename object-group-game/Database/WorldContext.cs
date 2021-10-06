using Microsoft.EntityFrameworkCore;

namespace object_group_game.Database
{
	public class WorldContext : DbContext
	{
		public DbSet<Location> Location { get; set; }
		public DbSet<Item> Item { get; set; }
		public DbSet<Effect> Effect { get; set; }
		public DbSet<ItemEffects> ItemEffects { get; set; }

		protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
		{
			// SQL server connection string
			// Set user and password values appropriate to your server settings
			optionsBuilder.UseMySQL("server=localhost;database=object-group;user=;password=");
		}

		protected override void OnModelCreating (ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Location>(entity => {
				entity.HasKey(e => e.ID);
				entity.Property(e => e.Latitude).IsRequired();
				entity.Property(e => e.Longtitude).IsRequired();
				entity.Property(e => e.Radius).IsRequired();
				entity.Property(e => e.ItemID).IsRequired();
				entity.Property(e => e.DisplayName).IsRequired();
			});

			modelBuilder.Entity<Item>(entity => {
				entity.HasKey(e => e.ID);
			});

			modelBuilder.Entity<Effect>(entity => {
				entity.HasKey(e => e.ID);
				entity.Ignore(e => e.Value);
			});

			modelBuilder.Entity<ItemEffects>(entity => {
				entity.HasKey(o => new { o.ItemID, o.EffectID });
			});
		}
	}
}

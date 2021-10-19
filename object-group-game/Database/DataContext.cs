using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace object_group_game.Database
{
	public class DataContext : DbContext
	{
		public DataContext()
		{
			//use when want to add tables
			//Database.EnsureDeleted();
			Database.EnsureCreated();
		}

		public DbSet<Character> Character { get; set; }
		public DbSet<Location> Location { get; set; }
		public DbSet<Item> Item { get; set; }
		public DbSet<Effect> Effect { get; set; }
		public DbSet<Player> Player { get; set; }

		protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
		{
			// SQL server connection string
			// Set user and password values appropriate to your server settings
			optionsBuilder.UseMySQL(
				"server=" + Configuration.GetInstance().Settings["server"]
				+ ";database=" + Configuration.GetInstance().Settings["database"]
				+ ";user=" + Configuration.GetInstance().Settings["user"]
				+ ";password=" + Configuration.GetInstance().Settings["password"]
				+ ";SslMode=" + Configuration.GetInstance().Settings["sslmode"]
			);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Player>().HasOne(c => c.Character);

			modelBuilder.Entity<Character>().HasMany(i => i.Items);
			modelBuilder.Entity<Item>().HasMany(c => c.Characters);

			modelBuilder.Entity<Character>().HasMany(l => l.VisitedLocations);
			modelBuilder.Entity<Location>().HasMany(c => c.Characters);


			modelBuilder.Entity<Item>().HasMany(e => e.Effects);
			modelBuilder.Entity<Effect>().HasMany(i => i.Items);

			modelBuilder.Entity<Player>().Property<string>("Password");
			modelBuilder.Entity<Player>().Property<string>("Salt");

			base.OnModelCreating(modelBuilder);
        }
	}
}

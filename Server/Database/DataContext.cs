using GameLibrary.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace GameLibrary.Database
{
	public class DataContext : DbContext
	{
		public DataContext()
		{
			//use when want to add tables
			//Database.EnsureDeleted();
			Database.EnsureCreated();
			this.ChangeTracker.LazyLoadingEnabled = true;
		}

		public DbSet<Character> Character { get; set; }
		public DbSet<Location> Location { get; set; }
		public DbSet<Item> Item { get; set; }
		public DbSet<Effect> Effect { get; set; }
		public DbSet<Player> Player { get; set; }
		public DbSet<Mission> Mission { get; set; }
		public DbSet<Expedition> Expedition { get; set; }

		public DbSet<Equipment> Equipment { get; set; }

		public void Replace<TEntity>(TEntity oldEntity, TEntity newEntity) where TEntity : class
		{
			ChangeTracker.TrackGraph(oldEntity, e => e.Entry.State = EntityState.Deleted);
			ChangeTracker.TrackGraph(newEntity, e => e.Entry.State = e.Entry.IsKeySet ? EntityState.Modified : EntityState.Added);
		}

		protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
		{
			// SQL server connection string
			// Set user and password values appropriate to your server settings
			optionsBuilder.UseMySQL(
				"server=" + Configuration.GetInstance().Settings["server"]
				+ ";database=" + Configuration.GetInstance().Settings["database"]
				+ ";user=" + Configuration.GetInstance().Settings["user"]
				+ ";password=" + Configuration.GetInstance().Settings["password"]
				+ ";port=" + Configuration.GetInstance().Settings["port"]
			);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Player>().HasOne(c => c.Character);

			modelBuilder.Entity<Character>().HasMany(i => i.Items);
			modelBuilder.Entity<Item>().HasMany(c => c.Characters);

			modelBuilder.Entity<Character>().HasMany(l => l.VisitedLocations);
			modelBuilder.Entity<Location>().HasMany(c => c.Characters);

			modelBuilder.Entity<Character>().HasOne(e => e.Equipment);

			modelBuilder.Entity<Item>().HasMany(e => e.Effects);
			modelBuilder.Entity<Effect>().HasMany(i => i.Items);

			modelBuilder.Entity<Player>().Property<string>("Password");
			modelBuilder.Entity<Player>().Property<string>("Salt");

			modelBuilder.Entity<Location>().Property(c => c.Coordinate)
				.HasConversion(
					v => JsonSerializer.Serialize(v, null),
					v => JsonSerializer.Deserialize<Coordinate>(v, null)
				);
			modelBuilder.Entity<Location>().HasMany(m => m.Missions);

			modelBuilder.Entity<Mission>().Property(m => m.MinDuration).HasConversion(new TimeSpanToTicksConverter());
			modelBuilder.Entity<Mission>().Property(m => m.MaxDuration).HasConversion(new TimeSpanToTicksConverter());

			modelBuilder.Entity<Expedition>().Property(d => d.Duration).HasConversion(new TimeSpanToTicksConverter());

			base.OnModelCreating(modelBuilder);
        }
	}
}

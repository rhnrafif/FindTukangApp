using FindTheBuilder.Databases.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBuilder.Databases
{
	public class AppDbContext: DbContext
	{
		public DbSet<Auth> auths { get; set; }
		public DbSet<Customers>	Customers { get; set; }
		public DbSet<Tukang> Tukang { get; set; }
		public DbSet<Skills> Skills { get; set; }
		public DbSet<Prices> Prices { get; set; }
		public DbSet<TransactionDetails> TransactionDetails { get; set; }
		public DbSet<Transactions> Transactions { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext>options):base(options) { }

		protected override void OnModelCreating(ModelBuilder mod)
		{
			mod.Entity<Skills>().HasData(
				new { Id = 1, Name = "House" },
				new { Id = 2, Name = "Garden" },
				new { Id = 3, Name = "Pool" }
				);
		}
		
	}
}

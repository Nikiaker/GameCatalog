using Mackowiak.GameCatalog.Core;
using Mackowiak.GameCatalog.DAO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mackowiak.GameCatalog.DAO
{
    public class GameCatalogDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Producer> Producers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=GameCatalog.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Producer)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.ProducerId);

            modelBuilder.Entity<Producer>().HasData(
                new Producer { Id = 1, Name = "CD Projekt Red" },
                new Producer { Id = 2, Name = "Ubisoft" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "The Witcher 3", Genre = GameGenre.RPG, ProducerId = 1 },
                new Product { Id = 2, Name = "Assassin's Creed", Genre = GameGenre.Adventure, ProducerId = 2 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}

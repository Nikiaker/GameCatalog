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
        public DbSet<Game> Games { get; set; }
        public DbSet<Developer> Developers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=GameCatalog.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasOne(p => p.Developer)
                .WithMany(p => p.Games)
                .HasForeignKey(p => p.DeveloperId);

            modelBuilder.Entity<Developer>().HasData(
                new Developer { Id = 1, Name = "CD Projekt Red" },
                new Developer { Id = 2, Name = "Ubisoft" }
            );

            modelBuilder.Entity<Game>().HasData(
                new Game { Id = 1, Name = "The Witcher 3", Genre = GameGenre.RPG, DeveloperId = 1 },
                new Game { Id = 2, Name = "Assassin's Creed", Genre = GameGenre.Adventure, DeveloperId = 2 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}

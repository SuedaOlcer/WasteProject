using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WasteProject.Data.Entities;

namespace WasteProject.Data
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<WasteTypes> WasteTypes { get; set; }
        public DbSet<Gifts> Gifts { get; set; }
        public DbSet<UserPoints> UserPoints { get; set; }
        public DbSet<WasteTransaction> WasteTransaction { get; set; }
        public DbSet<UserGifts> UserGifts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
 
            modelBuilder.Entity<Users>(entity => {
                entity.HasIndex(e => e.Tc).IsUnique();
            });

            modelBuilder.Entity<WasteTypes>().ToTable("WasteTypes");
            modelBuilder.Entity<Gifts>().ToTable("Gifts");
            modelBuilder.Entity<UserPoints>().ToTable("UserPoints");
            modelBuilder.Entity<WasteTransaction>().ToTable("WasteTransaction");
            modelBuilder.Entity<UserGifts>().ToTable("UserGifts");

 

        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TarOh.Models;


namespace TarOh.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Card> Card { get; set; }
        public DbSet<CardComment> CardComment { get; set; }
        public DbSet<CardType> CardType { get; set; }
        public DbSet<Deck> Deck { get; set; }
        public DbSet<OrdinalComment> OrdinalComment { get; set; }
        public DbSet<OrdinalPosition> OrdinalPosition { get; set; }
        public DbSet<SavedSpread> SavedSpread { get; set; }
        public DbSet<Spread> Spread { get; set; }
        public DbSet<SpreadComment> SpreadComment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Spread>()
            .Property(b => b.ReadingDate)
            .HasDefaultValueSql("GETDATE()");
        }

   
    }
}

using FootbalTeams.Models.ORM;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.Emit;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace FootbalTeams.Contexts
{
    public class FootbalContext : DbContext
    {

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<Team> Teams { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Player entity configuration
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId);

            modelBuilder.Entity<Player>()
                .HasOne(p => p.City)
                .WithMany(c => c.Players)
                .HasForeignKey(p => p.CityId);

            // Team entity configuration
            modelBuilder.Entity<Team>()
                .HasOne(t => t.City)
                .WithMany(c => c.Teams)
                .HasForeignKey(t => t.CityId);

            modelBuilder.Entity<Team>()
                .HasOne(t => t.Stadium)
                .WithOne(s => s.Team)
                .HasForeignKey<Team>(t => t.StadiumId);

            // Stadium entity configuration
            modelBuilder.Entity<Stadium>()
                .HasOne(s => s.City)
                .WithMany(c => c.Stadiums)
                .HasForeignKey(s => s.CityId);

            modelBuilder.Entity<Stadium>()
                .HasOne(s => s.Team)
                .WithOne(t => t.Stadium)
                .HasForeignKey<Stadium>(s => s.TeamId);

            // City entity configuration
            modelBuilder.Entity<City>()
                .HasMany(c => c.Teams)
                .WithOne(t => t.City)
                .HasForeignKey(t => t.CityId);

            modelBuilder.Entity<City>()
                .HasMany(c => c.Players)
                .WithOne(p => p.City)
                .HasForeignKey(p => p.CityId);

            modelBuilder.Entity<City>()
                .HasMany(c => c.Stadiums)
                .WithOne(s => s.City)
                .HasForeignKey(s => s.CityId);

            modelBuilder.Entity<City>()
                .HasOne(c => c.Country)
                .WithMany(ct => ct.Cities)
                .HasForeignKey(c => c.CountryId);

            // Country entity configuration
            modelBuilder.Entity<Country>()
                .HasMany(ct => ct.Cities)
                .WithOne(c => c.Country)
                .HasForeignKey(c => c.CountryId);

            base.OnModelCreating(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string ConnetionString = "server=.;database=footbalteams6;trusted_connection=true";
                optionsBuilder.UseSqlServer(ConnetionString);
            }
        }

   


    }
}

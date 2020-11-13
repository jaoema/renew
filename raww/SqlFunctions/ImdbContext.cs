using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SqlFunctions
{
    public class ImdbContext : DbContext
    {
        private readonly string _connectionString;
        public ImdbContext(string connectionString)
        {
            _connectionString = connectionString;
        }


        public DbSet<NameSearch> NameSearch { get; set; }

        public DbSet<FindPopularActors> FindPopularActors { get; set; }

        public DbSet<NameRating> NameRating { get; set; }

        public DbSet<StringSearch> StringSearch { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NameSearch>().HasNoKey();
            modelBuilder.Entity<NameSearch>().Property(x => x.nconst).HasColumnName("nconst");
            modelBuilder.Entity<NameSearch>().Property(x => x.primaryname).HasColumnName("primaryname");


            modelBuilder.Entity<FindPopularActors>().HasNoKey();
            modelBuilder.Entity<FindPopularActors>().Property(x => x.primaryname).HasColumnName("primaryname");
            modelBuilder.Entity<FindPopularActors>().Property(x => x.rating).HasColumnName("rating");

            modelBuilder.Entity<NameRating>().HasNoKey();
            modelBuilder.Entity<NameRating>().Property(x => x.primaryname).HasColumnName("name_rating");

            modelBuilder.Entity<StringSearch>().HasNoKey();
            modelBuilder.Entity<StringSearch>().Property(x => x.tconst).HasColumnName("tconst");
            modelBuilder.Entity<StringSearch>().Property(x => x.primarytitle).HasColumnName("primarytitle");




        }
    }
}

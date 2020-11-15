using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataserviceLib
{
    class ImdbContext : DbContext
    {
        private readonly string _connectionString;

        public ImdbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Titlebasics> Titlebasicses { get; set; }

        public DbSet<Searchhistory> Searchhistories { get; set; }

        public DbSet<Ratinghistory> Ratinghistories { get; set; }

        public DbSet<Bookmark> Bookmarks { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasNoKey();
            modelBuilder.Entity<Person>().Property(x => x.Nconst).HasColumnName("nconst");
            modelBuilder.Entity<Person>().Property(x => x.Primaryname).HasColumnName("primaryname");
            modelBuilder.Entity<Person>().Property(x => x.Birthyear).HasColumnName("birthyear");
            modelBuilder.Entity<Person>().Property(x => x.Deathyear).HasColumnName("deathyear");

            modelBuilder.Entity<User>().HasNoKey();
            modelBuilder.Entity<User>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<User>().Property(x => x.Password).HasColumnName("password");

            modelBuilder.Entity<Titlebasics>().HasNoKey();
            modelBuilder.Entity<Titlebasics>().Property(x => x.Tconst).HasColumnName("tconst");
            modelBuilder.Entity<Titlebasics>().Property(x => x.Titletype).HasColumnName("titletype");
            modelBuilder.Entity<Titlebasics>().Property(x => x.Primarytitle).HasColumnName("primarytitle");
            modelBuilder.Entity<Titlebasics>().Property(x => x.Originaltitle).HasColumnName("origintitle");
            modelBuilder.Entity<Titlebasics>().Property(x => x.Isadult).HasColumnName("isadult");
            modelBuilder.Entity<Titlebasics>().Property(x => x.Startyear).HasColumnName("startyear");
            modelBuilder.Entity<Titlebasics>().Property(x => x.Endyear).HasColumnName("endyear");
            modelBuilder.Entity<Titlebasics>().Property(x => x.Runtimeminutes).HasColumnName("runtimeminutes");

            modelBuilder.Entity<Ratinghistory>().HasNoKey();
            modelBuilder.Entity<Ratinghistory>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<Ratinghistory>().Property(x => x.Rating).HasColumnName("rating");
            modelBuilder.Entity<Ratinghistory>().Property(x => x.Tconst).HasColumnName("tconst");

            modelBuilder.Entity<Searchhistory>().HasNoKey();
            modelBuilder.Entity<Searchhistory>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<Searchhistory>().Property(x => x.Mysearch).HasColumnName("mysearch");

            modelBuilder.Entity<Bookmark>().HasNoKey();
            modelBuilder.Entity<Bookmark>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<Bookmark>().Property(x => x.Tconst).HasColumnName("tconst");
            modelBuilder.Entity<Bookmark>().Property(x => x.Nconst).HasColumnName("nconst");

        }
    }
}

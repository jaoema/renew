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

        public DbSet<Titleaka> Titleakas { get; set; }
        public DbSet<Titleepisode> Titleepisodes { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Omdb> omdbs { get; set; }

        public DbSet<SimpleSearch> SimpleSearches { get; set; }

        

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasKey(x => x.Nconst);
            modelBuilder.Entity<Person>().ToTable("person");
            modelBuilder.Entity<Person>().Property(x => x.Nconst).HasColumnName("nconst");
            modelBuilder.Entity<Person>().Property(x => x.Primaryname).HasColumnName("primaryname");
            //modelBuilder.Entity<Person>().Property(x => x.Birthyear).HasColumnName("birthyear");
            //modelBuilder.Entity<Person>().Property(x => x.Deathyear).HasColumnName("deathyear");

            modelBuilder.Entity<User>().HasKey(x => x.Username);
            modelBuilder.Entity<User>().ToTable("username");
            modelBuilder.Entity<User>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<User>().Property(x => x.Password).HasColumnName("password");

            modelBuilder.Entity<Titlebasics>().HasKey(x => x.Tconst);
            modelBuilder.Entity<Titlebasics>().ToTable("title_basics");
            modelBuilder.Entity<Titlebasics>().Property(x => x.Tconst).HasColumnName("tconst");
            modelBuilder.Entity<Titlebasics>().Property(x => x.Titletype).HasColumnName("titletype");
            modelBuilder.Entity<Titlebasics>().Property(x => x.Primarytitle).HasColumnName("primarytitle");
            modelBuilder.Entity<Titlebasics>().Property(x => x.Originaltitle).HasColumnName("originaltitle");
            modelBuilder.Entity<Titlebasics>().Property(x => x.Isadult).HasColumnName("isadult");
            modelBuilder.Entity<Titlebasics>().Property(x => x.Startyear).HasColumnName("startyear");
            modelBuilder.Entity<Titlebasics>().Property(x => x.Endyear).HasColumnName("endyear");
            //modelBuilder.Entity<Titlebasics>().Property(x => x.Runtimeminutes).HasColumnName("runtimeminutes");

            modelBuilder.Entity<Ratinghistory>().HasNoKey();
            modelBuilder.Entity<Ratinghistory>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<Ratinghistory>().Property(x => x.Title).HasColumnName("title");
            modelBuilder.Entity<Ratinghistory>().Property(x => x.Rating).HasColumnName("rating");
            modelBuilder.Entity<Ratinghistory>().Property(x => x.Tconst).HasColumnName("tconst");

            modelBuilder.Entity<Searchhistory>().HasNoKey();
            modelBuilder.Entity<Searchhistory>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<Searchhistory>().Property(x => x.Mysearch).HasColumnName("mysearch");

           modelBuilder.Entity<Bookmark>().HasNoKey();

            modelBuilder.Entity<Bookmark>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<Bookmark>().Property(x => x.Tconst).HasColumnName("tconst");
            modelBuilder.Entity<Bookmark>().Property(x => x.Nconst).HasColumnName("nconst");

            modelBuilder.Entity<SimpleSearch>().HasNoKey();
            modelBuilder.Entity<SimpleSearch>().Property(x => x.Tconst).HasColumnName("tconst");
            modelBuilder.Entity<SimpleSearch>().Property(x => x.Title).HasColumnName("primarytitle");
            modelBuilder.Entity<SimpleSearch>().Property(x => x.Year).HasColumnName("startyear");


            

            modelBuilder.Entity<Titleaka>().HasKey(x => x.Tconst);

            modelBuilder.Entity<Genre>().HasKey("Tconst");
            modelBuilder.Entity<Genre>().ToTable("genre");
            modelBuilder.Entity<Genre>().Property(x => x.Tconst).HasColumnName("tconst");
            modelBuilder.Entity<Genre>().Property(x => x.Name).HasColumnName("name");

            modelBuilder.Entity<Omdb>().HasKey("Tconst");

            modelBuilder.Entity<Titleepisode>().HasKey("Tconst");

            modelBuilder.Entity<Titleprincipal>().HasKey("Tconst");

            modelBuilder.Entity<Titlerating>().HasKey("Tconst");

            modelBuilder.Entity<Types>().HasKey("Tconst");
           

            
        }
    }

    
}

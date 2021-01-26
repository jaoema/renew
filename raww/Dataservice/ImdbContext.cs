using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataserviceLib
{
    //Definere Database sets, hvilket kan bruges til at instanciere entities af vores classer ex person.
    class ImdbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Titlebasics> Titlebasicses { get; set; }

        public DbSet<Searchhistory> Searchhistories { get; set; }

        public DbSet<Ratinghistory> Ratinghistories { get; set; }

        public DbSet<Bookmark> Bookmarks { get; set; }

        public DbSet<Titleaka> Titleakas { get; set; }

        public DbSet<Titleprincipal> Titleprincipals { get; set; }

        public DbSet<Titleepisode> Titleepisodes { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Omdb> omdbs { get; set; }

        public DbSet<SimpleSearch> SimpleSearches { get; set; }

      

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("host=rawdata.ruc.dk;db=raw2;uid=raw2;pwd=OlUDAGe9");
            //optionsBuilder.UseNpgsql("host=localhost;db=imdb;uid=postgres;pwd =Franet0365");
           optionsBuilder.UseNpgsql("host=localhost;db=imdb;uid=postgres;pwd =Baad666");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   //Definere en pimary key, hvis den entity har en. 
            modelBuilder.Entity<Person>().HasKey(x => x.Nconst);
            //Mapper vores enity til et table.
            modelBuilder.Entity<Person>().ToTable("person");
            //definere hvilke attributes der skal sættes ind fra vores database. 
            modelBuilder.Entity<Person>().Property(x => x.Nconst).HasColumnName("nconst");
            modelBuilder.Entity<Person>().Property(x => x.Primaryname).HasColumnName("primaryname");
           
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
            //modelBuilder.Entity<Titlebasics>().Property(x => x.TitleprincipalTconst).HasColumnName("endyear");

            modelBuilder.Entity<Ratinghistory>().HasNoKey();
            modelBuilder.Entity<Ratinghistory>().ToTable("ratinghistory");
            modelBuilder.Entity<Ratinghistory>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<Ratinghistory>().Property(x => x.Title).HasColumnName("title");
            modelBuilder.Entity<Ratinghistory>().Property(x => x.Rating).HasColumnName("rating");
            modelBuilder.Entity<Ratinghistory>().Property(x => x.Tconst).HasColumnName("tconst");

            modelBuilder.Entity<Searchhistory>().HasNoKey();
            modelBuilder.Entity<Searchhistory>().ToTable("searchhistory");
            modelBuilder.Entity<Searchhistory>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<Searchhistory>().Property(x => x.Mysearch).HasColumnName("mysearch");

            modelBuilder.Entity<Bookmark>().HasNoKey();
            modelBuilder.Entity<Bookmark>().ToTable("bookmarked");
            modelBuilder.Entity<Bookmark>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<Bookmark>().Property(x => x.Tconst).HasColumnName("tconst");
            modelBuilder.Entity<Bookmark>().Property(x => x.Nconst).HasColumnName("nconst");
            modelBuilder.Entity<Bookmark>().Property(x => x.Primarytitle).HasColumnName("primarytitle");
            modelBuilder.Entity<Bookmark>().Property(x => x.Primaryname).HasColumnName("primaryname");
            modelBuilder.Entity<Bookmark>().Property(x => x.Startyear).HasColumnName("startyear");

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
          // modelBuilder.Entity<Titleprincipal>().HasKey("Nconst");
           //modelBuilder.Entity<Titleprincipal>().HasNoKey();
            modelBuilder.Entity<Titleprincipal>().ToTable("title_principals");
            modelBuilder.Entity<Titleprincipal>().Property(x => x.Tconst).HasColumnName("tconst");
            modelBuilder.Entity<Titleprincipal>().Property(x => x.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<Titleprincipal>().Property(x => x.Nconst).HasColumnName("nconst");
            modelBuilder.Entity<Titleprincipal>().Property(x => x.Category).HasColumnName("category");
            modelBuilder.Entity<Titleprincipal>().Property(x => x.Job).HasColumnName("job");
            modelBuilder.Entity<Titleprincipal>().Property(x => x.Characters).HasColumnName("characters");


        modelBuilder.Entity<Titlerating>().HasKey("Tconst");

            modelBuilder.Entity<Types>().HasKey("Tconst"); 
        }
    } 
}

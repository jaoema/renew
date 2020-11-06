using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Text;

namespace CallSqlFunctions
{
    public class ImdbContext : DbContext
    {
        private readonly string _connectionString;
        public ImdbContext(string connectionString)
        {
         _connectionString = connectionString;
        }


        

        public DbSet<Name_Search> Name_Search { get; set; }

        public DbSet<Rate> Rate{ get; set; }

        public DbSet<Find_Popular_Actors> Find_Popular_Actors { get; set; }

        public DbSet<Add_Rating_History> Add_Rating_Histories { get; set; }

        public DbSet<Ratinghistory> Ratinghistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Name_Search>().HasNoKey();
            modelBuilder.Entity<Name_Search>().Property(x => x.nconst).HasColumnName("nconst");
            modelBuilder.Entity<Name_Search>().Property(x => x.primaryname).HasColumnName("primaryname");


            modelBuilder.Entity<Rate>().HasNoKey();
            modelBuilder.Entity<Rate>().Property(x => x.username).HasColumnName("username");
            modelBuilder.Entity<Rate>().Property(x => x.rating).HasColumnName("rating");
            modelBuilder.Entity<Rate>().Property(x => x.tconst).HasColumnName("tconst");

            modelBuilder.Entity<Find_Popular_Actors>().HasNoKey();
            modelBuilder.Entity<Find_Popular_Actors>().Property(x => x.primaryname).HasColumnName("primaryname");
            modelBuilder.Entity<Find_Popular_Actors>().Property(x => x.rating).HasColumnName("rating");

            modelBuilder.Entity<Add_Rating_History>().HasNoKey();
            modelBuilder.Entity<Add_Rating_History>().Property(x => x.username).HasColumnName("username");
            modelBuilder.Entity<Add_Rating_History>().Property(x => x.movie).HasColumnName("movie");
            modelBuilder.Entity<Add_Rating_History>().Property(x => x.rating).HasColumnName("rating");

            modelBuilder.Entity<Ratinghistory>().HasNoKey();
            modelBuilder.Entity<Ratinghistory>().Property(x => x.username).HasColumnName("username");
            modelBuilder.Entity<Ratinghistory>().Property(x => x.rating).HasColumnName("rating");
            modelBuilder.Entity<Ratinghistory>().Property(x => x.tconst).HasColumnName("tconst");
        }
    }
}

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


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasNoKey();
            modelBuilder.Entity<Person>().Property(x => x.Nconst).HasColumnName("nconst");
            modelBuilder.Entity<Person>().Property(x => x.Primaryname).HasColumnName("primaryname");
            // modelBuilder.Entity<Person>().Property(x => x.Birthyear).HasColumnName("birthyear");
            //modelBuilder.Entity<Person>().Property(x => x.Deathyear).HasColumnName("deathyear");

            modelBuilder.Entity<User>().HasNoKey();
            modelBuilder.Entity<User>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<User>().Property(x => x.Password).HasColumnName("password");
        }
    }
}

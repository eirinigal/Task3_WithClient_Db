using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PB_Task3.Model
{
    public class PhoneBookContext: DbContext
    {

        //Specify the path to the database - this is a default location we havent specified a specific path
        private const string connectionString = "Server=(localdb)\\mssqllocaldb;DataBase=PhoneBookDb;Trusted_Connection=False;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer(connectionString);
        }

        //Making the Name field unique
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PhoneBookEntry>().HasIndex(pb=> pb.Number).IsUnique();
        }

        //We need the Dbset property to maniputlate the entries in the phone book table
        public DbSet<PhoneBookEntry> PhoneBook { get; set; }

        
    }
}

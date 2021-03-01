using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PB_Task3.Model
{
    public class PhoneBookContext: DbContext
    {
       
        //We need the Dbset property to maniputlate the entries in the phone book table
        public DbSet<PhoneBookEntry> PhoneBook { get; set; }

        //I tried to create connection string in the web.config but it was giving me 500 ERROR???
    }
}

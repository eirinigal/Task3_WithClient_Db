using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace PB_Task3.Model
{
    public class PhoneBookEntry
    {
        //Properties
        [Key]
        public int ID { get; set; }

        [Required]
        public int Number { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        //ToString
        public override string ToString()
        {
            return String.Format("ID: {0} - Number: {1} - Name: {2} - Address: {3}", ID, Number, Name, Address);
        }
    }
}

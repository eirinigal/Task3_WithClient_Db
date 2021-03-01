using Microsoft.AspNetCore.Mvc;
using PB_Task3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PB_Task3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneBookController : ControllerBase
    {
        //Since we have a database we need to talk to it via the dbset
        private PhoneBookContext ctx = new PhoneBookContext();
       

        // GET: api/<PhoneBookController>
        [HttpGet]
        public List<PhoneBookEntry> Get()
        {
            if (ctx.PhoneBook.Count() == 0) //database is empty
            {
                return null;
            }
            else
            {
                return ctx.PhoneBook.OrderBy(pb=> pb.Name).ToList();
            }

        }

        // GET api/<PhoneBookController>/0858382955
        //Return the name and address of a specified number
        [HttpGet("{number}")]
        public PhoneBookEntry GetNameAddress(int number)
        {
            PhoneBookEntry pb = ctx.PhoneBook.SingleOrDefault(pb => pb.Number == number);

            if (pb != null)
            {
                return pb;
            }
            else
            {
                return null; 
            }
           
        }

        // GET api/<PhoneBookController>/GetNumbers/Eirini
        //Return numbers and addresses for a specified name 
        [HttpGet("GetNumbers/{name}")]
        public List<PhoneBookEntry> GetNumbers(string name)
        {

            List<PhoneBookEntry> pb = ctx.PhoneBook.Where(pb => pb.Name.ToUpper() == name.ToUpper()).Select(pb => pb).ToList();

            if (pb.Count() != 0)
            {
                return pb;

            }
            else
            {
                return null;
            }
        }

        //Additional Operations
        //Add an entry aka POST
        // POST api/<PhoneBookController>
        [HttpPost]
        public string Post([FromBody] PhoneBookEntry newPB)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //checking for duplicate
                    var duplicateRecord = ctx.PhoneBook.SingleOrDefault(pb=> pb.Number == newPB.Number);
                    if(duplicateRecord == null) //basically we did not find an existing record
                    {
                        ctx.PhoneBook.Add(newPB);
                        ctx.SaveChanges();

                        return "A new phone book was added!"; // since we return a string 

                    }
                    else
                    {
                        return "This number " + newPB.Number +" already exists in the phone book database!";
                    }
                }
                else
                {
                    return "Phone book was not added";
                }

            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }

        }


        //Replace entry for a specified number with a new entry
        // PUT api/<PhoneBookController>/0830492724
        [HttpPut("{number}")]
        public string Put(int number, [FromBody] PhoneBookEntry updatePB)
        {
            try
            {
                if (ModelState.IsValid) // checking if the updatePB meets the data annotations from the PhoneBookEntry.cs
                {

                    //checking if the specified number is the same with the updatePB.number
                    if (number == updatePB.Number)
                    {
                        //find the number entry in the database
                        var findNumber = ctx.PhoneBook.SingleOrDefault(pb=> pb.Number == number);
                        if (findNumber != null)
                        {
                            //update Name and Address
                            findNumber.Name = updatePB.Name;
                            findNumber.Address = updatePB.Address;
                            ctx.SaveChanges();
                            return "Phone Book details were updated!";
                        }
                        else
                        {
                            return "Phone Book entry was not found";
                        }
                    }
                    else
                    {
                        return "Incorrect number: " + number ;
                    }

                }
                else
                {
                    return "Phone Book details were not updated :(";
                }

            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        // DELETE api/<PhoneBookController>/0830492724
        //Delete an entry(based on phone number)
        [HttpDelete("{number}")]
        public string Delete(int number)
        {
            try
            {
                //find phone book 
                var findPB = ctx.PhoneBook.SingleOrDefault(pb => pb.Number == number);
                if (findPB != null) //meaning we found the number we want to delete
                {
                    ctx.PhoneBook.Remove(findPB);
                    ctx.SaveChanges();
                    return "Phone book details were deleted!";
                }
                else
                {
                    return "Number does not exist!";
                }

            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }

        }
    }
}

   

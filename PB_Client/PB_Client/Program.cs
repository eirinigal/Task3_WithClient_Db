using PB_Task3.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PB_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            GetListOfNameAddress().Wait();
            Console.WriteLine();

            GetNameAddress().Wait();

            AddAsync().Wait();
            UpdateAsync().Wait();
            DeleteAsync().Wait();


        }


        //Async methods to communicate with the RestFul APIs
        //GET api/<PhoneBookController>/GetNumbers/Eirini
        private static async Task GetListOfNameAddress()
        {
            try
            {
                //1. Class HTTP Client to talk to restFul API
                HttpClient client = new HttpClient();

                //2.  Base URL for API controller eg. restFull service
                client.BaseAddress = new Uri("http://localhost:61290");

                //3. Adding a accept header eg. application/json
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //4. HTTP response from the GET API -- async call, await suspends until task finished
                HttpResponseMessage response = await client.GetAsync("api/PhoneBook/GetNumbers/Eirini");

                response.EnsureSuccessStatusCode();
                List<PhoneBookEntry> pList = await response.Content.ReadAsAsync<List<PhoneBookEntry>>();

                foreach (PhoneBookEntry pb in pList)
                {
                    Console.WriteLine("\n" + pb.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }

        }



        //GET api/<PhoneBookController>/0858382955
        private static async Task GetNameAddress()
        {
            try
            {
                //1. Class HTTP Client to talk to restFul API
                HttpClient client = new HttpClient();

                //2.  Base URL for API controller eg. restFull service
                client.BaseAddress = new Uri("http://localhost:61290");

                //3. Adding a accept header eg. application/json
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //4. HTTP response from the GET API -- async call, await suspends until task finished
                HttpResponseMessage response = await client.GetAsync("api/PhoneBook/0858382955");

                response.EnsureSuccessStatusCode();
                PhoneBookEntry pb = await response.Content.ReadAsAsync<PhoneBookEntry>();

                Console.WriteLine("\n" + pb.ToString());


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }

        }

        //Adding a phone book
        // add a stock listing
        static async Task AddAsync()
        {
            try
            {
                //1. Class HTTP Client to talk to restFul API
                HttpClient client = new HttpClient();

                //2.  Base URL for API controller eg. restFull service
                client.BaseAddress = new Uri("http://localhost:61290");                           // base URL for API Controller i.e. RESTFul service

                //3. Adding a accept header eg. application/json
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                
                // create a new phobe book 
                PhoneBookEntry newPB = new PhoneBookEntry() { Number = 834039271, Address="Cork", Name="Sean" };

                HttpResponseMessage response = await client.PostAsJsonAsync("api/PhoneBookController", newPB);
                response.EnsureSuccessStatusCode(); // throws an exception if it isnt
                Console.WriteLine("Phone Book was added!");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // update an existing phone book entry
        private static async Task UpdateAsync()
        {
            try
            {
                //1. Class HTTP Client to talk to restFul API
                HttpClient client = new HttpClient();

                //2. Base URL for API controller eg. restFull service
                client.BaseAddress = new Uri("http://localhost:61290");

                //3. Updating the name and address of a number 
                PhoneBookEntry newPB = new PhoneBookEntry() { Number = 834039271, Address = "Cork", Name = "Sean" };
                newPB.Name = "Kate";
                newPB.Address = "Donegal";


                //4. HTTP response from the GET API -- async call, await suspends until task finished
                HttpResponseMessage response = await client.PutAsJsonAsync("api/PhoneBook/834039271", newPB);

                response.EnsureSuccessStatusCode(); // throws an exception if it isnt
                Console.WriteLine("Phone Book was updated!");


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                //or Console.WriteLine(e.ToString());
            }
        }
        // delete a stock listing
        private static async Task DeleteAsync()
        {
            try
            {
                //1. Class HTTP Client to talk to restFul API
                HttpClient client = new HttpClient();

                //2. Base URL for API controller eg. restFull service
                client.BaseAddress = new Uri("http://localhost:61290");

                //3. HTTP response from the GET API -- async call, await suspends until task finished
                HttpResponseMessage response = await client.DeleteAsync("api/PhoneBook/834039271");

                response.EnsureSuccessStatusCode(); // throws an exception if it isnt
                Console.WriteLine("Number was deleted!");


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                //or Console.WriteLine(e.ToString());
            }
        }

    }
}

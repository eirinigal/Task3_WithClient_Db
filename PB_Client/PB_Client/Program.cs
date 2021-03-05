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
            GetAll().Wait();
            Console.WriteLine();

            GetNameAddress(83048523).Wait();
            Console.WriteLine();

            GetListOfNameAddress("Eirini").Wait();
            Console.WriteLine();

            //Create a new phobe book 
            PhoneBookEntry newPB = new PhoneBookEntry() { Number = 83403000, Address = "Kolindros", Name = "Sofia" }; //this already exists in my database, so we will get a message stating that!
            AddAsync(newPB).Wait();
            Console.WriteLine();

           
            //Updating the name and address of a number 
            PhoneBookEntry updateV = new PhoneBookEntry() { Number = 83403000, Address = "Kolindros-Pierias", Name = "Sofia Sarigiannidou" };
            UpdateAsync(83403000, updateV).Wait();


            DeleteAsync(83403000).Wait();
           
           

        }


        //Async methods to communicate with the RestFul APIs

        //GET api/<PhoneBookController>
        private static async Task GetAll()
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
                HttpResponseMessage response = await client.GetAsync("api/PhoneBook");

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

        //GET api/<PhoneBookController>/83048523
        private static async Task GetNameAddress(int number)
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
                HttpResponseMessage response = await client.GetAsync("api/PhoneBook/"+number);

                response.EnsureSuccessStatusCode();
                PhoneBookEntry pb = await response.Content.ReadAsAsync<PhoneBookEntry>();

                Console.WriteLine("\n" + pb.ToString());


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }

        }

        //GET api/<PhoneBookController>/GetNumbers/Eirini
        private static async Task GetListOfNameAddress(string name)
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
                HttpResponseMessage response = await client.GetAsync("api/PhoneBook/GetNumbers/"+name);

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



      

        //Adding a phone book
        static async Task AddAsync(PhoneBookEntry newPB)
        {
            try
            {
                //1. Class HTTP Client to talk to restFul API
                HttpClient client = new HttpClient();

                //2.  Base URL for API controller eg. restFull service
                client.BaseAddress = new Uri("http://localhost:61290");                           // base URL for API Controller i.e. RESTFul service

                //3. Adding a accept header eg. application/json
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                /*
                HttpResponseMessage response = await client.PostAsJsonAsync("api/PhoneBook", newPB);
                response.EnsureSuccessStatusCode(); // throws an exception if it isnt
                */

                HttpResponseMessage response = await client.PostAsJsonAsync("api/PhoneBook", newPB);
                if (response.IsSuccessStatusCode)
                {
                    string postStringReturn = await response.Content.ReadAsAsync<string>();
                    Console.WriteLine(postStringReturn);

                }
                else
                {
                    Console.WriteLine("Status Code: " + response.StatusCode + "\nReason Phrase: " + response.ReasonPhrase);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // update an existing phone book entry
        private static async Task UpdateAsync(int number, PhoneBookEntry updateValues)
        {
            try
            {
                //1. Class HTTP Client to talk to restFul API
                HttpClient client = new HttpClient();

                //2. Base URL for API controller eg. restFull service
                client.BaseAddress = new Uri("http://localhost:61290");

                //3. HTTP response from the GET API -- async call, await suspends until task finished
                HttpResponseMessage response = await client.PutAsJsonAsync("api/PhoneBook/"+number, updateValues);

                if (response.IsSuccessStatusCode)
                {
                    string updateStringReturn = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(updateStringReturn);

                }
                else
                {
                    Console.WriteLine("Status Code: " + response.StatusCode + "\nReason Phrase: " + response.ReasonPhrase);
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
               
            }
        }


        // delete a stock listing
        private static async Task DeleteAsync(int number)
        {
            try
            {
                //1. Class HTTP Client to talk to restFul API
                HttpClient client = new HttpClient();

                //2. Base URL for API controller eg. restFull service
                client.BaseAddress = new Uri("http://localhost:61290");

                //3. HTTP response from the GET API -- async call, await suspends until task finished
                HttpResponseMessage response = await client.DeleteAsync("api/PhoneBook/"+number);

                if (response.IsSuccessStatusCode)
                {
                    string deleteStringReturn = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(deleteStringReturn);

                }
                else
                {
                    Console.WriteLine("Status Code: " + response.StatusCode + "\nReason Phrase: " + response.ReasonPhrase);
                }


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                //or Console.WriteLine(e.ToString());
            }
        }

    }
}

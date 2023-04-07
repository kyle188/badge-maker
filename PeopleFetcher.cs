using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using CatWorx.BadgeMaker;

namespace Catworx.BadgeMaker
{
    class PeopleFetcher
    {
        public static Boolean UseApiQuestion()
        {
            Console.Write("To autogenerate badges type true \nFor customized badges type false \nAuto generate?: ");
            bool autoGen = Convert.ToBoolean(Console.ReadLine());
            return autoGen;
        }
        public static List<Employee> GetEmployees()
        {

            List<Employee> employees = new List<Employee>();
             while (true)
            {
                //Move the initial prompt inside the loop, so it repeats for each employee
                Console.WriteLine("Please enter first name: (leave empty to exit): ");

                string firstName = Console.ReadLine() ?? "";

                //break if the user hits enter withpout typing a name
                if (firstName == "")
                {
                    break;
                }

                Console.WriteLine("Please enter last name: ");
                string lastName = Console.ReadLine() ?? "";
                Console.WriteLine("Please enter ID: ");
                int id = Int32.Parse(Console.ReadLine() ?? "");
                Console.WriteLine("Please enter Photo URL: ");
                string photoUrl = Console.ReadLine() ?? "";
                Employee currentEmployee = new Employee(firstName, lastName, id, photoUrl);
                //Add currentEmployee, not a string
                employees.Add(currentEmployee);
                
                }
                return employees;
        }

        async public static Task<List<Employee>> GetFromApi()
        {
            List<Employee> employees = new List<Employee>();
            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync("https://randomuser.me/api/?results=10&nat=us&inc=name,id,picture");
                JObject json = JObject.Parse(response);
                
                foreach (JToken token in json.SelectToken("results")!) 
                {
                    //Parse JOSN data
                    Employee emp = new Employee
                    (
                        token.SelectToken("name.first")!.ToString(),
                        token.SelectToken("name.last")!.ToString(),
                        Int32.Parse(token.SelectToken("id.value")!.ToString().Replace("-", "")),
                        token.SelectToken("picture.large")!.ToString()
                    );
                    employees.Add(emp);
                }
            }
            return employees;
        }
    }
}
using System.Collections.Generic;

namespace CatWorx.BadgeMaker
{
    class Program
    {
        static List<Employee> GetEmployees()
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

        static void Main(string[] args) {
            List<Employee> employees = GetEmployees();
            Util.PrintEmployees(employees);
            Util.MakeCSV(employees);
        }
    }
}
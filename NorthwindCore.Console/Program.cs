using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NorthwindCore.Data;
using NorthwindCore.Data.Domain;

namespace NorthwindCore.Console
{
    // Excersize the Northwind EF connection
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine conflicts with local namespace so must prepend correct namespace.
            System.Console.WriteLine("Hello Northwind!");

            //Employees employee = InsertEmployee();
            //AssignTerritory(employee, "55439");

            //SimpleQueryEmployees();

            Employees gunder = GetEmployee("Dod");
            foreach (var terr in gunder.EmployeeTerritories)
            {
                System.Console.WriteLine("Territories found: " + terr.Territory.TerritoryDescription);
            }
            
            //gunder.HireDate = DateTime.Now;
            //SaveEmployee(gunder);
            //DeleteEmployee(gunder);

            System.Console.ReadKey();
        }

        private static void AssignTerritory(Employees employee, string territoryId)
        {
            using (var context = new NorthwindContext())
            {
                var empTerr = new EmployeeTerritories() { EmployeeId = employee.EmployeeId, TerritoryId = territoryId };
                context.EmployeeTerritories.Add(empTerr);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Removal requires the entity to be loaded with proper ID. If the entity is not
        /// available then Find() will have load the entity prior to call Remove().
        /// </summary>
        /// <param name="gunder"></param>
        private static void DeleteEmployee(Employees gunder)
        {
            using (var context = new NorthwindContext())
            {
                context.Employees.Remove(gunder);
                context.SaveChanges();
            }
        }

        private static void SaveEmployee(Employees gunder)
        {
            using (var context = new NorthwindContext())
            {
                context.Employees.Update(gunder);
                context.SaveChanges();
            }
        }

        private static Employees GetEmployee(string partialLastName)
        {
            string likeName = string.Format("%{0}%", partialLastName);
            using (var context = new NorthwindContext())
            {
                return context.Employees
                    .Include(et=>et.EmployeeTerritories)
                    .ThenInclude(t=>t.Territory)
                    .FirstOrDefault(n => EF.Functions.Like(n.LastName, likeName));
            }
        }

        private static void SimpleQueryEmployees()
        {
            using (var context = new NorthwindContext())
            {
                var employees = context.Employees.ToList();
                foreach(Employees emp in employees)
                {
                    System.Console.WriteLine("LastName: " + emp.LastName);
                }
            }
        }

        private static Employees InsertEmployee()
        {
            var employee = new Employees { FirstName = "Michael", LastName = "Gunderson2", TitleOfCourtesy = "Mr." };
            using (var context = new NorthwindContext())
            {
                context.Employees.Add(employee);
                context.SaveChanges();
            }
            return employee;
        }
    }
}

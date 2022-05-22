using ConsoleApp1.Helper;
using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
   
    class Program
    {
        static async Task Main(string[] args)
        {
            var filePath = (@"C:\Users\i.kalchenko\Desktop\companies_data.csv");

            try
            {
                var employee = await EmployeeParser.ReadFile(filePath);
                var rootElements = GrupByParent(employee);
                PrintRekursywly(rootElements,0,20);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine($"Koliczestwo elementow bolsze czem w masivie");
            }
             catch (ArgumentException argumentEx)
            {
                Console.WriteLine($"Invalid file pass: {argumentEx.Message}");
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong see details: {ex}");
            }

            

            

            Console.ReadLine();
        }

       
           private static void OrderByRowId(IEnumerable<Employee> employees)
            {
                var orderEmployees = employees.OrderBy(x => x.ID);
                               
                foreach (var item in orderEmployees)
                {
                    Console.WriteLine($@"Id :{item.ID} {item.Name} {item.Surname} {item.System} {item.Position}");
                    
                }


            }

        public static List<Employee> GrupByParent(List<Employee> employees)
        {
            var parentRelation = employees.ToLookup(x => x.ParentID);
            foreach (var item in employees)
            {
                item.Children = parentRelation[item.ID].OrderBy(x => x.ID).ToList();
            }
            return employees.Where(x => x.ParentID == 0).OrderBy(x => x.ID).ToList();
        }

        private static void PrintRekursywly(List<Employee> employees, int level, int maxIteration)
        {
            
            foreach (var item in employees)
            {
                Console.WriteLine($"Id :{item.ID} ParentId :{item.ParentID} {item.Name} {item.Surname} {item.System} {item.Position}");
                
                if (level > maxIteration)
                {
                    throw new InvalidOperationException();
                }

                PrintRekursywly(item.Children, level + 1, maxIteration);

            }
        }
  
    }
}

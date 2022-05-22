using CompanyTree.Helper;
using CompanyTree.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyTree
{
   
    class Program
    {
        static async Task Main(string[] args)
        {
            if( args.Length == 0)
            {
                Console.WriteLine("Please provide .csv file path, press any key to finish");
                Console.ReadKey();
                return;
            }
            var filePath = args[0];

            try
            {
                var employee = await EmployeeParser.ReadFileAsync(filePath);
                var rootElements = GrupByParent(employee);
                PrintRecursively(rootElements,0,19);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine($"Number of elements more than in the array");
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

        /*Groups rows by key(ParentID)*/

        private static List<Employee> GrupByParent(List<Employee> employees)
        {
            var parentRelation = employees.ToLookup(x => x.ParentID);
            foreach (var item in employees)
            {
                item.Children = parentRelation[item.ID].ToList();
            }
            return employees.Where(x => x.ParentID == 0).OrderBy(x => x.ID).ToList();
        }

        /*Prints grouped rows */

        private static void PrintRecursively(List<Employee> employees, int level, int maxIteration)
        {            
            foreach (var item in employees)
            {
                for (int i = 0; i < level; i++)
                {
                    Console.Write("     ");
                }

                Console.Write($"-> {item.Name} {item.Surname}, {item.System}, {item.Position} {Environment.NewLine}");
                
                if (level > maxIteration)
                {
                    throw new InvalidOperationException();
                }

                PrintRecursively(item.Children, level + 1, maxIteration);
            }
        }
  
    }
}

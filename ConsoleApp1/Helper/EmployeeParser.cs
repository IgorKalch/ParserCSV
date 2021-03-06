using CompanyTree.Exceptions;
using CompanyTree.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyTree.Helper
{
    public static class EmployeeParser
    {
        /// <summary>
        /// Splits lines into elements.The 6 desired elements are highlighted.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static async Task<List<Employee>> ReadFileAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("Provided file path is empty", nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new ArgumentException("File doesn't exist", nameof(filePath));
            }

            List<Employee> listEmployees = new List<Employee>();

            using (var textReader = new StreamReader(filePath))
            {
                string line;
                var row = 0;
               
                while ((line = await textReader.ReadLineAsync()) != null)
                {
                    row++;

                    var textData = line.Split(',');
                    Employee employee = new Employee();

                    if (textData.Length < 6)
                    {
                        throw new InvalidRowDataException("Amount properties lass then 6", row);
                    }

                    if (!int.TryParse(textData[0], out var id))
                    {
                        throw new InvalidRowDataException("Id is not a number", row);
                    }

                    employee.ID = id;

                    if (!int.TryParse(textData[1], out var parentid))
                    {
                        throw new InvalidRowDataException("ParentId is not a number", row);
                    }
                    employee.ParentID = parentid;

                    employee.Name = textData[2];
                    employee.Surname = textData[3];
                    employee.System = textData[4];
                    employee.Location = textData[5];
                    employee.Position = textData[6];

                    listEmployees.Add(employee);

                }

            }

            return listEmployees;

        }
    }
}

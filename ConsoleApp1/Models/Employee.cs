using System.Collections.Generic;

namespace ConsoleApp1.Models
{
    public class Employee
    {
        public int ID { get; set; }

        public int ParentID { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string System { get; set; }

        public string Location { get; set; }

        public string Position { get; set; }

        public int Nr1 { get; set; }

        public int Nr2 { get; set; }

        public int Nr3 { get; set; }

        public List<Employee> Children { get; set; }
    }
}

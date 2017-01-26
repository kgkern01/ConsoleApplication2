using System.Collections.Generic;

namespace ConsoleApplication2.Models
{
    public class Task
    {
        public string Name { get; set; }

        //public List<Tool> Tools { get; set; }

        public Tool Tool { get; set; }

        public List<string> Properties { get; set; }
    }
}

namespace ConsoleApplication2.Models
{
    public class LinkedProperty
    {
        public string Name { get; set; }
        public string ToolType { get; set; }

        public Program Program { get; set; }

        public bool IsExternal { get; set; }
    }
}

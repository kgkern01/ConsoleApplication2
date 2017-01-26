using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleApplication2.Models;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = @"C:\Projects\Test\Sample.txt";

            // Read a text file using StreamReader 
            using (var sr = new StreamReader(fileName))
            {
                string line;
                var toolList = new List<ToolPath>();
                var toolPath = new ToolPath();

                while ((line = sr.ReadLine()) != null)
                {
                    //Remove leading line
                    if (!line.StartsWith("Link List") && line.Trim() != "")
                    {
                        var pathName = line;
                        var isTool = line.StartsWith("[");

                        var type = line.Split('[', ']')[1];

                        var linkedProperty = "";
                        if (line.Contains(":"))
                            linkedProperty = line.Substring(line.IndexOf(":") + 1);

                        var decimalCount = line.Count(d => d == '.');

                        var programName = "";
                        var taskName = "";
                        var toolName = "";
                        var propertyName = "";

                        var parameterName = "";
                        var paramterType = "";
                        var tool = new string[] {};

                        if (line.Contains("--->"))
                        {
                            parameterName = line.Substring(0, line.IndexOf(" --->")).Trim();
                            paramterType = "Output";
                            tool = line.Substring(line.IndexOf(" --->")).Split('.');
                        }
                        else if (line.Contains("<---"))
                        {
                            parameterName = line.Substring(0, line.IndexOf(" <---")).Trim();
                            paramterType = "Input";
                            tool = line.Substring(line.IndexOf(" <---")).Split('.');
                        }
                        else
                        {
                            tool = line.Split('.');
                        }

                        programName = tool[0].Substring(tool[0].IndexOf("]") + 2).Trim();

                        

                        if (isTool)
                        {
                            toolPath = new ToolPath
                            {
                                ToolType = pathName,
                                ProgramName = programName,
                                ToolProperties = new List<Property>()
                            };
                            toolList.Add(toolPath);
                        }
                        else
                        {
                            switch (decimalCount)
                            {
                                case 1:
                                    taskName = tool[1];
                                    break;
                                case 2:
                                    taskName = tool[1];
                                    toolName = tool[2].Contains(":") ? tool[2].Substring(0, tool[2].IndexOf(":")) : tool[2];
                                    break;
                            }

                            toolPath.ToolProperties.Add(new Property
                            {
                                Direction = paramterType,
                                Name = parameterName,
                                LinkedProperty = new LinkedProperty
                                {
                                    ToolType = type,
                                    Program = new Models.Program
                                    {
                                        Name = tool[0].Substring(tool[0].IndexOf("]") +2 ).Trim(),
                                        Task = new Task { Name = taskName, Tool = new Tool { Name = toolName} }
                                    },
                                   IsExternal = toolPath.ProgramName == tool[0].Substring(tool[0].IndexOf("]") + 2).Trim(),
                                   Name = linkedProperty
                                }
                            });

                        }
                    }
                }

                var isValid = true;
                do
                {
                    Console.WriteLine("File has been ran.  Please select output option:");
                    Console.WriteLine(" 1 - To show results on screen");
                    Console.WriteLine(" 2 - Output results to file named Sample_Output.txt");
                    Console.WriteLine(" 3 - Exit");
                    var option = Console.ReadLine();

                    switch (option)
                    {
                        case "1":
                            OutputResults(toolList);
                            break;
                        case "2":
                            var fs = new FileStream(@"C:\Projects\Test\Sample_Output.txt", FileMode.Create);
                            // First, save the standard output.
                            var tmp = Console.Out;
                            var sw = new StreamWriter(fs);
                            Console.SetOut(sw);
                            OutputResults(toolList);
                            Console.SetOut(tmp);
                            sw.Close();
                            break;
                        case "3":
                            return;
                        default:
                            isValid = false;
                            Console.WriteLine("Invalid option, please enter either 1 - Screen or 2 - File");
                            break;
                    }
                } while (isValid);
            }
        }

        private static void OutputResults(List<ToolPath> toolList)
        {
            foreach (var item in toolList)
            {
                Console.WriteLine("Tool Name: " + item.ToolType);
                Console.WriteLine("");

                if (item.ToolProperties.Any(t => !t.LinkedProperty.IsExternal))
                {
                    Console.WriteLine("***Internal Program Dependencies ***");
                    foreach (var prop in item.ToolProperties.Where(p => !p.LinkedProperty.IsExternal))
                    {
                        Console.WriteLine("     Property Name: " + prop.Name);
                        Console.WriteLine("     Linked Property: Type: " + prop.LinkedProperty.ToolType);
                        if (prop.LinkedProperty.Program.Task.Name != "")
                            Console.WriteLine("                      Task: " + prop.LinkedProperty.Program.Task.Name);
                        if (prop.LinkedProperty.Program.Task.Tool.Name != "")
                            Console.WriteLine("                      Tool: " + prop.LinkedProperty.Program.Task.Tool.Name);
                        Console.WriteLine("                      Property Name: " + prop.LinkedProperty.Name);
                        Console.WriteLine("     Direction: " + prop.Direction);

                        Console.WriteLine("");
                    }
                }
                if (item.ToolProperties.Any(t => t.LinkedProperty.IsExternal))
                {
                    Console.WriteLine("***External Program Dependencies ***");
                    foreach (var prop in item.ToolProperties.Where(p => p.LinkedProperty.IsExternal))
                    {
                        Console.WriteLine("     Property Name: " + prop.Name);
                        Console.WriteLine("     Direction: " + prop.Direction);
                        if (prop.LinkedProperty.Program.Name != item.ProgramName)
                        {
                            Console.WriteLine("     ***External Program: " + prop.LinkedProperty.Program.Name);
                        }
                        Console.WriteLine("     Linked Property: Type: " + prop.LinkedProperty.ToolType);
                        if (prop.LinkedProperty.Program.Task.Name != "")
                            Console.WriteLine("                      Task: " + prop.LinkedProperty.Program.Task.Name);
                        if (prop.LinkedProperty.Program.Task.Tool.Name != "")
                            Console.WriteLine("                      Tool: " + prop.LinkedProperty.Program.Task.Tool.Name);
                        Console.WriteLine("                      Property Name: " + prop.LinkedProperty.Name);

                        Console.WriteLine("");
                    }
                }
                Console.WriteLine("************************************************");
                Console.WriteLine("");
            }
        }
    }
}

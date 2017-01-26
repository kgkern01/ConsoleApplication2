using System;
using System.Collections.Generic;
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
            using (var sr = new System.IO.StreamReader(fileName))
            {
                string line;
                var pathName = "";
                var newTool = true;
                var toolList = new List<ToolPath>();
                var toolPath = new ToolPath();

                while ((line = sr.ReadLine()) != null)
                {
                    //Remove leading line
                    if (!line.StartsWith("Link List") && line.Trim() != "")
                    {
                        pathName = line;
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
                                    }
                                }
                            });
                        }

                        //toolList.Add(toolPath);

                        //Console.WriteLine("Path: " + pathName);
                        //Console.WriteLine("Parameter Name: " + parameterName);
                        //Console.WriteLine("Parameter type: " + paramterType);
                        //Console.WriteLine("Program: " + programName);
                        //Console.WriteLine("Type:" + toolPath.ToolType);
                        //Console.WriteLine("Task: " + taskName);
                        //Console.WriteLine("Tool: " + toolName);
                        //Console.WriteLine("Linked Property: " + linkedProperty);
                        //Console.WriteLine("****************");
                    }

                }
                foreach (var item in toolList)
                {
                    Console.WriteLine("Tool Name: " + item.ToolType);
                    Console.WriteLine("Program:" + item.ProgramName);
                    
                    foreach (var prop in item.ToolProperties)
                    {
                        Console.WriteLine("Property Name: " + prop.Name);
                        Console.WriteLine("Direction: " + prop.Direction);
                        Console.WriteLine("Linked Property: Type: " + prop.LinkedProperty.ToolType + " Program: " + prop.LinkedProperty.Program.Name);
                        if (prop.LinkedProperty.Program.Name != item.ProgramName)
                        {
                            Console.WriteLine("External Program: " + prop.LinkedProperty.Program.Name);
                        }
                    }
                    Console.WriteLine("*************************");
                    Console.WriteLine("*************************");
                }
            }

            Console.WriteLine("*** Press any key to continue ***");
            Console.ReadLine();
        }
    }
}

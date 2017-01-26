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

                        var tool = line.Split('.');

                        switch (decimalCount)
                        {
                            case 0:
                                programName = tool[0].Substring(line.IndexOf("]") + 2);
                                break;
                            case 1:
                                programName = tool[0].Substring(line.IndexOf("]") + 2).Trim();
                                taskName = tool[1];
                                break;
                            case 2:
                                programName = tool[0].Substring(line.IndexOf("]") + 2).Trim();
                                taskName = tool[1];
                                toolName = tool[2].Contains(":") ? tool[2].Substring(0, tool[2].IndexOf(":")) : tool[2];
                                break;
                        }

                        var parameterName = "";
                        var paramterType = "";
                        if (line.Contains("--->"))
                        {
                            parameterName = line.Substring(0, line.IndexOf(" --->")).Trim();
                            paramterType = "Output";
                        }
                        else if (line.Contains("<---"))
                        {
                            parameterName = line.Substring(0, line.IndexOf(" <---")).Trim();
                            paramterType = "Input";
                        }

                        if (isTool)
                        {
                            toolPath = new ToolPath
                            {
                                ToolType = type
                            };
                        }
                        else
                        {
                            toolPath.ToolProperties.Add(new Property
                            {
                                Direction = paramterType,
                                Name = parameterName
                            });
                        }

                        Console.WriteLine("Path: " + pathName);
                        Console.WriteLine("Parameter Name: " + parameterName);
                        Console.WriteLine("Parameter type: " + paramterType);
                        Console.WriteLine("Program: " + programName);
                        Console.WriteLine("Type:" + type);
                        Console.WriteLine("Task: " + taskName);
                        Console.WriteLine("Tool: " + toolName);
                        Console.WriteLine("Linked Property: " + linkedProperty);
                        Console.WriteLine("****************");
                    }
                }
            }

            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = @"C:\Projects\Test\test.txt";

            // Read a text file using StreamReader 
            using (var sr = new System.IO.StreamReader(fileName))
            {
                string line;
                string pathName = "";
                var outputParameterList = new List<string>();
                var inputParameterList = new List<string>();
                var programList = new List<Program>();
                var newPath = true;

                while ((line = sr.ReadLine()) != null)
                {
                    //if (line.StartsWith("["))
                    //{
                        if (outputParameterList.Count > 0 && newPath == false)
                        {
                            Console.WriteLine("Output parameters:");
                            foreach (var output in outputParameterList)
                            {
                                Console.WriteLine(output);
                            }
                            outputParameterList.Clear();
                        }
                        if (inputParameterList.Count > 0 && newPath == false)
                        {
                            Console.WriteLine("Input parameters:");
                            foreach (var input in inputParameterList)
                            {
                                Console.WriteLine(input);
                            }
                            inputParameterList.Clear();
                        }
                        newPath = true;
                        pathName = line;
                        var type = line.Substring(line.IndexOf("[") + 1, line.IndexOf("]")-1);

                        var property = "";
                        if(line.Contains(":"))
                            property = line.Substring(line.IndexOf(":"));

                        var decimalCount = line.Count(d => d == '.');

                        var programName = "";
                        var taskName = "";
                        var toolName = "";
                        var propertyName = "";

                        var tool = line.Split('.');
                        //programName = tool[0];
                        //taskName = tool[1];
                        //toolName = tool[2];


                        switch (decimalCount)
                        {
                            case 0:
                                programName = tool[0].Substring(line.IndexOf("]") - 1);
                                break;
                            case 1:
                                programName = tool[0];
                                taskName = tool[1];
                                break;
                            case 2:
                                programName = tool[0];
                                taskName = tool[1];
                                toolName = tool[2];
                                break;
                        }   


                        //var program = new Models.Program
                        //{
                        //    Name = line.Substring()
                        //};

                        Console.WriteLine("Path: " + pathName);
                        Console.WriteLine("Program: " + programName);
                        Console.WriteLine("Type:" + type);
                        Console.WriteLine("Task: " + taskName);
                        Console.WriteLine("Tool: " + toolName);
                        Console.WriteLine("Property: " + property);
                        Console.WriteLine("****************");
                    //}
                    //else
                    //{
                    //    newPath = false;
                    //}

                    if (line.Contains("--->"))
                    {
                        outputParameterList.Add(line.Substring(0, line.IndexOf(" --->")));


                    }
                    else if (line.Contains("<---"))
                    {
                        inputParameterList.Add(line.Substring(0, line.IndexOf(" <---")));
                    }

                }

            }

            Console.ReadLine();
        }

        private static void ParseLine(string line, int decimalCount)
        {
            var cells = line.Split('.');

            var property = line.Substring(line.IndexOf(":") + 1, line.Length);
        }
    }
}

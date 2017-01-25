using System;
using System.Collections.Generic;

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
                string pathName = "";
                var outputParameterList = new List<string>();
                var inputParameterList = new List<string>();
                var newPath = true;

                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("["))
                    {
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
                        var type = line.Substring(line.IndexOf("[") + 1, line.IndexOf("]"));

                        var program = new Models.Program();
                        {

                        };
                        Console.WriteLine("Path: " + pathName);
                        Console.WriteLine("Type:" + type);
                    }
                    else
                    {
                        newPath = false;
                    }

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

        private static void ParseLine(string line)
        {
            
        }
    }
}

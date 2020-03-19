using System;
using System.IO;
using System.Linq;
using System.Configuration;
using TextProcessor;


namespace NumberParser
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Parsing the Number formats from the text file:\n");

                string inputfilePath = ConfigurationManager.AppSettings["filePath"];
                Console.WriteLine("Parsing text from The file :{0}", inputfilePath);

                var allLines = File.ReadLines(inputfilePath);
                var parsedStrs = TextProcessorBL.ParseText(allLines.ToList());

                Console.WriteLine("Extracted number strings:\n");
                foreach (var str in parsedStrs)
                    Console.WriteLine(str);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occured:\n {0}",ex.Message);
            }
            finally
            {
                Console.WriteLine(Console.Read());
            }
        }
    }
}

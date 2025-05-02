using System;
using System.IO;
using System.Collections.Generic;
using ParseOrderData.Models;
using ParseOrderData.Services;
using System.Xml;
using System.Xml.XPath;
namespace ParseOrderData
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = @"C:\Users\Kranthi\Documents\data.csv"; // Update with actual file path
            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("Input file not found.");
                return;
            }

            FileParser fileParser = new FileParser();
            (List<HeaderRecord> headers, List<DetailRecord> details) headers1 = fileParser.ParseFile(inputFilePath);

            XmlGenerator xmlGenerator = new XmlGenerator();
            string xmlOutput = xmlGenerator.GenerateXml(headers1.headers, headers1.details);

            string outputFilePath = "C:\\Users\\Kranthi\\Documents\\outputfile.xml"; // Update with desired output file path
            File.WriteAllText(outputFilePath, xmlOutput);

            Console.WriteLine("XML file generated successfully.");
        }
    }
}
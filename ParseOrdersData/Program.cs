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
            try
            {

            
            string inputFilePath = @"C:\Users\Kranthi\Documents\data.csv"; 
            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("Input file not found.");
                return;
            }

            FileParserService fileParser = new FileParserService();
            (List<HeaderRecord> headers, List<DetailRecord> details) headers1 = fileParser.ParseCsvFile(inputFilePath);

            XmlService xmlGenerator = new XmlService();
            string xmlOutput = xmlGenerator.GenerateXmlFromRawData(headers1.headers, headers1.details);

            string outputFilePath = "C:\\Users\\Kranthi\\Documents\\outputfile.xml"; // Update with desired output file path
            File.WriteAllText(outputFilePath, xmlOutput);

            Console.WriteLine("XML file generated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong while parsing the data",ex.Message);

                
            }
        }
    }
}
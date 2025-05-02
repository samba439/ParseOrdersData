using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ParseOrderData.Models;
using ParseOrderData.Utilities;

namespace ParseOrderData.Services
{
    public class FileParser
    {
        public (List<HeaderRecord> headers, List<DetailRecord> details) ParseFile(string filePath)
        {
            var headers = new List<HeaderRecord>();
            var details = new List<DetailRecord>();

            var lines = File.ReadAllLines(filePath);
            if(lines.Length == 0)
            {
                throw new InvalidDataException("File is empty");
            }
            HeaderRecord currentHeader = null;
            HashSet<int> existingLineNumbers= new HashSet<int>();
            foreach (var line in lines)
            {
                

                var columns = line.Split(',');
               
                if (columns[0] == "H")
                {
                    existingLineNumbers.Clear();
                    if (ValidationHelper.ValidateHeaderRecord(columns) == false)
                    {
                        Console.WriteLine($"Invalid header record: {line}");
                        continue;
                    }
                    currentHeader = new HeaderRecord
                    {
                        PurchaseOrderNumber = columns[1],
                        Supplier = columns[2],
                        Origin = columns[3],
                        Destination =new DestinationResolver().ResolveDestination(columns[2], columns[4]),
                        CargoReadyDate = DateTime.Parse(columns[5])
                    };
                    headers.Add(currentHeader);
                }
                else if (columns[0] == "D" && currentHeader != null)
                {
                    

                    if (ValidationHelper.ValidateDetailRecord(columns, existingLineNumbers) == false)
                    {
                        Console.WriteLine($"Invalid detail record: {line}");
                        //continue;
                        throw new InvalidDataException();
                    }
                    var detail = new DetailRecord
                    {
                        PurchaseOrderNumber = columns[1],
                        LineNumber = int.Parse(columns[2]),
                        ItemDescription = columns[3],
                        OrderQty = int.Parse(columns[4])
                    };
                    details.Add(detail);
                }
            }

            return (headers, details);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ParseOrderData.Models;

namespace ParseOrderData.Services
{
    public class FileParser
    {
        public (List<HeaderRecord> headers, List<DetailRecord> details) ParseFile(string filePath)
        {
            var headers = new List<HeaderRecord>();
            var details = new List<DetailRecord>();

            var lines = File.ReadAllLines(filePath);
            HeaderRecord currentHeader = null;

            foreach (var line in lines)
            {
                var columns = line.Split(',');

                if (columns[0] == "H")
                {
                    currentHeader = new HeaderRecord
                    {
                        PurchaseOrderNumber = columns[1],
                        Supplier = columns[2],
                        Origin = columns[3],
                        Destination = columns[4],
                        CargoReadyDate = DateTime.Parse(columns[5])
                    };
                    headers.Add(currentHeader);
                }
                else if (columns[0] == "D" && currentHeader != null)
                {
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
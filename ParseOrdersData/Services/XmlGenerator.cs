using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ParseOrderData.Models;

namespace ParseOrderData.Services
{
    public class XmlGenerator
    {
        public string GenerateXml(List<HeaderRecord> headers, List<DetailRecord> details)
        {
            var xmlStringBuilder = new StringBuilder();
            using (var xmlWriter = XmlWriter.Create(xmlStringBuilder, new XmlWriterSettings { Indent = true }))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Orders");

                foreach (var header in headers)
                {
                    xmlWriter.WriteStartElement("Order");
                    xmlWriter.WriteElementString("PurchaseOrderNumber", header.PurchaseOrderNumber);
                    xmlWriter.WriteElementString("Supplier", header.Supplier);
                    xmlWriter.WriteElementString("Origin", header.Origin);
                    xmlWriter.WriteElementString("Destination", header.Destination);
                    xmlWriter.WriteElementString("CargoReadyDate", header.CargoReadyDate.ToString("o"));

                    foreach (var detail in details)
                    {
                        if (detail.PurchaseOrderNumber == header.PurchaseOrderNumber)
                        {
                            xmlWriter.WriteStartElement("Detail");
                            xmlWriter.WriteElementString("LineNumber", detail.LineNumber.ToString());
                            xmlWriter.WriteElementString("ItemDescription", detail.ItemDescription);
                            xmlWriter.WriteElementString("OrderQty", detail.OrderQty.ToString());
                            xmlWriter.WriteEndElement(); // Detail
                        }
                    }

                    xmlWriter.WriteEndElement(); // Order
                }

                xmlWriter.WriteEndElement(); // Orders
                xmlWriter.WriteEndDocument();
            }

            return xmlStringBuilder.ToString();
        }
    }
}
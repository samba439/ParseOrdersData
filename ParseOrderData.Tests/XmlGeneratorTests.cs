using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using ParseOrderData.Models;
using ParseOrderData.Services;

namespace ParseOrderData.Tests
{
    public class XmlGeneratorTests
    {
        [Fact]
        public void GenerateXml_ValidHeaderAndDetails_ReturnsCorrectXml()
        {
            // Arrange
            var headers = new List<HeaderRecord>() {
        new HeaderRecord
        {
            PurchaseOrderNumber = "PO123",
            Supplier = "SHANGHAI FURNITURE COMPANY",
            Origin = "Shanghai",
            Destination = "Melbourne AUMEL",
            CargoReadyDate = DateTime.Parse("2023-10-01")
        }
    };

            var details = new List<DetailRecord>
    {
        new DetailRecord
        {
            PurchaseOrderNumber = "PO123",
            LineNumber = 1,
            ItemDescription = "Chair",
            OrderQty = 10
        },
        new DetailRecord
        {
            PurchaseOrderNumber = "PO123",
            LineNumber = 2,
            ItemDescription = "Table",
            OrderQty = 5
        }
    };

            var xmlGenerator = new XmlService();

            // Act
            var result = xmlGenerator.GenerateXmlFromRawData(headers, details);
            var resultXml = XElement.Parse(result);
            // Assert
            var expectedXml = XElement.Parse(@"
<Orders>
  <Order>
    <PurchaseOrderNumber>PO123</PurchaseOrderNumber>
    <Supplier>SHANGHAI FURNITURE COMPANY</Supplier>
    <Origin>Shanghai</Origin>
    <Destination>Melbourne AUMEL</Destination>
    <CargoReadyDate>2023-10-01T00:00:00.0000000</CargoReadyDate>
    <Detail>
      <LineNumber>1</LineNumber>
      <ItemDescription>Chair</ItemDescription>
      <OrderQty>10</OrderQty>
    </Detail>
    <Detail>
      <LineNumber>2</LineNumber>
      <ItemDescription>Table</ItemDescription>
      <OrderQty>5</OrderQty>
    </Detail>
  </Order>
</Orders>");

            Assert.Equal(expectedXml.FirstNode.ToString(), resultXml.FirstNode.ToString());
        }

        [Fact]
        public void GenerateXml_HeaderWithoutDestination_UsesDefaultDestination()
        {
            // Arrange
            var headers = new List<HeaderRecord>() {
             new HeaderRecord
            {
                PurchaseOrderNumber = "PO124",
                Supplier = "YANTIAN INDUSTRIAL PRODUCTS",
                Origin = "Yantian",
                CargoReadyDate = DateTime.Parse("2023-10-02")
            }
            };

            var details = new List<DetailRecord>
                {
                    new DetailRecord
                    {
                        PurchaseOrderNumber = "PO124",
                        LineNumber = 1,
                        ItemDescription = "Sofa",
                        OrderQty = 2
                    }
                };

            var xmlGenerator = new XmlService();

            // Act
            var result = xmlGenerator.GenerateXmlFromRawData(headers, details);
            var resultXml = XElement.Parse(result);
            // Assert
            var expectedXml = XElement.Parse(@"<Orders>
  <Order>
    <PurchaseOrderNumber>PO124</PurchaseOrderNumber>
    <Supplier>YANTIAN INDUSTRIAL PRODUCTS</Supplier>
    <Origin>Yantian</Origin>
    <Destination />
    <CargoReadyDate>2023-10-02T00:00:00.0000000</CargoReadyDate>
    <Detail>
      <LineNumber>1</LineNumber>
      <ItemDescription>Sofa</ItemDescription>
      <OrderQty>2</OrderQty>
    </Detail>
  </Order>
</Orders>");

            Assert.Equal(expectedXml.FirstNode.ToString(), resultXml.FirstNode.ToString());
        }
    }
}

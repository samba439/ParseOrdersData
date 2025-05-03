using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParseOrderData.Services;

namespace ParseOrderData.Tests
{
    public class FileParserTests
    {
        private readonly FileParserService _fileParser;

        public FileParserTests()
        {
            _fileParser = new FileParserService();
        }

        [Fact]
        public void Parse_ValidHeaderAndDetail_ReturnsCorrectRecords()
        {

            var result = _fileParser.ParseCsvFile("data.csv");

            Assert.Equal(3,result.headers.Count);
            Assert.Equal("PO12345", result.headers[0].PurchaseOrderNumber);
            Assert.Equal("SupplierA", result.headers[0].Supplier);
            Assert.Equal("Los Angeles", result.headers[0].Destination);
            Assert.Equal(8, result.details.Count);
        }

        [Fact]
        public void Parse_HeaderWithoutDestination_UsesDefaultDestination()
        {


            var result = _fileParser.ParseCsvFile("data.csv");

            Assert.Equal("Melbourne AUMEL", result.headers[1].Destination);
        }

        [Fact]
        public void Parse_InvalidLineNumber_ThrowsException()
        {
            var input = "H,PO125,SFC01,,Melbourne,2023-10-01\n" +
                        "D,PO125,1,Desk,3\n" +
                        "D,PO125,1,Chair,4"; // Duplicate line number

           // var result = _fileParser.ParseFile("data.csv");
            Assert.Throws<InvalidDataException>(() => _fileParser.ParseCsvFile("data-duplicatelinenumber.csv"));
        }

        [Fact]
        public void Parse_EmptyInput_ThrowsException()
        {
            var input = "";

            Assert.Throws<InvalidDataException>(() => _fileParser.ParseCsvFile("data-empty.csv"));
        }
    }
}

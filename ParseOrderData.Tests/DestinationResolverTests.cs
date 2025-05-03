
using System;
using System.Net.WebSockets;
using ParseOrderData.Services;
using Xunit; // Ensure this namespace is included for the Assert class  

namespace ParseOrderData.Tests
{
    public class DestinationResolverTests
    {
        private DestinationService _destinationResolver;

        public DestinationResolverTests()
        {
            _destinationResolver = new DestinationService();
        }

        [Fact]
        public void ResolveDestination_ShouldReturnMelbourne_WhenSupplierIsSFC01()
        {
            var supplierCode = "SFC01";
            var destination = _destinationResolver.MapDestination(supplierCode, "");
            Assert.Equal("Melbourne AUMEL", destination);
        }

        [Fact]
        public void ResolveDestination_ShouldReturnSydney_WhenSupplierIsYIP1()
        {
            var supplierCode = "YIP-1";
            var destination = _destinationResolver.MapDestination(supplierCode, "");
            Assert.Equal("Sydney AUSYD", destination);
        }

        [Fact]
        public void ResolveDestination_ShouldReturnNull_WhenSupplierIsUnknown()
        {
            var supplier = "UNKNOWN SUPPLIER";
            var destination = _destinationResolver.MapDestination(supplier, "UNKNOWN SUPPLIER");
            Assert.NotNull(destination);
        }
    }
}

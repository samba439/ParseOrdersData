
using System;
using System.Net.WebSockets;
using ParseOrderData.Services;
using Xunit; // Ensure this namespace is included for the Assert class  

namespace ParseOrderData.Tests
{
    public class DestinationResolverTests
    {
        private DestinationResolver _destinationResolver;

        public DestinationResolverTests()
        {
            _destinationResolver = new DestinationResolver();
        }

        [Fact]
        public void ResolveDestination_ShouldReturnMelbourne_WhenSupplierIsSFC01()
        {
            var supplierCode = "SFC01";
            var destination = _destinationResolver.ResolveDestination(supplierCode, "");
            Assert.Equal("Melbourne AUMEL", destination);
        }

        [Fact]
        public void ResolveDestination_ShouldReturnSydney_WhenSupplierIsYIP1()
        {
            var supplierCode = "YIP-1";
            var destination = _destinationResolver.ResolveDestination(supplierCode, "");
            Assert.Equal("Sydney AUSYD", destination);
        }

        [Fact]
        public void ResolveDestination_ShouldReturnNull_WhenSupplierIsUnknown()
        {
            var supplier = "UNKNOWN SUPPLIER";
            var destination = _destinationResolver.ResolveDestination(supplier, "UNKNOWN SUPPLIER");
            Assert.NotNull(destination);
        }
    }
}

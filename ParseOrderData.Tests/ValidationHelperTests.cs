using System.Collections.Generic;
using Xunit;
using ParseOrderData.Utilities;

namespace ParseOrdersData.Tests.Utilities
{
    public class ValidationHelperTests
    {
        [Fact]
        public void ValidateDetailRecord_ValidRecord_ReturnsTrue()
        {
            // Arrange  
            var detailFields = new[] { "Item1", "Description", "1", "10.00", "2" };
            var existingLineNumbers = new HashSet<int>();

            // Act  
            var result = ValidationHelper.ValidateDetailRecord(detailFields, existingLineNumbers);

            // Assert  
            Assert.True(result);
            Assert.Contains(1, existingLineNumbers);
        }

        [Fact]
        public void ValidateDetailRecord_InvalidLength_ReturnsFalse()
        {
            // Arrange  
            var detailFields = new[] { "Item1", "Description", "1", "10.00" };
            var existingLineNumbers = new HashSet<int>();

            // Act  
            var result = ValidationHelper.ValidateDetailRecord(detailFields, existingLineNumbers);

            // Assert  
            Assert.False(result);
        }

        [Fact]
        public void ValidateDetailRecord_DuplicateLineNumber_ReturnsFalse()
        {
            // Arrange  
            var detailFields = new[] { "Item1", "Description", "1", "10.00", "2" };
            var existingLineNumbers = new HashSet<int> { 1 };

            // Act  
            var result = ValidationHelper.ValidateDetailRecord(detailFields, existingLineNumbers);

            // Assert  
            Assert.False(result);
        }

        [Fact]
        public void ValidateDetailRecord_InvalidLineNumber_ReturnsFalse()
        {
            // Arrange  
            var detailFields = new[] { "Item1", "Description", "InvalidNumber", "10.00", "2" };
            var existingLineNumbers = new HashSet<int>();

            // Act  
            var result = ValidationHelper.ValidateDetailRecord(detailFields, existingLineNumbers);

            // Assert  
            Assert.False(result);
        }
    }
}

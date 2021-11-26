using System;
using System.Collections.Generic;
using Xunit;

namespace ApplicationEDUUnitTest
{
    public class SampleUnitTest
    {
        [Fact]
        public void TestSample_IsCollectionEmpty()
        {
            // Arrange
            var collection = new List<int>() { 1, 2, 3 };

            // Act
            collection.Clear();

            // Assert
            Assert.Empty(collection );
        }
    }
}

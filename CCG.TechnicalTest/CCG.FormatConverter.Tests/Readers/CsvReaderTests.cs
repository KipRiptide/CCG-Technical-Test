using CCG.FormatConverter.Builders;
using CCG.FormatConverter.Readers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CCG.FormatConverter.Tests
{
    [TestClass]
    public class CsvReaderTests
    {
        [TestMethod]
        public void Read_WithNullSourcePath_ThrowArgumentNullException()
        {
            var dynamicObjectBuilderMock = new Mock<IDynamicObjectBuilder>();
            var reader = new CsvReader(dynamicObjectBuilderMock.Object);
            Assert.ThrowsException<ArgumentNullException>(() => reader.Read(null));
        }

        [TestMethod]
        public void Read_WithValidSourcePath_ButEmptyFile_ReturnsEmptyCollection()
        {
            var dynamicObjectBuilderMock = new Mock<IDynamicObjectBuilder>();
            var reader = new CsvReader(dynamicObjectBuilderMock.Object);
            var result = reader.Read(@".\Samples\Empty.csv");
            Assert.IsInstanceOfType(result, typeof(IEnumerable<dynamic>));
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void Read_WithValidSourcePath_AndFileHasContent_ReturnsCollection()
        {
            var dynamicObjectBuilderMock = new Mock<IDynamicObjectBuilder>();
            var reader = new CsvReader(dynamicObjectBuilderMock.Object);
            var result = reader.Read(@".\Samples\Contacts.csv");
            Assert.IsInstanceOfType(result, typeof(IEnumerable<dynamic>));
            Assert.AreEqual(3, result.Count());
        }
    }
}

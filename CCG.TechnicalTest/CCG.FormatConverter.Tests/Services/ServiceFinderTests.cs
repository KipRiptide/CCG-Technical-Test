using CCG.FormatConverter.Readers;
using CCG.FormatConverter.Services;
using CCG.FormatConverter.Writers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCG.FormatConverter.Tests.Services
{
    [TestClass]
    public class ServiceFinderTests
    {
        [TestMethod]
        public void Find_WithTypeNameThatIsNotInServices_ReturnsNull()
        {
            var writers = new List<IWriter>() { new JsonWriter() };
            var serviceFinder = new ServiceFinder<IWriter>(writers);
            var service = serviceFinder.Find("TestWriter");
            Assert.IsNull(service);
        }

        [TestMethod]
        public void Find_WithTypeNameThatIsInServices_ReturnsService()
        {
            var writers = new List<IWriter>() { new JsonWriter() };
            var serviceFinder = new ServiceFinder<IWriter>(writers);
            var service = serviceFinder.Find("jsonWriter");
            Assert.IsInstanceOfType(service, typeof(JsonWriter));
        }
    }
}

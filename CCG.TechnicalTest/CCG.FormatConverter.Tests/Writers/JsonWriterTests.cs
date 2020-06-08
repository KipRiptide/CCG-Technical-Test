using CCG.FormatConverter.Writers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CCG.FormatConverter.Tests.Writers
{
    [TestClass]
    public class JsonWriterTests
    {
        [TestMethod]
        public void Read_WithNullDestinationPath_ThrowArgumentNullException()
        {
            var writer = new JsonWriter();
            Assert.ThrowsException<ArgumentNullException>(() => writer.Write(null, new List<dynamic>()));
        }

        [TestMethod]
        public void Read_WithNullItems_ThrowArgumentNullException()
        {
            var writer = new JsonWriter();
            Assert.ThrowsException<ArgumentNullException>(() => writer.Write("path", null));
        }

        [TestMethod]
        public void Read_WithPathAndItems_WritesFile()
        {
            var writer = new XmlWriter();
            writer.Write(@".\Results.json", new List<dynamic>());
            Assert.IsTrue(File.Exists(@".\Results.json"));
            File.Delete(@".\Results.json");
        }
    }
}

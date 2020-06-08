using CCG.FormatConverter.Builders;
using CCG.FormatConverter.Readers;
using CCG.FormatConverter.Services;
using CCG.FormatConverter.Writers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCG.FormatConverter.Tests
{
    [TestClass]
    public class ConverterTests
    {
        [TestMethod]
        public void Convert_SourceFormatIsNull_DoesNotCallReadOrWrite()
        {
            var loggerMock = new Mock<ILogger<Converter>>();
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.SetupGet(configuration => configuration["sourceFormat"]).Returns((string)null);
            var readerMock = new Mock<IReader>();
            var readerFinderMock = new Mock<IServiceFinder<IReader>>();
            var writerMock = new Mock<IWriter>();
            var writerFinderMock = new Mock<IServiceFinder<IWriter>>();
            var converter = new Converter(loggerMock.Object, configurationMock.Object, readerFinderMock.Object, writerFinderMock.Object);

            converter.Convert();

            readerMock.Verify(reader => reader.Read(It.IsAny<string>()), Times.Never);
            writerMock.Verify(writer => writer.Write(It.IsAny<string>(), It.IsAny<IEnumerable<dynamic>>()), Times.Never);
        }

        [TestMethod]
        public void Convert_SourcePathIsNull_DoesNotCallReadOrWrite()
        {
            var loggerMock = new Mock<ILogger<Converter>>();
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.SetupGet(configuration => configuration["sourceFormat"]).Returns("csv");
            configurationMock.SetupGet(configuration => configuration["sourcePath"]).Returns((string)null);
            var readerMock = new Mock<IReader>();
            var readerFinderMock = new Mock<IServiceFinder<IReader>>();
            var writerMock = new Mock<IWriter>();
            var writerFinderMock = new Mock<IServiceFinder<IWriter>>();
            var converter = new Converter(loggerMock.Object, configurationMock.Object, readerFinderMock.Object, writerFinderMock.Object);

            converter.Convert();

            readerMock.Verify(reader => reader.Read(It.IsAny<string>()), Times.Never);
            writerMock.Verify(writer => writer.Write(It.IsAny<string>(), It.IsAny<IEnumerable<dynamic>>()), Times.Never);
        }

        [TestMethod]
        public void Convert_DestinationFormatIsNull_DoesNotCallReadOrWrite()
        {
            var loggerMock = new Mock<ILogger<Converter>>();
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.SetupGet(configuration => configuration["sourceFormat"]).Returns("csv");
            configurationMock.SetupGet(configuration => configuration["sourcePath"]).Returns(@"C:\temp\test.csv");
            configurationMock.SetupGet(configuration => configuration["destinationFormat"]).Returns((string)null);
            var readerMock = new Mock<IReader>();
            var readerFinderMock = new Mock<IServiceFinder<IReader>>();
            var writerMock = new Mock<IWriter>();
            var writerFinderMock = new Mock<IServiceFinder<IWriter>>();
            var converter = new Converter(loggerMock.Object, configurationMock.Object, readerFinderMock.Object, writerFinderMock.Object);

            converter.Convert();

            readerMock.Verify(reader => reader.Read(It.IsAny<string>()), Times.Never);
            writerMock.Verify(writer => writer.Write(It.IsAny<string>(), It.IsAny<IEnumerable<dynamic>>()), Times.Never);
        }

        [TestMethod]
        public void Convert_DestinationPathIsNull_DoesNotCallReadOrWrite()
        {
            var loggerMock = new Mock<ILogger<Converter>>();
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.SetupGet(configuration => configuration["sourceFormat"]).Returns("csv");
            configurationMock.SetupGet(configuration => configuration["sourcePath"]).Returns(@"C:\temp\test.csv");
            configurationMock.SetupGet(configuration => configuration["destinationFormat"]).Returns("json");
            configurationMock.SetupGet(configuration => configuration["destinationPath"]).Returns((string)null);
            var readerMock = new Mock<IReader>();
            var readerFinderMock = new Mock<IServiceFinder<IReader>>();
            var writerMock = new Mock<IWriter>();
            var writerFinderMock = new Mock<IServiceFinder<IWriter>>();
            var converter = new Converter(loggerMock.Object, configurationMock.Object, readerFinderMock.Object, writerFinderMock.Object);

            converter.Convert();

            readerMock.Verify(reader => reader.Read(It.IsAny<string>()), Times.Never);
            writerMock.Verify(writer => writer.Write(It.IsAny<string>(), It.IsAny<IEnumerable<dynamic>>()), Times.Never);
        }

        [TestMethod]
        public void Convert_CannotFindReader_DoesNotCallReadOrWrite()
        {
            var loggerMock = new Mock<ILogger<Converter>>();
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.SetupGet(configuration => configuration["sourceFormat"]).Returns("test");
            var readerMock = new Mock<IReader>();
            var readerFinderMock = new Mock<IServiceFinder<IReader>>();
            var writerMock = new Mock<IWriter>();
            var writerFinderMock = new Mock<IServiceFinder<IWriter>>();
            var converter = new Converter(loggerMock.Object, configurationMock.Object, readerFinderMock.Object, writerFinderMock.Object);

            converter.Convert();

            readerMock.Verify(reader => reader.Read(It.IsAny<string>()), Times.Never);
            writerMock.Verify(writer => writer.Write(It.IsAny<string>(), It.IsAny<IEnumerable<dynamic>>()), Times.Never);
        }

        [TestMethod]
        public void Convert_CannotFindWriter_DoesNotCallReadOrWrite()
        {
            var loggerMock = new Mock<ILogger<Converter>>();
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.SetupGet(configuration => configuration["sourceFormat"]).Returns("csv");
            configurationMock.SetupGet(configuration => configuration["sourcePath"]).Returns(@"C:\temp\test.csv");
            configurationMock.SetupGet(configuration => configuration["destinationFormat"]).Returns("json");
            configurationMock.SetupGet(configuration => configuration["destinationPath"]).Returns(@"C:\temp\test.json");
            var readerMock = new Mock<IReader>();
            var readerFinderMock = new Mock<IServiceFinder<IReader>>();
            var writerMock = new Mock<IWriter>();
            var writerFinderMock = new Mock<IServiceFinder<IWriter>>();
            var converter = new Converter(loggerMock.Object, configurationMock.Object, readerFinderMock.Object, writerFinderMock.Object);

            converter.Convert();

            readerMock.Verify(reader => reader.Read(It.IsAny<string>()), Times.Never);
            writerMock.Verify(writer => writer.Write(It.IsAny<string>(), It.IsAny<IEnumerable<dynamic>>()), Times.Never);
        }

        [TestMethod]
        public void Convert_FindReaderAndWriter_CallsReadAndWrite()
        {
            var loggerMock = new Mock<ILogger<Converter>>();

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.SetupGet(configuration => configuration["sourceFormat"]).Returns("csv");
            configurationMock.SetupGet(configuration => configuration["sourcePath"]).Returns(@"C:\temp\test.csv");
            configurationMock.SetupGet(configuration => configuration["destinationFormat"]).Returns("json");
            configurationMock.SetupGet(configuration => configuration["destinationPath"]).Returns(@"C:\temp\test.json");

            var readerMock = new Mock<IReader>();
            var readerFinderMock = new Mock<IServiceFinder<IReader>>();
            readerFinderMock.Setup(readerFinder => readerFinder.Find(It.IsAny<string>())).Returns(readerMock.Object);

            var writerMock = new Mock<IWriter>();
            var writerFinderMock = new Mock<IServiceFinder<IWriter>>();
            writerFinderMock.Setup(writerFinder => writerFinder.Find(It.IsAny<string>())).Returns(writerMock.Object);

            var converter = new Converter(loggerMock.Object, configurationMock.Object, readerFinderMock.Object, writerFinderMock.Object);
            
            converter.Convert();

            readerMock.Verify(reader => reader.Read(It.IsAny<string>()), Times.Once);
            writerMock.Verify(writer => writer.Write(It.IsAny<string>(), It.IsAny<IEnumerable<dynamic>>()), Times.Once);
        }
    }
}

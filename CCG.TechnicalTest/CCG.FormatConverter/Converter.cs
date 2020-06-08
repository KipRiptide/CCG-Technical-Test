using CCG.FormatConverter.Readers;
using CCG.FormatConverter.Services;
using CCG.FormatConverter.Writers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CCG.FormatConverter
{
    public class Converter : IConverter
    {
        public Converter(ILogger<Converter> logger, 
                        IConfiguration configuration,
                        IServiceFinder<IReader> readerFinder, 
                        IServiceFinder<IWriter> writerFinder)
        {
            _logger = logger;
            _configuration = configuration;
            _readerFinder = readerFinder;
            _writerFinder = writerFinder;
        }

        private readonly ILogger<Converter> _logger;
        private readonly IServiceFinder<IReader> _readerFinder;
        private readonly IServiceFinder<IWriter> _writerFinder;
        private readonly IConfiguration _configuration;

        private string SourceFormat => _configuration["sourceFormat"];
        private string SourcePath => _configuration["sourcePath"];
        private string DestinationFormat => _configuration["destinationFormat"];
        private string DestinationPath => _configuration["destinationPath"];
        private IReader Reader => _readerFinder.Find($"{SourceFormat}Reader");
        private IWriter Writer => _writerFinder.Find($"{DestinationFormat}Writer");

        public void Convert()
        {
            try
            {
                if(!CanConvert())
                {
                    return;
                }

                _logger.LogInformation($"Reading {SourceFormat} input from {SourcePath}...");
                var items = Reader.Read(SourcePath);
                _logger.LogInformation($"{items.Count()} items read.");
                _logger.LogInformation($"Writing {DestinationFormat} output...");
                Writer.Write(DestinationPath, items);
                _logger.LogInformation($"Output written to {DestinationPath}.");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error");
                _logger.LogError(ex, "Unexpected error prevented formation conversion from completing.");
            }
        }

        private bool CanConvert()
        {
            if (SourceFormat == null)
            {
                _logger.LogError("No source format specified.");
                return false;
            }

            if (SourcePath == null)
            {
                _logger.LogError("No source path specified.");
                return false;
            }

            if (DestinationFormat == null)
            {
                _logger.LogError("No destination format specified.");
                return false;
            }

            if (DestinationPath == null)
            {
                _logger.LogError("No destination path specified.");
                return false;
            }

            if (Reader == null)
            {
                _logger.LogError($"No reader exists to accept input in '{SourceFormat}' format.");
                return false;
            }


            if (Writer == null)
            {
                _logger.LogError($"No writer exists to generate output in '{DestinationFormat}' format.");
                return false;
            }

            return true;
        }
    }
}

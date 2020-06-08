using CCG.FormatConverter.Builders;
using CCG.FormatConverter.Readers;
using CCG.FormatConverter.Services;
using CCG.FormatConverter.Writers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CCG.FormatConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            AddErrorHandler();
            var config = GetConfiguration(args);
            var serviceProvider = GetServiceProvider(config);
            var formatConverter = serviceProvider.GetService<IConverter>();
            formatConverter.Convert();
        }

        static IConfiguration GetConfiguration(string[] args)
        {

            var switchMappings = new Dictionary<string, string>()
            {
                {"-sf", "sourceFormat" },
                {"-sp", "sourcePath" },
                {"-df", "destinationFormat" },
                {"-dp", "destinationPath" },
            };
            var builder = new ConfigurationBuilder();
            builder.AddCommandLine(args, switchMappings);
            return builder.Build();
        }

        static void AddErrorHandler()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(ErrorHandler);
        }

        static void ErrorHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Console.WriteLine("Error : " + e.Message);
            Console.WriteLine("Runtime terminating: {0}", args.IsTerminating);
        }

        static ServiceProvider GetServiceProvider(IConfiguration configuration)
        {
            return new ServiceCollection()
                        .AddLogging(builder => builder.AddConsole())
                        .AddSingleton(configuration)
                        .AddSingleton<IDynamicObjectBuilder, DynamicObjectBuilder>()
                        .AddSingleton<IReader, CsvReader>()
                        .AddSingleton<IWriter, JsonWriter>()
                        .AddSingleton<IWriter, XmlWriter>()
                        .AddSingleton<IServiceFinder<IReader>, ServiceFinder<IReader>>()
                        .AddSingleton<IServiceFinder<IWriter>, ServiceFinder<IWriter>>()
                        .AddSingleton<IConverter, Converter>()
                        .BuildServiceProvider();
        }




    }




}

using CCG.FormatConverter.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CCG.FormatConverter.Readers
{
    public class CsvReader : IReader
    {
        public CsvReader(IDynamicObjectBuilder dynamicObjectBuilder)
        {
            _dynamicObjectBuilder = dynamicObjectBuilder;
        }

        private readonly IDynamicObjectBuilder _dynamicObjectBuilder;

        /// <summary>
        /// Reads the a contents of a CSV file at the specified <paramref name="sourcePath"/>, and returns a collection of dynamic objects that represent each record.
        /// </summary>
        /// <param name="sourcePath">The path of the CSV file to be read.</param>
        /// <returns>A collection of dynamic objects.</returns>
        public IEnumerable<dynamic> Read(string sourcePath)
        {
            if (sourcePath == null)
            {
                throw new ArgumentNullException(nameof(sourcePath));
            }

            Queue<string> lines = GetLines(sourcePath);
            // if we've got no lines, or only headers in the csv file, then we've got no objects to build and return
            if (lines.Count() < 2)
            {
                return Enumerable.Empty<dynamic>();
            }

            return GetItems(lines);
        }

        /// <summary>
        /// Gets all lines from the source CSV as a queue for processing
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private Queue<string> GetLines(string sourcePath)
        {
            return new Queue<string>(File.ReadAllLines(sourcePath));
        }

        /// <summary>
        /// Retrieves the comma separated values from a line of CSV text
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        private string[] ReadLine(Queue<string> lines)
        {
            return lines.Dequeue().Split(',');
        }

        /// <summary>
        /// Retrieves a list of dynamic objects based on the lines of the CSV text
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        private List<dynamic> GetItems(Queue<string> lines)
        {
            List<dynamic> items = new List<dynamic>();
            string[] headers = ReadLine(lines);
            while(lines.Any())
            {
                dynamic item = _dynamicObjectBuilder.Build(headers, ReadLine(lines));
                items.Add(item);
            }
            return items;
        }

        
    }
}

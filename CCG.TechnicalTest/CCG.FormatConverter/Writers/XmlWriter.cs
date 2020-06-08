using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CCG.FormatConverter.Writers
{
    public class XmlWriter : IWriter
    {
        /// <summary>
        /// Writes a collection of dynamic objects specified by <paramref name="items"/> to a json file at <paramref name="destinationPath"/>.
        /// </summary>
        /// <param name="destinationPath">The path to the file the XML output is to be written to. If file does not exist, it will be created. If it does exist, contents will be overwritten.</param>
        /// <param name="items">Collection of dynamic objects to be written to XML format.</param>
        public void Write(string destinationPath, IEnumerable<dynamic> items)
        {
            if (destinationPath == null)
            {
                throw new ArgumentNullException(nameof(destinationPath));
            }


            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            string json = JsonConvert.SerializeObject(new { item = items });
            XDocument xDocument = JsonConvert.DeserializeXNode(json, "items");
            xDocument.Save(destinationPath);
        }
    }
}

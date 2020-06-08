using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CCG.FormatConverter.Writers
{
    public interface IWriter
    {
        void Write(string destinationPath, IEnumerable<dynamic> items);
    }
}

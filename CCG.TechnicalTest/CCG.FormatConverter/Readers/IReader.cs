using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CCG.FormatConverter.Readers
{
    public interface IReader
    {
        IEnumerable<dynamic> Read(string sourcePath);
    }
}

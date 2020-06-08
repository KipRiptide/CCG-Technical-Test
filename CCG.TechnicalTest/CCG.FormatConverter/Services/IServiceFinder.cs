using System;
using System.Collections.Generic;
using System.Text;

namespace CCG.FormatConverter.Services
{
    public interface IServiceFinder<T>
    {
        T Find(string typeName);
    }
}

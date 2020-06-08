using System;
using System.Collections.Generic;
using System.Text;

namespace CCG.FormatConverter.Builders
{
    public interface IDynamicObjectBuilder
    {
        dynamic Build(string[] fields, string[] values);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCG.FormatConverter.Services
{
    public class ServiceFinder<T>: IServiceFinder<T>
    {
        public ServiceFinder(IEnumerable<T> services)
        {
            _services = services;
        }

        private readonly IEnumerable<T> _services;

        public T Find(string typeName)
        {
            return _services.FirstOrDefault(service => service.GetType().Name.Equals(typeName, StringComparison.OrdinalIgnoreCase));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MindBeacon.Service
{
    /// <summary>
    /// Signals that a entity was not found.
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName)
            : base($"{entityName} not found.")
        { }
    }
}

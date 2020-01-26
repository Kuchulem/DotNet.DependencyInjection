using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.DotNet.DependencyInjection.Exceptions
{
    /// <summary>
    /// Throw when trying to register a dependency that has already been registered
    /// </summary>
    public class AlreadyRegisteredDependencyException : Exception
    {
        public AlreadyRegisteredDependencyException(Type type)
            : base($"A dependency for {type.Name} is already registered")
        {
        }
    }
}

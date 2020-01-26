using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.DotNet.DependencyInjection.Exceptions
{
    /// <summary>
    /// Thrown when trying to resolve a dependency that was not previously registered.
    /// </summary>
    public class NotRegisteredDependencyException : Exception
    {
        public NotRegisteredDependencyException(Type type)
            : base($"Dependency {type.Name} not registered in the dependency service.")
        {
        }
    }
}

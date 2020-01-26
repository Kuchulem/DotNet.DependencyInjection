using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.DotNet.DependencyInjection.Exceptions
{
    /// <summary>
    /// Thrown when the implementation of a dependency can not be instantiated, ie : an abstract class. 
    /// </summary>
    public class ImplementationNotInstanciableException : Exception
    {
        public ImplementationNotInstanciableException(Type type)
            : base($"Can't instanciate {type.Name}. No constructor found")
        {
        }
    }
}

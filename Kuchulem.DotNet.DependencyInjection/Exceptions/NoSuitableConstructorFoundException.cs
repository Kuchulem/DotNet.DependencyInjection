using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.DotNet.DependencyInjection.Exceptions
{
    /// <summary>
    /// Thrown when an implementation can not be instaited because it has no default constructor and no constructor
    /// with registered dependencies to inject
    /// </summary>
    public class NoSuitableConstructorFoundException : Exception
    {
        public NoSuitableConstructorFoundException(Type type)
            : base($"No suitable constructor found for {type.Name}. Neither a default constructor or constructor with known dependencies where found.")
        {
        }
    }
}

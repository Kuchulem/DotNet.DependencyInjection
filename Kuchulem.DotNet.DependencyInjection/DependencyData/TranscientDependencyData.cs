using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Kuchulem.DotNet.DependencyInjection.DependencyData
{
    /// <summary>
    /// Describes a registered dependency using
    /// <em>DependencyInjectionService.Register&lt;TDependency, ITîmplementation&gt;()</em>
    /// method.
    /// </summary>
    public class TranscientDependencyData
    {
        /// <summary>
        /// The Type of the implementation to instanciate.
        /// </summary>
        public Type ImplementationType { get; set; }

        /// <summary>
        /// The first suitable constructor found to be used.
        /// </summary>
        public ConstructorInfo ConstructorInfo { get; set; }
    }
}

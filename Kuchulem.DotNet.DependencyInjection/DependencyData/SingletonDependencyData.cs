using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.DotNet.DependencyInjection.DependencyData
{
    /// <summary>
    /// Describes a registered dependency using
    /// <em>DependencyInjectionService.Singleton&lt;TDependency, ITîmplementation&gt;()</em>
    /// or <em>DependencyInjectionService.Singleton&lt;TDependency&gt;(TDependecy implementation)</em>
    /// methods.
    /// </summary>
    public class SingletonDependencyData : TranscientDependencyData
    {
        /// <summary>
        /// The instance to return if previously resolved. That property will be null
        /// if the instance was not yet resolved.
        /// </summary>
        public object Instance { get; set; }
    }
}

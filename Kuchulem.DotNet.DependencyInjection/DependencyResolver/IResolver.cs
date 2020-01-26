using Kuchulem.DotNet.DependencyInjection.DependencyData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.DotNet.DependencyInjection.DependencyResolver
{
    public interface IResolver
    {
        /// <summary>
        /// Resovles a dependency using the generic as dependency interface / base class
        /// </summary>
        /// <typeparam name="TDependency">the dependency interface / base class</typeparam>
        /// <param name="dependencyData">The data containing the implementation type and, if already resolved, the constructor</param>
        /// <param name="dependencyLock">Locks the dependency service to ensure if to be thread safe</param>
        /// <returns></returns>
        TDependency Resolve<TDependency>(TranscientDependencyData dependencyData, object dependencyLock);

        /// <summary>
        /// Resovles a dependency using the type of the dependency interface / base class
        /// </summary>
        /// <param name="dependencyData">The data containing the implementation type and, if already resolved, the constructor</param>
        /// <param name="dependencyLock">Locks the dependency service to ensure if to be thread safe</param>
        /// <returns></returns>
        object Resolve(TranscientDependencyData dependencyData, object dependencyLock);
    }
}

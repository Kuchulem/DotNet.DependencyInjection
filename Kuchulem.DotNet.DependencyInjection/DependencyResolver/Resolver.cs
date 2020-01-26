using Kuchulem.DotNet.DependencyInjection.DependencyData;
using Kuchulem.DotNet.DependencyInjection.Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Kuchulem.DotNet.DependencyInjection.DependencyResolver
{
    internal class Resolver : IResolver
    {
        /// <summary>
        /// Resovles a dependency using the type of the dependency interface / base class
        /// </summary>
        /// <param name="dependencyData">The data containing the implementation type and, if already resolved, the constructor</param>
        /// <param name="dependencyLock">Locks the dependency service to ensure if to be thread safe</param>
        /// <returns></returns>
        public object Resolve(TranscientDependencyData dependencyData, object dependencyLock)
        {
            // lock the dependency service for it to be thread safe
            // Getting the constructor info modifies the DependencyService
            // the lock avoid collision and doing the same thing twice.
            lock (dependencyLock)
            {
                if (dependencyData.ConstructorInfo is null)
                {
                    GetConstructorInfo(dependencyData);
                }

            }
            // Release the lock as soon and possible
            // Construct(DependencyData) will not modify the DependencyInjectionService

            return Construct(dependencyData);
        }

        /// <summary>
        /// Resovles a dependency using the generic as dependency interface / base class
        /// </summary>
        /// <typeparam name="TDependency">the dependency interface / base class</typeparam>
        /// <param name="dependencyData">The data containing the implementation type and, if already resolved, the constructor</param>
        /// <param name="dependencyLock">Locks the dependency service to ensure if to be thread safe</param>
        /// <returns></returns>
        public TDependency Resolve<TDependency>(TranscientDependencyData dependencyData, object dependencyLock)
        {
            return (TDependency)Resolve(dependencyData, dependencyLock);
        }

        private void GetConstructorInfo(TranscientDependencyData dependencyData)
        {
            var constructors = dependencyData.ImplementationType.GetConstructors();

            if (constructors.Length == 0)
                throw new ImplementationNotInstanciableException(dependencyData.ImplementationType);

            dependencyData.ConstructorInfo = TryGetFirstEligibleConstructor(dependencyData);

            if (dependencyData.ConstructorInfo is null)
                throw new NoSuitableConstructorFoundException(dependencyData.ImplementationType);
        }

        private ConstructorInfo TryGetFirstEligibleConstructor(TranscientDependencyData dependencyData)
        {
            foreach (var constructor in dependencyData.ImplementationType.GetConstructors())
            {
                var eligible = true;
                foreach (var parameter in constructor.GetParameters())
                {
                    if (!DependencyInjectionService.IsRegistered(parameter.ParameterType))
                    {
                        eligible = false;
                        break;
                    }
                }

                if (eligible)
                    return constructor;
            }

            return null;
        }

        private object Construct(TranscientDependencyData dependencyData)
        {
            var parameters = dependencyData.ConstructorInfo.GetParameters();
            List<object> constructorParameters = new List<object>();

            try
            {
                foreach (var parameter in parameters)
                {
                    var paramType = parameter.ParameterType;
                    constructorParameters.Add(DependencyInjectionService.Resolve(paramType));
                }

                return Activator.CreateInstance(dependencyData.ImplementationType, constructorParameters.ToArray());
            }
            catch (NotRegisteredDependencyException)
            {
                return null;
            }
        }
    }
}

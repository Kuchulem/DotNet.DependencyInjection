using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Kuchulem.DotNet.DependencyInjection.DependencyData;
using Kuchulem.DotNet.DependencyInjection.DependencyResolver;
using Kuchulem.DotNet.DependencyInjection.Exceptions;

namespace Kuchulem.DotNet.DependencyInjection
{
    public class DependencyInjectionService
    {
        private static readonly Dictionary<Type, TranscientDependencyData> dependencies = new Dictionary<Type, TranscientDependencyData>();

        private static readonly Dictionary<Type, SingletonDependencyData> singletons = new Dictionary<Type, SingletonDependencyData>();

        private static readonly List<Type> assemblyDependencies = new List<Type>();

        private static readonly List<Assembly> assemblies = new List<Assembly>();

        private static readonly object dependencyLock = new object();

        private static IResolver Resolver = new Resolver();

        /// <summary>
        /// Used to change the object that will resolve dependencies.
        /// </summary>
        /// <param name="resolver">The IResolver implementation to use</param>
        public static void ResolveUsing(IResolver resolver)
        {
            Resolver = resolver;
        }

        /// <summary>
        /// Reverts the resolver to the default one.
        /// </summary>
        public static void ResolveUsingDefaultResolver()
        {
            Resolver = new Resolver();
        }

        /// <summary>
        /// Registers a dependency
        /// </summary>
        /// <typeparam name="TDependency">The interface / base class of the dependency</typeparam>
        /// <typeparam name="TImplementation">The implementation class to use</typeparam>
        public static void Register<TDependency, TImplementation>()
            where TDependency : class
            where TImplementation : TDependency
        {
            Register<TDependency>(typeof(TImplementation));
        }

        /// <summary>
        /// Registers a dependency with the implementation type.
        /// </summary>
        /// <typeparam name="TDependency"></typeparam>
        /// <param name="implementationType"></param>
        public static void Register<TDependency>(Type implementationType)
            where TDependency : class
        {
            Register(typeof(TDependency), implementationType);
        }

        public static void Register(Type dependencyType, Type implementationType)
        {
            lock (dependencyLock)
            {
                if (!IsRegistered(dependencyType))
                    dependencies[dependencyType] = new TranscientDependencyData
                    {
                        ImplementationType = implementationType,
                    };
                else
                    throw new AlreadyRegisteredDependencyException(dependencyType); // dependency already registered

            }
        }

        /// <summary>
        /// Register a dependency that will be resolved by looking in assemblies registered
        /// with <i>AddSourceAssmbly(Assembly)</i>.
        /// </summary>
        /// <typeparam name="TDependency"></typeparam>
        public static void Register<TDependency>()
            where TDependency : class
        {
            lock (dependencyLock)
            {
                if (!IsRegistered<TDependency>())
                    assemblyDependencies.Add(typeof(TDependency));
                else
                    throw new AlreadyRegisteredDependencyException(typeof(TDependency));
            }
        }

        /// <summary>
        /// Adds an assembly to look for when resolving a dependency registered with
        /// the <i>Register&lt;TDependency&gt;()</i> method
        /// </summary>
        /// <param name="assembly"></param>
        public static void AddSourceAssembly(Assembly assembly)
        {
            lock (dependencyLock)
            {
                if (!assemblies.Contains(assembly))
                    assemblies.Add(assembly);
            }
        }

        /// <summary>
        /// Registers a singleton with an already instantiated implementation.<br/>
        /// the <em>Resolve&lt;TDependency&gt;(TDependency implementation)</em> method 
        /// will always return that instance.
        /// </summary>
        /// <typeparam name="TDependency">The dependency interface / base class</typeparam>
        /// <param name="implementation">The implementation instance to use</param>
        public static void Singleton<TDependency>(TDependency implementation) where TDependency : class
        {
            singletons[typeof(TDependency)] = new SingletonDependencyData
            {
                ImplementationType = implementation.GetType(),
                Instance = implementation
            };
        }

        /// <summary>
        /// Registered a singleton for TDependency using TImplementation implementation.<br/>
        /// The first time the <em>Resolve&lt;TDependency, TImplementation&gt;()</em> method 
        /// is used for that dependency it will create a new instance. All following calls 
        /// will return the same instance.
        /// </summary>
        /// <typeparam name="TDependency">The dependency interface / base class</typeparam>
        /// <typeparam name="TImplementation">The implementation class</typeparam>
        public static void Singleton<TDependency, TImplementation>()
            where TDependency : class
            where TImplementation : TDependency
        {
            singletons[typeof(TDependency)] = new SingletonDependencyData
            {
                ImplementationType = typeof(TImplementation)
            };
        }

        /// <summary>
        /// Checkes if the dependency is registered, either with the 
        /// <em>Register&lt;TDependency, ITîmplementation&gt;()</em> method or the 
        /// <em>Singleton&lt;TDependency&gt;(TDependecy implementation)</em> or 
        /// <em>Singleton&lt;TDependency, TImplementation&gt;()</em> methods.
        /// </summary>
        /// <typeparam name="TDependency">The dependency interface / base class</typeparam>
        /// <returns></returns>
        public static bool IsRegistered<TDependency>()
        {
            return IsRegistered(typeof(TDependency));
        }

        /// <summary>
        /// Checks if the dependency is registered using its type.
        /// </summary>
        /// <param name="type">The type of the dependency interface / base class</param>
        /// <returns></returns>
        public static bool IsRegistered(Type dependencyType)
        {
            return
                dependencies.ContainsKey(dependencyType) ||
                assemblyDependencies.Contains(dependencyType) ||
                singletons.ContainsKey(dependencyType);
        }

        /// <summary>
        /// Resolves a dependency and return it.<br/>
        /// If the dependency is transcient (registered with <em>Register&lt;TDependency, ITîmplementation&gt;()</em>)
        /// will return a new instance with all constructor depencies resolved.<br/>
        /// If registered using <em>Singleton&lt;TDependency&gt;()</em> will return the same instance at each call.
        /// Also with all constructor dependencies resolved.
        /// </summary>
        /// <typeparam name="TDependency"></typeparam>
        /// <returns></returns>
        public static TDependency Resolve<TDependency>() where TDependency : class
        {
            return (TDependency)Resolve(typeof(TDependency));
        }

        /// <summary>
        /// <em>This version is mostly for internal purpose. Prefere the </em>Resolve&lt;TDependency&gt;()<em> overload
        /// that will return a casted result.</em><br/>
        /// Resolves a dependency and return it.<br/>
        /// If the dependency is transcient (registered with <em>Register&lt;TDependency, ITîmplementation&gt;()</em>)
        /// will return a new instance with all constructor depencies resolved.<br/>
        /// If registered using <em>Singleton&lt;TDependency&gt;()</em> will return the same instance at each call.
        /// Also with all constructor dependencies resolved.
        /// </summary>
        /// <param name="dependencyType"></param>
        /// <returns></returns>
        public static object Resolve(Type dependencyType)
        {
            if (dependencies.ContainsKey(dependencyType))
                return Resolver.Resolve(dependencies[dependencyType], dependencyLock);

            if (singletons.ContainsKey(dependencyType))
            {
                lock (dependencyLock)
                {
                    if (singletons[dependencyType].Instance is null)
                        singletons[dependencyType].Instance = Resolver.Resolve(singletons[dependencyType], dependencyLock);
                }

                return singletons[dependencyType].Instance;
            }

            if(assemblyDependencies.Contains(dependencyType))
            {
                Type implementationType = null;
                lock(dependencyLock)
                {
                    implementationType = SearchImplementation(dependencyType);

                    if (implementationType != null)
                    {
                        Register(dependencyType, implementationType);
                    }
                }

                if (implementationType != null)
                    return Resolver.Resolve(dependencies[dependencyType], dependencyLock);
            }

            throw new NotRegisteredDependencyException(dependencyType);
        }

        /// <summary>
        /// Creates a new instance of T class injecting all its constructor dependencies.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Inject<T>() where T : class
        {
            return Resolver.Resolve<T>(new TranscientDependencyData { ImplementationType = typeof(T) }, dependencyLock);
        }

        private static Type SearchImplementation(Type dependencyType)
        {
            foreach(var assembly in assemblies)
            {
                var type = SearchImplementation(dependencyType, assembly);
                if (type != null)
                    return type;
            }

            return null;
        }

        private static Type SearchImplementation(Type dependencyType, Assembly assembly)
        {
            foreach(var type in assembly.GetTypes())
            {
                if (dependencyType.IsAssignableFrom(type))
                    return type;
            }

            return null;
        }
    }
}

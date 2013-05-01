using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XPlatUtils
{
    /// <summary>
    /// A simple service container implementation, singleton only
    /// </summary>
    public static class ServiceContainer
    {
        static readonly Dictionary<Type, Lazy<object>> services = new Dictionary<Type, Lazy<object>>();
        static readonly Stack<Dictionary<Type, object>> scopedServices = new Stack<Dictionary<Type, object>>();

        /// <summary>
        /// Register the specified service with an instance
        /// </summary>
        public static void Register<T>(T service)
        {
            services[typeof(T)] = new Lazy<object>(() => service);
        }

        /// <summary>
        /// Register the specified service for a class with a default constructor
        /// </summary>
        public static void Register<T>() where T : new()
        {
            services[typeof(T)] = new Lazy<object>(() => new T());
        }

        /// <summary>
        /// Register the specified service with a callback to be invoked when requested
        /// </summary>
        public static void Register<T>(Func<T> function)
        {
            services[typeof(T)] = new Lazy<object>(() => function());
        }

        /// <summary>
        /// Register the specified service with an instance
        /// </summary>
        public static void Register(Type type, object service)
        {
            services[type] = new Lazy<object>(() => service);
        }

        /// <summary>
        /// Register the specified service with a callback to be invoked when requested
        /// </summary>
        public static void Register(Type type, Func<object> function)
        {
            services[type] = new Lazy<object>(function);
        }

        /// <summary>
        /// Register the specified service with an instance that is scoped
        /// </summary>
        public static void RegisterScoped<T>(T service)
        {
            Dictionary<Type, object> services;
            if (scopedServices.Count == 0)
            {
                services = new Dictionary<Type, object>();
                scopedServices.Push(services);
            }
            else
            {
                services = scopedServices.Peek();
            }

            services[typeof(T)] = service;
        }

        /// <summary>
        /// Resolves the type, throwing an exception if not found
        /// </summary>
        public static T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        /// <summary>
        /// Resolves the type, throwing an exception if not found
        /// </summary>
        public static object Resolve(Type type)
        {
            //Scoped services
            if (scopedServices.Count > 0)
            {
                var services = scopedServices.Peek();

                object service;
                if (services.TryGetValue(type, out service))
                {
                    return service;
                }
            }

            //Non-scoped services
            {
                Lazy<object> service;
                if (services.TryGetValue(type, out service))
                {
                    return service.Value;
                }
                else
                {
                    throw new KeyNotFoundException(string.Format("Service not found for type '{0}'", type));
                }
            }
        }

        /// <summary>
        /// Adds a "scope" which is a way to register a service on a stack to be popped off at a later time
        /// </summary>
        public static void AddScope()
        {
            scopedServices.Push(new Dictionary<Type, object>());
        }

        /// <summary>
        /// Removes the current "scope" which pops off some local services
        /// </summary>
        public static void RemoveScope()
        {
            if (scopedServices.Count > 0)
                scopedServices.Pop();
        }

        /// <summary>
        /// Mainly for testing, clears the entire container
        /// </summary>
        public static void Clear()
        {
            services.Clear();
            scopedServices.Clear();
        }
    }
}
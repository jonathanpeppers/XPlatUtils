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
		static readonly Dictionary<Type, Lazy<object>> services;

		static ServiceContainer ()
		{
			services = new Dictionary<Type, Lazy<object>> ();

			Register<IMessengerHub> (() => new MessengerHub ());
		}

		/// <summary>
		/// Register the specified service with an instance
		/// </summary>
		public static void Register<T> (T service)
		{
			services [typeof(T)] = new Lazy<object> (() => service);
		}

		/// <summary>
		/// Register the specified service for a class with a default constructor
		/// </summary>
		public static void Register<T> () where T : new ()
		{
			services [typeof(T)] = new Lazy<object> (() => new T ());
		}

		/// <summary>
		/// Register the specified service with a callback to be invoked when requested
		/// </summary>
		public static void Register<T> (Func<object> function)
		{
			services [typeof(T)] = new Lazy<object> (function);
		}

		/// <summary>
		/// Resolves the type, throwing an exception if not found
		/// </summary>
		public static T Resolve<T> ()
		{
			Lazy<object> service;
			if (services.TryGetValue (typeof(T), out service)) {
				return (T)service.Value;
			} else {
				throw new KeyNotFoundException (string.Format ("Service not found for type '{0}'", typeof(T)));
			}
		}

		/// <summary>
		/// Mainly for testing, clears the entire container
		/// </summary>
		public static void Clear()
		{
			services.Clear();
		}
	}
}

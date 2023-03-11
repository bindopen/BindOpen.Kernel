using BindOpen.Extensions.Connectors;
using BindOpen.Scopes.Scopes;

namespace BindOpen.Scopes
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static class BdoScopeExtensions
    {
        /// <summary>
        /// Creates a new service.
        /// </summary>
        /// <param key="scope"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Returns the log of the operation.</returns>
        public static T CreateScoped<T>(
            this IBdoScope scope)
            where T : IBdoScoped, new()
        {
            var service = new T
            {
                Scope = scope
            };

            return service;
        }

        /// <summary>
        /// Creates a new connected service.
        /// </summary>
        /// <param key="scope"></param>
        /// <param key="connector"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Returns the log of the operation.</returns>
        public static T CreateConnected<T>(
            this IBdoScope scope,
            IBdoConnector connector)
            where T : IBdoConnected, new()
        {
            var service = scope.CreateScoped<T>();

            service.WithConnector(connector);

            return service;
        }
    }
}

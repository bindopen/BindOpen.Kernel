using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// This static class extends .Net core configuration.
    /// </summary>
    public static class ConfigurationExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static T GetBdoValue<T>(
            this IConfiguration configuration,
            string key, IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
            where T : class
        {
            return configuration?.GetBdoValue<T>(key, default, scope, varElementSet, log);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <param name="scope"></param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static T GetBdoValue<T>(
            this IConfiguration configuration,
            string key,
            string defaultValue,
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
            where T : class
        {
            return configuration?.GetBdoValue(typeof(T), key, defaultValue, scope, varElementSet, log) as T;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetBdoValue(
            this IConfiguration configuration,
            Type type,
            string key)
        {
            if (configuration == default) return default;
            return configuration.GetValue(type, key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <param name="scope"></param>
        /// <param name="varElementSet">The script variable set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static object GetBdoValue(
            this IConfiguration configuration,
            Type type,
            string key,
            string defaultValue,
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
        {
            if (configuration == default) return default;
            var value = configuration.GetValue<string>(key, defaultValue);

            if (scope != null)
            {
                var interpreter = scope.NewScriptInterpreter();

                return Convert.ChangeType(interpreter?.Evaluate(value, BdoExpressionKind.Script, varElementSet, log), type);
            }
            else if (type == typeof(string))
            {
                return Convert.ChangeType(value, type);
            }
            return default;
        }
    }
}
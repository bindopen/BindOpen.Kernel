using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.System.Diagnostics;
using BindOpen.Framework.System.Scripting;
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
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static T GetBdoValue<T>(this IConfiguration configuration, string key, IBdoScope scope = null, ScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
            where T : class
        {
            return configuration?.GetBdoValue<T>(key, default, scope, scriptVariableSet, log);
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
        public static T GetBdoValue<T>(this IConfiguration configuration,
            string key,
            string defaultValue,
            IBdoScope scope = null,
            ScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
            where T : class
        {
            return configuration?.GetBdoValue(typeof(T), key, defaultValue, scope, scriptVariableSet, log) as T;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <param name="scope"></param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static object GetBdoValue(this IConfiguration configuration, Type type, string key)
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
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static object GetBdoValue(this IConfiguration configuration,
            Type type,
            string key,
            string defaultValue,
            IBdoScope scope = null, ScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            if (configuration == default) return default;
            var value = configuration.GetValue<string>(key, defaultValue);

            if (scope?.Interpreter != null)
            {
                return Convert.ChangeType(scope?.Interpreter?.Evaluate(value, scriptVariableSet, log), type);
            }
            else if (type == typeof(string))
            {
                return Convert.ChangeType(value, type);
            }
            return default;
        }
    }
}
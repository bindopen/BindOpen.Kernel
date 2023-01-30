using BindOpen.Data.Meta;
using BindOpen.Data.Items;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// This static class extends .Net core config.
    /// </summary>
    public static class ConfigurationExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <param name="key"></param>
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static T GetBdoValue<T>(
            this IConfiguration config,
            string key, IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            where T : class
        {
            return config?.GetBdoValue<T>(key, default, scope, varSet, log);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <param name="scope"></param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static T GetBdoValue<T>(
            this IConfiguration config,
            string key,
            string defaultValue,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            where T : class
        {
            return config?.GetBdoValue(typeof(T), key, defaultValue, scope, varSet, log) as T;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetBdoValue(
            this IConfiguration config,
            Type type,
            string key)
        {
            if (config == default) return default;
            return config.GetValue(type, key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <param name="scope"></param>
        /// <param name="varSet">The script variable set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static object GetBdoValue(
            this IConfiguration config,
            Type type,
            string key,
            string defaultValue,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (config == default) return default;
            var value = config.GetValue<string>(key, defaultValue);

            if (scope != null)
            {
                var interpreter = scope.NewScriptInterpreter();

                return Convert.ChangeType(interpreter?.Evaluate(value, BdoExpressionKind.Script, varSet, log), type);
            }
            else if (type == typeof(string))
            {
                return Convert.ChangeType(value, type);
            }
            return default;
        }
    }
}
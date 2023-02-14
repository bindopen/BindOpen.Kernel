using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class BdoMetaListExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T FromObject<T>(
            this T list,
            object obj,
            Type type = null,
            bool onlyMetaAttributes = false)
            where T : IBdoMetaList
        {
            list?.With(
                obj.ToMetaArray(type, onlyMetaAttributes));
            return list;
        }

        /// <summary>
        /// Creates a data element list from a dynamic object.
        /// </summary>
        /// <param name="obj">The objet to consider.</param>
        public static IBdoMetaList ToMetaList(
            this object obj,
            Type type = null,
            bool onlyMetaAttributes = true)
            => obj.ToMetaList<BdoMetaList>(type, onlyMetaAttributes);

        /// <summary>
        /// Creates a data element list from a dynamic object.
        /// </summary>
        /// <param name="obj">The objet to consider.</param>
        public static T ToMetaList<T>(
            this object obj,
            Type type = null,
            bool onlyMetaAttributes = true)
            where T : class, IBdoMetaList, new()
        {
            T list = default;

            if (obj != null)
            {
                type ??= obj.GetType();

                if (!type.IsScalar())
                {
                    list = new();
                    foreach (var propInfo in type.GetProperties(BindingFlags.Public))
                    {
                        var bdoAttribute = propInfo.GetCustomAttribute(typeof(BdoDataAttribute)) as BdoDataAttribute;
                        if (bdoAttribute != null || !onlyMetaAttributes)
                        {
                            string propName = propInfo.Name;
                            object propValue = propInfo.GetValue(obj);

                            if (!string.IsNullOrEmpty(bdoAttribute?.Name))
                            {
                                propName = bdoAttribute.Name;
                            }
                            list.Add(propValue.ToMetaData(propName));
                        }
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Sets information of the specified prop.
        /// </summary>
        /// <param name="obj">The object to update.</param>
        /// <param name="list">The list of elements to return.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element list to use.</param>
        /// <param name="log">The log to consider.</param>
        public static void UpdateFromMeta(
            this object obj,
            IBdoMetaList list,
            bool onlyMetaAttributes = true,
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
        {
            if (obj == null || !list.Has()) return;

            foreach (var propInfo in obj.GetType().GetProperties())
            {
                var bdoAttribute = propInfo.GetCustomAttribute(typeof(BdoDataAttribute)) as BdoDataAttribute;
                if (bdoAttribute != null || !onlyMetaAttributes)
                {
                    string name = bdoAttribute.Name;
                    if (string.IsNullOrEmpty(name))
                    {
                        name = propInfo.Name;
                    }

                    try
                    {
                        if (list.Has(name))
                        {
                            var type = propInfo.PropertyType;
                            var value = list.GetData(name, scope, varSet, log);
                            if (value != null)
                            {
                                if (type.IsEnum)
                                {
                                    if (!value.GetType().IsEnum && Enum.IsDefined(type, value))
                                    {
                                        value = Enum.Parse(type, value as string);
                                    }
                                }
                            }
                            else if (value?.GetType() == typeof(Dictionary<string, object>)
                                && type.IsGenericType
                                && type.GetGenericTypeDefinition() == typeof(Dictionary<,>)
                                && type != typeof(Dictionary<string, object>))
                            {
                                Type itemType = type.GetGenericArguments()[0];

                                var dictionary = Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(typeof(string), itemType));
                                var method = dictionary.GetType().GetMethod("Add", new Type[] { typeof(string), itemType });

                                foreach (var item in value as Dictionary<string, object>)
                                {
                                    method.Invoke(dictionary, new object[] { item.Key, Convert.ChangeType(item.Value, itemType) });
                                }
                                value = dictionary;
                            }

                            propInfo.SetValue(obj, value);
                        }
                    }
                    catch (Exception ex)
                    {
                        log?.AddException(ex);
                    }
                }
            }
        }

        // Add

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param name="pairs">The value to add.</param>
        public static T Add<T>(
            this T list,
            params KeyValuePair<string, object>[] pairs)
            where T : IBdoMetaList
        {
            if (list != null)
            {
                foreach (var pair in pairs)
                {
                    list.Add(pair.Key, pair.Value);
                }
            }

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T Add<T>(
            this T list,
            object value)
            where T : IBdoMetaList
        {
            list?.Add(null, value);

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        /// <param name="availableKeys">The available keys to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T Add<T>(
            this T list,
            string key, object value)
            where T : IBdoMetaList
        {
            list?.Add(BdoMeta.New(key, value));

            return list;
        }

        // With

        /// <summary>
        /// Withs a new value to this instance.
        /// </summary>
        /// <param name="pairs">The value to add.</param>
        public static T With<T>(
            this T list,
            params KeyValuePair<string, object>[] pairs)
            where T : IBdoMetaList
        {
            if (list != null)
            {
                foreach (var pair in pairs)
                {
                    list.With(pair.Key, pair.Value);
                }
            }

            return list;
        }

        /// <summary>
        /// Withs a new value to this instance with the specified key and text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T With<T>(
            this T list,
            object value)
            where T : IBdoMetaList
        {
            list?.With(null, value);

            return list;
        }

        /// <summary>
        /// Withs a new value to this instance with the specified key and text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        /// <param name="availableKeys">The available keys to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T With<T>(
            this T list,
            string key, object value)
            where T : IBdoMetaList
        {
            list?.With(BdoMeta.New(key, value));

            return list;
        }
    }
}
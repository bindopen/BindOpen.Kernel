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
    public static class BdoMetaSetExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T FromObject<T>(
            this T set,
            object obj,
            Type type = null,
            bool onlyMetaAttributes = false)
            where T : ITBdoItemSet<IBdoMetaItem>
        {
            set?.WithItems(
                obj.ToMetaArray(type, onlyMetaAttributes));
            return set;
        }

        /// <summary>
        /// Creates a data element set from a dynamic object.
        /// </summary>
        /// <param name="obj">The objet to consider.</param>
        public static IBdoMetaSet ToMetaSet(
            this object obj,
            Type type = null,
            bool onlyMetaAttributes = true)
            => obj.ToMetaSet<BdoMetaSet>(type, onlyMetaAttributes);

        /// <summary>
        /// Creates a data element set from a dynamic object.
        /// </summary>
        /// <param name="obj">The objet to consider.</param>
        public static T ToMetaSet<T>(
            this object obj,
            Type type = null,
            bool onlyMetaAttributes = true)
            where T : class, IBdoMetaSet, new()
        {
            T set = default;

            if (obj != null)
            {
                type ??= obj.GetType();

                set = new();
                foreach (var propInfo in type.GetProperties())
                {
                    string propName = propInfo.Name;
                    object propValue = propInfo.GetValue(obj);

                    var bdoAttribute = propInfo.GetCustomAttribute(typeof(BdoDataAttribute)) as BdoDataAttribute;
                    if (bdoAttribute != null || !onlyMetaAttributes)
                    {
                        if (!string.IsNullOrEmpty(bdoAttribute?.Name))
                        {
                            propName = bdoAttribute.Name;
                        }
                        set.Add(propValue.ToMetaData(propName));
                    }
                }
            }

            return set;
        }

        /// <summary>
        /// Sets information of the specified prop.
        /// </summary>
        /// <param name="obj">The object to update.</param>
        /// <param name="set">The set of elements to return.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <param name="log">The log to consider.</param>
        public static void UpdateFromMeta(
            this object obj,
            IBdoMetaSet set,
            bool onlyMetaAttributes = true,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (obj == null || !set.HasItem()) return;

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
                        if (set.HasItem(name))
                        {
                            var type = propInfo.PropertyType;
                            var value = set.GetData(name, scope, varSet, log);
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

                                foreach (var item in (value as Dictionary<string, object>))
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
    }
}
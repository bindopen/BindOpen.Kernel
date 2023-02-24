using BindOpen.Data.Items;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BindOpen.Data.Meta.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class BdoMetaDataExtensions
    {
        /// <summary>
        /// Sets information of the specified prop.
        /// </summary>
        /// <param key="obj">The object to update.</param>
        /// <param key="list">The list of elements to return.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element list to use.</param>
        /// <param key="log">The log to consider.</param>
        public static void UpdateFromMeta(
            this object obj,
            IBdoMetaSet list,
            bool onlyMetaAttributes = false,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (obj == null || !list.Has()) return;

            foreach (var propInfo in obj.GetType().GetProperties())
            {
                var bdoAttribute = propInfo.GetCustomAttribute(typeof(BdoPropertyAttribute)) as BdoPropertyAttribute;
                if (bdoAttribute != null || !onlyMetaAttributes)
                {
                    string name = bdoAttribute?.Name;
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
    }
}
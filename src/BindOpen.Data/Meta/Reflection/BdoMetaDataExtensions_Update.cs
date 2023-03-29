using BindOpen.Data.Assemblies;
using BindOpen.Logging;
using BindOpen.Scopes;
using System;
using System.Collections.Generic;
using System.Linq;
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
            string groupId = null,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            obj.UpdateFromMeta<BdoPropertyAttribute>(
                list, onlyMetaAttributes, groupId, scope, varSet, log);
        }
        /// <summary>
        /// Sets information of the specified prop.
        /// </summary>
        /// <param key="obj">The object to update.</param>
        /// <param key="list">The list of elements to return.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element list to use.</param>
        /// <param key="log">The log to consider.</param>
        public static void UpdateFromMeta<T>(
            this object obj,
            IBdoMetaSet list,
            bool onlyMetaAttributes = false,
            string groupId = null,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            where T : BdoPropertyAttribute
        {
            if (obj == null || !list.Has()) return;

            foreach (var propInfo in obj.GetType().GetProperties())
            {
                var hasMetaAttribute = propInfo.GetCustomAttributes(typeof(T)).Any();
                if (hasMetaAttribute || !onlyMetaAttributes)
                {
                    var spec = BdoMeta.NewSpec();
                    spec.UpdateFrom<T>(propInfo);

                    var name = spec.Name;

                    try
                    {
                        if (list.Has(name, groupId))
                        {
                            var type = propInfo.PropertyType;

                            object value;

                            if (typeof(IBdoMetaData).IsAssignableFrom(type))
                            {
                                var meta = BdoMeta.New(name, type);
                                meta?.Update(list.GetFromGroup(name, groupId));
                                value = meta;
                            }
                            else
                            {
                                value = list.GetData(name, scope, varSet, log);

                                if (value != null)
                                {
                                    if (type.IsEnum)
                                    {
                                        if (!value.GetType().IsEnum && Enum.IsDefined(type, value))
                                        {
                                            value = Enum.Parse(type, value as string);
                                        }
                                    }
                                    else if (value.GetType() == typeof(Dictionary<string, object>)
                                        && type.IsGenericType
                                        && type.GetGenericTypeDefinition() == typeof(Dictionary<,>)
                                        && type != typeof(Dictionary<string, object>))
                                    {
                                        Type itemType = type.GetGenericArguments()[0];

                                        var dictionary = typeof(Dictionary<,>).MakeGenericType(typeof(string), itemType).CreateInstance();
                                        var method = dictionary.GetType().GetMethod("Add", new Type[] { typeof(string), itemType });

                                        foreach (var item in value as Dictionary<string, object>)
                                        {
                                            method.Invoke(dictionary, new object[] { item.Key, Convert.ChangeType(item.Value, itemType) });
                                        }
                                        value = dictionary;
                                    }
                                }
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
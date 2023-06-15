using BindOpen.System.Logging;
using BindOpen.System.Data.Assemblies;
using BindOpen.System.Data.Helpers;
using BindOpen.System.Scoping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BindOpen.System.Data.Meta.Reflection
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
            IBdoMetaSet set,
            bool onlyMetaAttributes = false,
            string groupId = null,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            obj.UpdateFromMeta<BdoPropertyAttribute>(
                set, onlyMetaAttributes, groupId, scope, varSet, log);
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

                    Type itemType = null;

                    try
                    {
                        if (list.Has(name, groupId))
                        {
                            var type = propInfo.PropertyType;

                            object value;

                            Type metaType;

                            if (type.IsGenericType)
                            {
                                metaType = type.GetGenericTypeDefinition();
                                itemType = type.GenericTypeArguments.GetAt(0);
                            }
                            else
                            {
                                metaType = type;
                            }

                            IBdoMetaData metaValue = null;

                            if (typeof(IBdoMetaData).IsAssignableFrom(type))
                            {
                                // if current is meta

                                var meta = list.GetOfGroup(name, groupId);

                                if (typeof(ITBdoMetaScalar<>).IsAssignableFrom(metaType)
                                    && itemType?.IsScalar() == true)
                                {
                                    metaValue = (typeof(TBdoMetaScalar<>).MakeGenericType(itemType).CreateInstance() as BdoMetaScalar)
                                        .WithName(name);
                                }
                                else if (typeof(IBdoMetaScalar).IsAssignableFrom(metaType))
                                {
                                    metaValue = BdoMeta.NewScalar(name, meta?.GetSpec().DataValueType);
                                }
                                else if (typeof(IBdoConfiguration).IsAssignableFrom(metaType))
                                {
                                    var config = meta as IBdoConfiguration;
                                    metaValue = BdoMeta.NewConfig(name)
                                        .With(config.Items?.ToArray());
                                }
                                else if (typeof(IBdoMetaSet).IsAssignableFrom(metaType))
                                {
                                    var set = meta as IBdoMetaSet;
                                    metaValue = BdoMeta.NewSet(name)
                                        .With(set?.Items?.ToArray());
                                }
                                else if (typeof(ITBdoMetaObject<>).IsAssignableFrom(metaType)
                                    && itemType != null)
                                {
                                    metaValue = (typeof(TBdoMetaObject<>).MakeGenericType(itemType).CreateInstance() as BdoMetaObject)
                                        .WithName(name);
                                }
                                else if (typeof(IBdoMetaObject).IsAssignableFrom(type))
                                {
                                    metaValue = BdoMeta.NewObject(name);
                                }

                                metaValue?.Update(meta);
                                value = metaValue;
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
                                        itemType = type.GetGenericArguments()[0];

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
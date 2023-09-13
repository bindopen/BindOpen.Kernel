using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Assemblies;
using BindOpen.Kernel.Data.Helpers;
using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BindOpen.Kernel.Data.Meta.Reflection
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
            IBdoMetaSet set,
            bool onlyMetaAttributes = false,
            string groupId = null,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            where T : BdoPropertyAttribute
        {
            if (obj == null || set?.Has() != true) return;

            foreach (var propInfo in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var hasMetaAttribute = propInfo.GetCustomAttributes(typeof(T)).Any();
                if (hasMetaAttribute || !onlyMetaAttributes)
                {
                    IBdoSpec spec = BdoData.NewSpec();
                    spec.UpdateFrom<T>(propInfo);

                    var name = spec?.Name ?? propInfo.Name;

                    Type itemType = null;

                    try
                    {
                        var meta = spec.Reference?.Kind == BdoReferenceKind.Identifier ?
                            set.Descendant<IBdoMetaData>(spec.Reference.Identifier?.Split('/')) : set.GetOfGroup(name, groupId);

                        if (meta != null)
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

                                if (typeof(ITBdoMetaScalar<>).IsAssignableFrom(metaType)
                                    && itemType?.IsScalar() == true)
                                {
                                    metaValue = (typeof(TBdoMetaScalar<>).MakeGenericType(itemType).CreateInstance() as BdoMetaScalar)
                                        .WithName(name);
                                }
                                else if (typeof(IBdoMetaScalar).IsAssignableFrom(metaType))
                                {
                                    metaValue = BdoData.NewScalar(name, meta?.DataType.ValueType);
                                }
                                else if (typeof(ITBdoMetaObject<>).IsAssignableFrom(metaType)
                                    && itemType != null)
                                {
                                    metaValue = (typeof(TBdoMetaObject<>).MakeGenericType(itemType).CreateInstance() as BdoMetaObject)
                                        .WithName(name);
                                }
                                else if (typeof(IBdoMetaObject).IsAssignableFrom(type))
                                {
                                    metaValue = BdoData.NewObject(name);
                                }
                                else if (typeof(IBdoMetaNode).IsAssignableFrom(metaType))
                                {
                                    var subSet = meta as IBdoMetaNode;
                                    metaValue = BdoData.NewNode(name)
                                        .With(subSet?.Items?.ToArray());
                                }

                                metaValue?.Update(meta);
                                value = metaValue;
                            }
                            else
                            {
                                value = meta.GetData(scope, varSet, log);

                                if (value != null)
                                {
                                    if (type.IsEnum)
                                    {
                                        if (!value.GetType().IsEnum && Enum.IsDefined(type, value))
                                        {
                                            value = Enum.Parse(type, value as string);
                                        }
                                    }
                                    else if (
                                        type.IsGenericType
                                        && typeof(TBdoDictionary<>).IsAssignableFrom(type.GetGenericTypeDefinition()))
                                    {
                                        //var itemType0 = type.GetGenericArguments()[0];
                                        itemType = type.GetGenericArguments()[0];

                                        var dictionary = type.GetGenericTypeDefinition().MakeGenericType(itemType).CreateInstance();
                                        var method = dictionary.GetType().GetMethod("Add", new Type[] { typeof(string), itemType });

                                        if (meta is IBdoMetaSet metaSet)
                                        {
                                            foreach (var child in metaSet)
                                            {
                                                var childValue = child?.GetData(scope, varSet, log);
                                                method?.Invoke(dictionary, new object[] { child?.Name, Convert.ChangeType(childValue, itemType) });
                                            }
                                        }
                                        value = dictionary;
                                    }
                                    else if (
                                        type.IsGenericType
                                        && typeof(List<>).IsAssignableFrom(type.GetGenericTypeDefinition()))
                                    {
                                        itemType = type.GetGenericArguments()[0];

                                        var list = type.GetGenericTypeDefinition().MakeGenericType(itemType).CreateInstance();
                                        var method = list.GetType().GetMethod("Add", new Type[] { itemType });

                                        foreach (var item in value as IEnumerable)
                                        {
                                            method?.Invoke(list, new object[] { item });
                                        }
                                        value = list;
                                    }
                                }
                                else
                                {
                                    // if object case we parse sub meta data

                                    if (!type.IsScalar() && meta is IBdoMetaObject metaObject)
                                    {
                                        var metaObj = AssemblyHelper.CreateInstance(type, log);
                                        metaObj.UpdateFromMeta(metaObject, onlyMetaAttributes);
                                        meta.WithData(metaObj);
                                        meta.WithDataType(DataValueTypes.Null);

                                        value = metaObj;
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
using BindOpen.Data.Assemblies;
using BindOpen.Data.Helpers;
using BindOpen.Data.Objects;
using BindOpen.Data.Schema;
using BindOpen.Logging;
using BindOpen.Scoping;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace BindOpen.Data.Meta.Reflection
{
    /// <summary>
    /// Provides reflection-based extension methods for populating CLR objects from metadata sets.
    /// </summary>
    public static partial class BdoMetaDataExtensions
    {
        /// <summary>
        /// Updates the public properties of the specified object from a metadata set
        /// using <see cref="BdoPropertyAttribute"/> mappings.
        /// </summary>
        /// <param name="obj">The target object to update.</param>
        /// <param name="set">The metadata set providing property values.</param>
        /// <param name="onlyMetaAttributes">Indicates whether only properties decorated with <see cref="BdoPropertyAttribute"/> should be updated.</param>
        /// <param name="groupId">Optional group identifier used to filter metadata items.</param>
        /// <param name="scope">The scope used to resolve dynamic expressions or references.</param>
        /// <param name="varSet">Optional variable set used during resolution.</param>
        /// <param name="log">The log instance used to record exceptions.</param>
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
        /// Updates the public properties of the specified object from a metadata set
        /// using attribute mappings of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The attribute type inheriting from <see cref="BdoPropertyAttribute"/>.</typeparam>
        /// <param name="obj">The target object to update.</param>
        /// <param name="set">The metadata set providing property values.</param>
        /// <param name="onlyMetaAttributes">Indicates whether only properties decorated with <typeparamref name="T"/> should be updated.</param>
        /// <param name="groupId">Optional group identifier used to filter metadata items.</param>
        /// <param name="scope">The scope used to resolve dynamic expressions or references.</param>
        /// <param name="varSet">Optional variable set used during resolution.</param>
        /// <param name="log">The log instance used to record exceptions.</param>
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
                    IBdoSchema schema = BdoData.NewSchema();
                    schema.UpdateFrom<T>(propInfo);

                    var name = schema?.Name ?? propInfo.Name;

                    Type itemType = null;

                    try
                    {
                        var meta = schema.Reference?.Kind == BdoReferenceKind.Identifier ?
                            set.Descendant<IBdoMetaData>(schema.Reference.Identifier?.Split('/')) : set.GetOfGroup(name, groupId);

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
                                else if (typeof(IBdoMetaSet).IsAssignableFrom(metaType))
                                {
                                    var subSet = meta as IBdoMetaSet;
                                    metaValue = BdoData.NewSet(name)
                                        .Add(subSet?.Items?.ToArray());
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
                                        && (typeof(ITBdoDictionary<>).IsAssignableFrom(metaType)
                                            || typeof(TBdoDictionary<>).IsAssignableFrom(metaType)
                                            || typeof(IDictionary<,>).IsAssignableFrom(metaType)
                                            || typeof(Dictionary<,>).IsAssignableFrom(metaType)))
                                    {
                                        var genericArgs = type.GetGenericArguments();
                                        itemType = genericArgs.Length == 2 ? genericArgs[1] : genericArgs[0];

                                        var dictionaryType = type.IsInterface
                                            ? (genericArgs.Length == 2
                                                ? typeof(Dictionary<,>).MakeGenericType(genericArgs[0], itemType)
                                                : typeof(TBdoDictionary<>).MakeGenericType(itemType))
                                            : type;

                                        var dictionary = dictionaryType.CreateInstance();
                                        var method = dictionaryType.GetMethod("Add", new Type[] { typeof(string), itemType });

                                        if (meta is IBdoMetaSet metaSet)
                                        {
                                            var targetItemType = Nullable.GetUnderlyingType(itemType) ?? itemType;
                                            foreach (var child in metaSet)
                                            {
                                                var childValue = child?.GetData(scope, varSet, log);
                                                var convertedChildValue = childValue != null && childValue.GetType() != targetItemType
                                                    ? Convert.ChangeType(childValue, targetItemType, CultureInfo.InvariantCulture)
                                                    : childValue;

                                                method?.Invoke(dictionary, new object[] { child?.Name, convertedChildValue });
                                            }
                                        }
                                        value = dictionary;
                                    }
                                    else if (
                                        type.IsGenericType
                                        && (typeof(IList<>).IsAssignableFrom(metaType)
                                            || typeof(List<>).IsAssignableFrom(metaType)))
                                    {
                                        itemType = type.GetGenericArguments()[0];

                                        var listType = type.IsInterface
                                            ? typeof(List<>).MakeGenericType(itemType)
                                            : type;

                                        var list = listType.CreateInstance();
                                        var method = listType.GetMethod("Add", new Type[] { itemType });

                                        if (value is IEnumerable enumerable)
                                        {
                                            var targetItemType = Nullable.GetUnderlyingType(itemType) ?? itemType;
                                            foreach (var item in enumerable)
                                            {
                                                var convertedItem = item != null && item.GetType() != targetItemType
                                                    ? Convert.ChangeType(item, targetItemType, CultureInfo.InvariantCulture)
                                                    : item;

                                                method?.Invoke(list, new object[] { convertedItem });
                                            }
                                        }
                                        value = list;
                                    }
                                    else
                                    {
                                        var targetType = Nullable.GetUnderlyingType(type) ?? type;
                                        if (targetType.IsScalar() && value.GetType() != targetType)
                                        {
                                            value = Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
                                        }
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
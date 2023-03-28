using BindOpen.Data.Helpers;
using BindOpen.Extensions;
using System;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This static class provides methods to create data elems.
    /// </summary>
    public static partial class BdoMeta
    {
        // New

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoMetaData New(
            object data)
        {
            return New(null, data);
        }

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoMetaData New(
            string name,
            object data)
        {
            if (data == null) return NewObject(name);

            var type = data.GetType();
            var meta = New(name, type, DataValueTypes.Any, data);

            return meta;
        }

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoMetaData New(
            string name,
            DataValueTypes valueType,
            object data = null)
        {
            return New(name, null, valueType, data);
        }

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoMetaData New(
            string name,
            Type type,
            object data = null)
        {
            return New(name, type, DataValueTypes.Any, data);
        }

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        private static IBdoMetaData New(
            string name,
            Type type,
            DataValueTypes valueType,
            object data = null)
        {
            type ??= data?.GetType();

            if (type != null)
            {
                valueType = type.GetValueType();
            }
            else if (valueType == DataValueTypes.Any || valueType == DataValueTypes.None)
            {
                valueType = data.GetValueType();
            }

            if (valueType.IsScalar() && type?.IsScalar() == true)
            {
                var metaScalar = NewScalar(name, valueType, data);
                return metaScalar;
            }
            else if (valueType == DataValueTypes.Scriptword)
            {
                var script = data?.ToString();
                return New(name)
                    .WithDataReference(BdoData.NewRef(script));
            }
            else if (valueType == DataValueTypes.MetaData)
            {
                Type metaType = null;
                Type itemType = null;

                if (type.IsGenericType)
                {
                    metaType = type.GetGenericTypeDefinition();
                    itemType = type.GenericTypeArguments.GetAt(0);
                }
                else
                {
                    if (typeof(IBdoMetaScalar).IsAssignableFrom(type))
                        metaType = typeof(IBdoMetaScalar);
                    else if (typeof(IBdoConfiguration).IsAssignableFrom(type))
                        metaType = typeof(IBdoConfiguration);
                    else if (typeof(IBdoMetaSet).IsAssignableFrom(type))
                        metaType = typeof(IBdoMetaSet);
                    else if (typeof(IBdoMetaObject).IsAssignableFrom(type))
                        metaType = typeof(IBdoMetaObject);
                }

                IBdoMetaData metaValue = null;

                if (metaType == typeof(IBdoMetaScalar)
                    || (metaType.Name == "IT" + nameof(BdoMetaScalar)
                    || metaType.Name == "IT" + nameof(BdoMetaData))
                    && (itemType?.IsScalar() == true))
                {
                    var scalar = data as IBdoMetaScalar;
                    metaValue = NewScalar(name, scalar?.DataValueType)
                        .WithData(scalar.GetData());
                }
                else if (metaType == typeof(IBdoConfiguration)
                    && data is IBdoConfiguration config)
                {
                    metaValue = BdoConfig.New(name)
                        .With(config.Items?.ToArray());
                }
                else if (metaType == typeof(IBdoMetaSet)
                    && data is IBdoMetaSet set)
                {
                    metaValue = NewSet(name)
                        .With(set.Items?.ToArray());
                }
                else if (metaType == typeof(IBdoMetaObject)
                    && data is IBdoMetaObject obj)
                {
                    metaValue = NewObject(name)
                        .WithData(obj.GetData());
                }
                var meta = data as BdoMetaData;

                metaValue
                    .WithSpecs(meta?.Specs?.ToArray())
                    .WithDataMode(meta?.DataMode ?? DataMode.Value)
                    .WithDataReference(meta?.Reference);

                return NewObject(name);
            }
            else if (
                typeof(IBdoExtension).IsAssignableFrom(type)
                || valueType == DataValueTypes.Connector
                || valueType == DataValueTypes.Entity
                || valueType == DataValueTypes.Task)
            {
                if (data is IBdoExtension extension)
                {
                    var config = BdoConfig.NewFrom(extension, name);
                    if (config != null)
                    {
                        config.DefinitionUniqueName = extension?.DefinitionUniqueName;
                    }
                    return config;
                }
            }
            else if (type.IsList())
            {
                var objList = data.ToObjectArray();

                var metaSet = NewSet(name);
                if (objList != null)
                {
                    foreach (var obj in objList)
                    {
                        metaSet.InsertData(obj);
                    }
                }
                return metaSet;
            }
            else
            {
                var metaObj = NewObject(name, data);
                return metaObj;
            }

            return null;
        }

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static TBdoMetaData<T> New<T>(
            string name,
            T data = default)
        {
            return New(name, typeof(T), DataValueTypes.Any, data)
                as TBdoMetaData<T>;
        }
    }
}

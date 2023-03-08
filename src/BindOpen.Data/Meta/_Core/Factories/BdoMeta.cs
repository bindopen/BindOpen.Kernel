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
            if (type == null)
            {
                type = data?.GetType();
            }
            if (type != null)
            {
                valueType = type.GetValueType();
            }
            else if (valueType == DataValueTypes.Any || valueType == DataValueTypes.None)
            {
                valueType = data.GetValueType();
            }

            if (type != null)
            {
                if (type.IsScalar())
                {
                    var metaScalar = NewScalar(name, valueType, data);
                    return metaScalar;
                }
                else
                {
                    if (valueType == DataValueTypes.MetaData
                        || valueType == DataValueTypes.Scriptword)
                    {
                        var meta = data as IBdoMetaData;
                        if (meta != null) meta.Name ??= name;
                        return meta;
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
                }
            }

            return null;
        }
    }
}

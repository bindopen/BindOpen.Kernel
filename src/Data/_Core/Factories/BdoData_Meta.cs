using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using System;

namespace BindOpen.Data
{
    /// <summary>
    /// This static class provides methods to create data elems.
    /// </summary>
    public static partial class BdoData
    {
        // New

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoMetaData NewMeta(
            object data = null)
        {
            return NewMeta(null, data);
        }

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoMetaData NewMeta(
            string name,
            object data)
        {
            if (data == null) return NewObject(name);

            var type = data.GetType();
            var meta = NewMeta(name, type, DataValueTypes.Any, data);

            return meta;
        }

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoMetaData NewMeta(
            string name,
            DataValueTypes valueType,
            object data = null)
        {
            return NewMeta(name, null, valueType, data);
        }

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoMetaData NewMeta(
            string name,
            Type type,
            object data = null)
        {
            return NewMeta(name, type, DataValueTypes.Any, data);
        }

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        private static IBdoMetaData NewMeta(
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
                var scalar = NewScalar(name, valueType, data);
                return scalar;
            }
            else if (type?.IsList() == true)
            {
                var objList = data.ToObjectArray();

                var node = NewNode(name);
                if (objList != null)
                {
                    foreach (var obj in objList)
                    {
                        node.InsertData(obj);
                    }
                }
                return node;
            }
            else
            {
                var metaObj = NewObject(name, type, data);
                return metaObj;
            }
        }
    }
}

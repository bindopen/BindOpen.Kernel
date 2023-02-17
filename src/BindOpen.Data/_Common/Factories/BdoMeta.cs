using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using System;

namespace BindOpen.Data
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
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
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
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
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
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static IBdoMetaData New(
            string name,
            object data)
        {
            if (data == null) return NewObject(name);

            var type = data.GetType();
            var meta = New(name, type, data);

            return meta;
        }

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        private static IBdoMetaData New(
            string name,
            Type type,
            DataValueTypes valueType,
            object data = null)
        {
            if (type != null)
            {
                valueType = type.GetValueType();
            }
            if (valueType == DataValueTypes.Any || valueType == DataValueTypes.None)
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
                    if (type.IsList())
                    {
                        var objList = data.ToObjectArray();

                        var metaList = NewList(name);
                        if (objList != null)
                        {
                            foreach (var obj in objList)
                            {
                                metaList.Add(obj);
                            }
                        }
                        return metaList;
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

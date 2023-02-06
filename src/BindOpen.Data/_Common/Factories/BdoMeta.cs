using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This static class provides methods to create data elems.
    /// </summary>
    public static partial class BdoMeta
    {
        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static IBdoMetaData New(
            string name = null,
            object data = null)
        {
            return New(name, DataValueTypes.Any, data);
        }

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static IBdoMetaData New(
            string name,
            DataValueTypes valueType,
            object data)
        {
            if (valueType == DataValueTypes.Any || valueType == DataValueTypes.None)
            {
                valueType = data.GetValueType();
            }

            if (valueType.IsScalar())
            {
                var metaScalar = NewScalar(name, valueType, data);
                return metaScalar;
            }
            else
            {
                if (valueType.IsList())
                {
                    var objList = data.ToObjectList();
                    var metaSet = NewList(name, objList?.ToArray());
                    return metaSet;
                }
                else
                {
                    var metaObj = NewObject(name, data);
                    return metaObj;
                }
            }
        }

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static IBdoMetaData New<T>(
            string name,
            T data)
        {
            var type = typeof(T);

            var valueType = type.GetValueType();
            var meta = New(name, valueType, data);

            return meta;
        }
    }
}

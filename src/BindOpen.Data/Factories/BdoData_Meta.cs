using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This static class provides methods to create data elems.
    /// </summary>
    public static partial class BdoData
    {
        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaData NewMeta(
            string name,
            params object[] items)
        {
            return NewMeta(name, DataValueTypes.Any, items);
        }

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaData NewMeta(
            string name,
            DataValueTypes valueType,
            params object[] items)
        {
            if (valueType == DataValueTypes.Any || valueType == DataValueTypes.None)
            {
                valueType = items.GetValueType();
            }

            if (valueType.IsScalar())
            {
                var meta = NewMetaScalar(name, valueType, items);
                return meta;
            }
            else
            {
                switch (valueType)
                {
                    case DataValueTypes.Object:
                        var meta = NewMetaObject(name, items);
                        meta?.WithItems(items);
                        meta.UpdateTree();
                        return meta;
                }
            }

            return default;
        }

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaData NewMeta<T>(
            string name,
            params object[] items)
        {
            var type = typeof(T);

            var valueType = type.GetValueType();
            var meta = NewMeta(name, valueType, items);

            if (meta.GetType().IsAssignableFrom(typeof(IBdoMetaObjectSpec)))
            {
                var objectEl = meta as IBdoMetaObjectSpec;
                objectEl.ClassFilter.AddedValues.Add(meta.GetType().ToString());
            }

            return meta;
        }
    }
}

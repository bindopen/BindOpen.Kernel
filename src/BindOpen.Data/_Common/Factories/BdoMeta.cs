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
        public static BdoMetaData New(
            string name,
            params object[] items)
        {
            return New(name, DataValueTypes.Any, items);
        }

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaData New(
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
                var meta = NewScalar(name, valueType, items);
                return meta;
            }
            else
            {
                switch (valueType)
                {
                    case DataValueTypes.Object:
                        var meta = NewObject(name, items);
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
        public static BdoMetaData New<T>(
            string name,
            params object[] items)
        {
            var type = typeof(T);

            var valueType = type.GetValueType();
            var meta = New(name, valueType, items);

            if (meta.GetType().IsAssignableFrom(typeof(IBdoMetaObjectSpec)))
            {
                var objectEl = meta as IBdoMetaObjectSpec;
                objectEl.ClassFilter.AddedValues.Add(meta.GetType().ToString());
            }

            return meta;
        }
    }
}

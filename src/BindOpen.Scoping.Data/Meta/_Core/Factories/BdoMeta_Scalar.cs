namespace BindOpen.Scoping.Data.Meta
{
    /// <summary>
    /// This static class provides methods to create data elems.
    /// </summary>
    public static partial class BdoMeta
    {
        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        public static BdoMetaScalar NewScalar(
            params object[] items)
            => NewScalar(
                (string)null,
                DataValueTypes.Any,
                items);

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param key="valueType">The value type to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static BdoMetaScalar NewScalar(
            DataValueTypes valueType,
            params object[] items)
            => NewScalar(
                (string)null,
                valueType,
                items);

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static BdoMetaScalar NewScalar(
            string name,
            params object[] items)
            => NewScalar<object, BdoMetaScalar>(
                (string)name,
                DataValueTypes.Any,
                items);

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="valueType">The value type to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static BdoMetaScalar NewScalar(
            string name,
            DataValueTypes valueType,
            params object[] items)
        {
            var meta = NewScalar<object, BdoMetaScalar>((string)name, DataValueTypes.Any, items)
                .WithDataType(valueType);

            return meta;
        }

        // T creators -------------------------

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        public static TBdoMetaScalar<TItem> NewScalar<TItem>(
            params object[] items)
            => NewScalar<TItem, TBdoMetaScalar<TItem>>(items);

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="valueType">The value type to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static TBdoMetaScalar<TItem> NewScalar<TItem>(
            string name,
            params object[] items)
            => NewScalar<TItem, TBdoMetaScalar<TItem>>(
                null,
                DataValueTypes.Any,
                items);

        // TItem, TMeta creators -------------------------

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        public static TMeta NewScalar<TItem, TMeta>(
            params object[] items)
            where TMeta : BdoMetaScalar, new()
            => NewScalar<TItem, TMeta>(
                null,
                DataValueTypes.Any,
                items);

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="valueType">The value type to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static TMeta NewScalar<TItem, TMeta>(
            string name,
            DataValueTypes valueType,
            params object[] items)
            where TMeta : BdoMetaScalar, new()
        {
            if (valueType == DataValueTypes.Any)
            {
                if (typeof(TItem) == typeof(object)
                && items?.Length > 0)
                {
                    valueType = items[0]?.GetType().GetValueType()
                        ?? DataValueTypes.Any;
                }
                else
                {
                    valueType = typeof(TItem).GetValueType();
                }
            }

            var meta = new TMeta()
                .WithName(name)
                .WithDataType(valueType);

            if (items != null)
            {
                meta.WithData(items);
            }

            return meta;
        }
    }
}

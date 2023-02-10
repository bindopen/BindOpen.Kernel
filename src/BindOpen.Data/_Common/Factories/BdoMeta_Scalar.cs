using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This static class provides methods to create data elems.
    /// </summary>
    public static partial class BdoMeta
    {
        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaScalar NewScalar(
            params object[] items)
            => NewScalar<BdoMetaScalar>(
                (string)null,
                DataValueTypes.Any,
                items);

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaScalar NewScalar(
            DataValueTypes valueType,
            params object[] items)
            => NewScalar<BdoMetaScalar>(
                (string)null,
                valueType,
                items);

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaScalar NewScalar(
            string name,
            params object[] items)
            => NewScalar<BdoMetaScalar>(
                (string)name,
                DataValueTypes.Any,
                items);

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaScalar NewScalar(
            string name,
            DataValueTypes valueType,
            params object[] items)
            => NewScalar<BdoMetaScalar>(
                (string)name,
                valueType,
                items);

        // T creators -------------------------

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaScalar NewScalar<T>(
            params object[] items)
            => NewScalar<T>(null, items);

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaScalar NewScalar<T>(
            string name,
            params object[] items)
        {
            var valueType = typeof(T).GetValueType();
            var el = NewScalar<BdoMetaScalar>(name, valueType, items);
            return el;
        }

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static T NewScalar<T>(
            string name,
            DataValueTypes valueType,
            params object[] items)
            where T : class, IBdoMetaScalar, new()
        {
            if (valueType == DataValueTypes.Any)
            {
                if (items == null)
                {
                    valueType = DataValueTypes.None;
                }
                else
                {
                    valueType = items.GetValueType();
                }
            }

            var el = new T();
            el.WithName(name);
            el.WithValueType(valueType);

            if (items != null)
            {
                el.WithData(items);
            }

            return el;
        }
    }
}

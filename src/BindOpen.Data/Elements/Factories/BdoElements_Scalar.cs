namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create data elements.
    /// </summary>
    public static partial class BdoElements
    {
        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static ScalarElement NewScalar(
            params object[] items)
            => NewScalar<ScalarElement>(
                (string)null,
                DataValueTypes.Any,
                items);

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static ScalarElement NewScalar(
            DataValueTypes valueType,
            params object[] items)
            => NewScalar<ScalarElement>(
                (string)null,
                valueType,
                items);

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static ScalarElement NewScalar(
            string name,
            params object[] items)
            => NewScalar<ScalarElement>(
                (string)name,
                DataValueTypes.Any,
                items);

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static ScalarElement NewScalar(
            string name,
            DataValueTypes valueType,
            params object[] items)
            => NewScalar<ScalarElement>(
                (string)name,
                valueType,
                items);

        // T creators -------------------------

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static ScalarElement NewScalar<T>(
            params object[] items)
            => NewScalar<T>(null, items);

        /// <summary>
        /// Creates a new instance of the ScalarElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static ScalarElement NewScalar<T>(
            string name,
            params object[] items)
        {
            var valueType = typeof(T).GetValueType();
            var el = NewScalar<ScalarElement>(name, valueType, items);
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
            where T : class, IScalarElement, new()
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
                el.WithItem(items);
            }

            return el;
        }
    }
}

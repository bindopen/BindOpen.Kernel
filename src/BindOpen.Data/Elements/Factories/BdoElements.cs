using BindOpen.Extensions.Connecting;
using BindOpen.Extensions.Modeling;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create data elements.
    /// </summary>
    public static partial class BdoElements
    {
        // Static creators -------------------------

        /// <summary>
        /// Creates a data element with specified items.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoElement NewElement(
            string name,
            params object[] items)
        {
            return NewElement(name, DataValueTypes.Any, items);
        }

        /// <summary>
        /// Creates a data element with specified items.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoElement NewElement(
            string name,
            DataValueTypes valueType,
            params object[] items)
        {
            BdoElement element = default;

            if (valueType == DataValueTypes.Any || valueType == DataValueTypes.None)
            {
                valueType = items.GetValueType();
            }

            if (valueType.IsScalar())
            {
                element = NewScalar(name, valueType, items);
            }
            else
            {
                string definitionUniqueId;
                switch (valueType)
                {
                    case DataValueTypes.Carrier:
                        definitionUniqueId = ((items.Length > 0 ? items[0] : null) as IBdoCarrierConfiguration)?.DefinitionUniqueId;
                        element = NewCarrier(name, definitionUniqueId);
                        break;
                    case DataValueTypes.Datasource:
                        definitionUniqueId = ((items.Length > 0 ? items[0] : null) as IBdoConnectorConfiguration)?.DefinitionUniqueId;
                        element = NewSource(name, definitionUniqueId);
                        break;
                    case DataValueTypes.Object:
                        element = NewObject(name, items);
                        break;
                }

                if (items != null)
                {
                    element?.WithItem(items);
                }
            }

            return element;
        }

        /// <summary>
        /// Creates a data element with specified items.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoElement NewElement<T>(
            string name,
            params object[] items)
        {
            var type = typeof(T);
            if (type == null) return null;

            var valueType = type.GetValueType();
            var el = NewElement(name, valueType, items);

            if (el.GetType().IsAssignableFrom(typeof(IObjectElementSpec)))
            {
                var objectEl = el as IObjectElementSpec;
                objectEl.ClassFilter.AddedValues.Add(el.GetType().ToString());
            }

            return el;
        }
    }
}

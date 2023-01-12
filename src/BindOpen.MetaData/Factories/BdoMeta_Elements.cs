using BindOpen.Extensions.Connecting;
using BindOpen.Extensions.Modeling;
using BindOpen.Meta.Elements;

namespace BindOpen.Meta
{
    /// <summary>
    /// This static class provides methods to create data elements.
    /// </summary>
    public static partial class BdoMeta
    {
        // Static creators -------------------------

        /// <summary>
        /// Creates a data element with specified items.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoMetaElement NewElement(
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
        public static BdoMetaElement NewElement(
            string name,
            DataValueTypes valueType,
            params object[] items)
        {
            BdoMetaElement element = default;

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
        public static BdoMetaElement NewElement<T>(
            string name,
            params object[] items)
        {
            var type = typeof(T);

            var valueType = type.GetValueType();
            var el = NewElement(name, valueType, items);

            if (el.GetType().IsAssignableFrom(typeof(IBdoMetaObjectSpec)))
            {
                var objectEl = el as IBdoMetaObjectSpec;
                objectEl.ClassFilter.AddedValues.Add(el.GetType().ToString());
            }

            return el;
        }
    }
}

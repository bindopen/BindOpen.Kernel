using BindOpen.Data.Common;
using BindOpen.Extensions.Runtime;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create data elements.
    /// </summary>
    public static partial class ElementFactory
    {
        // Static creators -------------------------

        /// <summary>
        /// Creates a data element with specified items.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static DataElement Create(string name, params object[] items)
        {
            return Create(name, DataValueTypes.Any, items);
        }

        /// <summary>
        /// Creates a data element with specified items.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static DataElement Create(string name, DataValueTypes valueType, params object[] items)
        {
            DataElement element = null;

            if (valueType == DataValueTypes.Any || valueType == DataValueTypes.None)
            {
                valueType = items.GetValueType();
            }

            if (valueType.IsScalar())
            {
                element = CreateScalar(name, valueType, items);
            }
            else
            {
                string definitionUniqueId;
                switch (valueType)
                {
                    case DataValueTypes.Carrier:
                        definitionUniqueId = ((items.Length > 0 ? items[0] : null) as IBdoCarrierConfiguration)?.DefinitionUniqueId;
                        element = CreateCarrier(name, null, definitionUniqueId);
                        break;
                    case DataValueTypes.Datasource:
                        definitionUniqueId = ((items.Length > 0 ? items[0] : null) as IBdoConnectorConfiguration)?.DefinitionUniqueId;
                        element = CreateSource(name, null, definitionUniqueId);
                        break;
                    case DataValueTypes.Document:
                        element = CreateDocument(name, null);
                        break;
                    case DataValueTypes.Object:
                        element = CreateObject(name, items);
                        break;
                }

                if (items != null)
                {
                    element?.WithItems(items);
                }
            }

            return element;
        }
    }
}

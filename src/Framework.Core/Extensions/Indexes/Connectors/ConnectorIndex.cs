using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Indexes.Connectors
{
    /// <summary>
    /// This class represents a connector index.
    /// </summary>
    [Serializable()]
    [XmlType("ConnectorIndex", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "connectors.index", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ConnectorIndex : TAppExtensionItemIndex<IConnectorDefinition>
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConnectorIndex class.
        /// </summary>
        public ConnectorIndex() : base()
        {
        }

        #endregion
    }
}

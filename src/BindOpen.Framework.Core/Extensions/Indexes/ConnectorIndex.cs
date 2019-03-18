using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Indexes
{
    /// <summary>
    /// This class represents a connector index.
    /// </summary>
    [Serializable()]
    [XmlType("ConnectorIndex", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "connectors.index", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ConnectorIndex : TAppExtensionItemIndex<ConnectorDefinition>
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

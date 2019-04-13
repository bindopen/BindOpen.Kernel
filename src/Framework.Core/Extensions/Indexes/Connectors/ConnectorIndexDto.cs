using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Definition.Connectors;

namespace BindOpen.Framework.Core.Extensions.Indexes.Connectors
{
    /// <summary>
    /// This class represents a DTO connector index.
    /// </summary>
    [Serializable()]
    [XmlType("ConnectorIndex", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "connectors.index", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ConnectorIndexDto : TAppExtensionItemIndexDto<IConnectorDefinitionDto>
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConnectorIndex class.
        /// </summary>
        public ConnectorIndexDto() : base()
        {
        }

        #endregion
    }
}

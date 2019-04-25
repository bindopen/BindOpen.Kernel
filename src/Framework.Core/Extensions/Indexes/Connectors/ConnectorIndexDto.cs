using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Definitions.Connectors;

namespace BindOpen.Framework.Core.Extensions.Indexes.Connectors
{
    /// <summary>
    /// This class represents a DTO connector index.
    /// </summary>
    [Serializable()]
    [XmlType("ConnectorIndex", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "connectors.index", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class ConnectorIndexDto : TAppExtensionItemIndexDto<ConnectorDefinitionDto>
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

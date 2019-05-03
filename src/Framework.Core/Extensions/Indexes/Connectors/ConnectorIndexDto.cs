using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Items.Connectors;
using BindOpen.Framework.Core.Extensions.Items.Connectors.Definition;
using BindOpen.Framework.Core.Extensions.Items.Connectors.Definition.Dto;

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

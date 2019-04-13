using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Source;

namespace BindOpen.Framework.Core.Extensions.Definition.Connectors
{

    /// <summary>
    /// This class represents a DTO connector definition.
    /// </summary>
    [Serializable()]
    [XmlType("ConnectorDefinition", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "connector.definition", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ConnectorDefinitionDto : AppExtensionItemDefinitionDto, IConnectorDefinitionDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Item class of this instance.
        /// </summary>
        [XmlElement("itemClass")]
        public string ItemClass
        {
            get;
            set;
        }

        /// <summary>
        /// Data source kind of this instance.
        /// </summary>
        [XmlElement("dataSourceKind")]
        public DataSourceKind DataSourceKind { get; set; } = DataSourceKind.None;

        /// <summary>
        /// The data source detail specification of this instance.
        /// </summary>
        [XmlElement("dataSource.specification")]
        public DataElementSpecSet DatasourceDetailSpec { get; set; } = new DataElementSpecSet();

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConnectorDefinition class.
        /// </summary>
        public ConnectorDefinitionDto()
        {
        }

        #endregion
    }

}

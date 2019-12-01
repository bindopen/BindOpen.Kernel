using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Source;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Definition.Items
{
    /// <summary>
    /// This class represents a DTO connector definition.
    /// </summary>
    [Serializable()]
    [XmlType("BdoConnectorDefinition", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "connector.definition", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoConnectorDefinitionDto : BdoExtensionItemDefinitionDto, IBdoConnectorDefinitionDto
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
        public DatasourceKind DatasourceKind { get; set; } = DatasourceKind.None;

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
        /// Instantiates a new instance of the ConnectorDefinitionDto class.
        /// </summary>
        public BdoConnectorDefinitionDto()
        {
        }

        #endregion
    }

}

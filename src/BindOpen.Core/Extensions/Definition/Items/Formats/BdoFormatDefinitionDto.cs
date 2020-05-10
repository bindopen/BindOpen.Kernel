using BindOpen.Data.Items;
using System;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// This class represents the format definition.
    /// </summary>
    [XmlType("BdoFormatDefinition", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "format.definition", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoFormatDefinitionDto : BdoExtensionItemDefinitionDto, IBdoFormatDefinitionDto
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
        /// Viewer class of this instance.
        /// </summary>
        /// <remarks>Class using the following format: winForm=xxx.xxx.xxx;webForm=xxx.xxx.xxx</remarks>
        [XmlElement("viewerClass")]
        public string ViewerClass
        {
            get;
            set;
        }

        /// <summary>
        /// Data source kind of this instance.
        /// </summary>
        [XmlElement("dataSourceKind")]
        public DatasourceKind DatasourceKind { get; set; } = DatasourceKind.Memory;

        /// <summary>
        /// The runtime type of this instance.
        /// </summary>
        public Type RuntimeType { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the FormatDefinition class.
        /// </summary>
        public BdoFormatDefinitionDto()
        {
        }

        #endregion
    }
}

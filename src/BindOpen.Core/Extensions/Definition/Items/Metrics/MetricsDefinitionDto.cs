using System;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// This class represents a metrics definition.
    /// </summary>
    [XmlType("MetricsDefinition", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "metrics.definition", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoMetricsDefinitionDto : BdoExtensionItemDefinitionDto, IBdoBdoMetricsDefinitionDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

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
        /// Unit code of this instance.
        /// </summary>
        [XmlElement("unitCode")]
        public string UnitCode
        {
            get;
            set;
        }

        /// <summary>
        /// The runtime type of this instance.
        /// </summary>
        public Type RuntimeType { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the MetricsDefinition class. 
        /// </summary>
        public BdoMetricsDefinitionDto()
        {
        }

        #endregion
    }
}
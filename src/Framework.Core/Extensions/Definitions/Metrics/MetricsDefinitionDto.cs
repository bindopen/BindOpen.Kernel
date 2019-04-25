using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Definitions.Metrics
{
    /// <summary>
    /// This class represents a metrics definition.
    /// </summary>
    [Serializable()]
    [XmlType("MetricsDefinition", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "metrics.definition", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class MetricsDefinitionDto : AppExtensionItemDefinitionDto, IMetricsDefinitionDto
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
        public MetricsDefinitionDto()
        {
        }

        #endregion
    }
}
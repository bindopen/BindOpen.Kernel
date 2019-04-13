using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Definition.Metrics
{
    /// <summary>
    /// This class represents a metrics definition.
    /// </summary>
    [Serializable()]
    [XmlType("MetricsDefinition", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "metrics.definition", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
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
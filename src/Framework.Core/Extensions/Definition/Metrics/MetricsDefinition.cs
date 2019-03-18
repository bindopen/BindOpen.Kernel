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
    public class MetricsDefinition : AppExtensionItemDefinition
    {

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Item class of this instance.
        /// </summary>
        [XmlElement("itemClass")]
        public String ItemClass
        {
            get;
            set;
        }

        /// <summary>
        /// Unit code of this instance.
        /// </summary>
        [XmlElement("unitCode")]
        public String UnitCode
        {
            get;
            set;
        }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the MetricsDefinition class. 
        /// </summary>
        public MetricsDefinition()
        {
        }

        #endregion

    }
}
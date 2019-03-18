using BindOpen.Framework.Core.Extensions.Definition.Metrics;
using BindOpen.Framework.Core.Extensions.Configuration.Metrics;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Indexes
{
    /// <summary>
    /// This class represents a metrics index.
    /// </summary>
    [Serializable()]
    [XmlType("MetricsIndex", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "metrics.index", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class MetricsIndex : TAppExtensionItemIndex<MetricsDefinition>
    {

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the MetricsIndex class.
        /// </summary>
        public MetricsIndex()
        {
        }

        #endregion

    }
}

using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Definition.Metrics;

namespace BindOpen.Framework.Core.Extensions.Indexes.Metrics
{
    /// <summary>
    /// This class represents a metrics index.
    /// </summary>
    [Serializable()]
    [XmlType("MetricsIndex", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "metrics.index", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class MetricsIndex : TAppExtensionItemIndex<IMetricsDefinition>
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

using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Items.Metrics;
using BindOpen.Framework.Core.Extensions.Items.Metrics.Definition;

namespace BindOpen.Framework.Core.Extensions.Indexes.Metrics
{
    /// <summary>
    /// This class represents a metrics index.
    /// </summary>
    [Serializable()]
    [XmlType("MetricsIndex", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "metrics.index", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class MetricsIndexDto : TAppExtensionItemIndexDto<MetricsDefinitionDto>
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the MetricsIndex class.
        /// </summary>
        public MetricsIndexDto()
        {
        }

        #endregion
    }
}

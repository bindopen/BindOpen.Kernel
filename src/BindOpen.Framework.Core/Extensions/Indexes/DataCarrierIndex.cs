using BindOpen.Framework.Core.Extensions.Definition.Carriers;
using BindOpen.Framework.Core.Extensions.Configuration.Carriers;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Indexes
{
    /// <summary>
    /// This class represents a carrier index.
    /// </summary>
    [Serializable()]
    [XmlType("CarrierIndex", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "carriers.index", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class CarrierConfigurationIndex : TAppExtensionItemIndex<CarrierDefinition>
    {

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the CarrierConfigurationIndex class.
        /// </summary>
        public CarrierConfigurationIndex()
        {
        }

        #endregion


    }
}

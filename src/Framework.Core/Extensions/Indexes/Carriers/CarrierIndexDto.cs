using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Definition.Carriers;

namespace BindOpen.Framework.Core.Extensions.Indexes.Carriers
{
    /// <summary>
    /// This class represents a carrier index.
    /// </summary>
    [Serializable()]
    [XmlType("CarrierIndex", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "carriers.index", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class CarrierIndexDto : TAppExtensionItemIndexDto<ICarrierDefinitionDto>
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the CarrierIndex class.
        /// </summary>
        public CarrierIndexDto()
        {
        }

        #endregion
    }
}

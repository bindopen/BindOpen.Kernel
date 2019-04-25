using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Definitions.Carriers;

namespace BindOpen.Framework.Core.Extensions.Indexes.Carriers
{
    /// <summary>
    /// This class represents a carrier index.
    /// </summary>
    [Serializable()]
    [XmlType("CarrierIndex", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "carriers.index", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class CarrierIndexDto : TAppExtensionItemIndexDto<CarrierDefinitionDto>
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

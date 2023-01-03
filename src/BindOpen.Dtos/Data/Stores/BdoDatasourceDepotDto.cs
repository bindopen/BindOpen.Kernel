using BindOpen.Data.Items;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This class represents a datasource depot.
    /// </summary>
    [XmlType("DatasourceDepot", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "datasourceDepot", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoDatasourceDepotDto : IDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The sources of this instance.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The sources of this instance.
        /// </summary>
        public List<DatasourceDto> Sources { get; set; }

        #endregion


        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DatasourceDepotDto class.
        /// </summary>
        public BdoDatasourceDepotDto()
        {
        }

        #endregion
    }
}


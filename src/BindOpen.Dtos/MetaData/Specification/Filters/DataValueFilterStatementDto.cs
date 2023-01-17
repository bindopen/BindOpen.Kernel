using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.MetaData.Specification
{
    /// <summary>
    /// This interface specifies the value filter statement.
    /// </summary>
    [XmlType("DataValueFilterStatement", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "value.filter.statement", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class DataValueFilterStatementDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Filters of this instance.
        /// </summary>
        [JsonPropertyName("filters")]
        [XmlArray("filters")]
        [XmlArrayItem("add.filter")]
        public List<DataValueFilterDto> Filters { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataValueFilterStatementDto class.
        /// </summary>
        public DataValueFilterStatementDto()
        {
        }

        #endregion
    }

}

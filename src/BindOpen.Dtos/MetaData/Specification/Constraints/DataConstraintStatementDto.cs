using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.MetaData.Specification
{
    /// <summary>
    /// This class represents the data constraint statement.
    /// </summary>
    [XmlType("DataConstraintStatement", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "constraintStatement", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class DataConstraintStatementDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The added values of this instance.
        /// </summary>
        /// <remarks>If empty then all the values are added.</remarks>
        //[XmlElement("add")]
        //public List<BdoRoutineConfigurationDto> Constraints { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataConstraintStatementDto class.
        /// </summary>
        public DataConstraintStatementDto()
        {
        }

        #endregion
    }
}

using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Meta.Specification
{
    /// <summary>
    /// This class represents the data constraint statement.
    /// </summary>
    [XmlType("DataConstraintStatement", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "constraintStatement", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
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

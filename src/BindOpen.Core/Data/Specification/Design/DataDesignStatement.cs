using BindOpen.Data.Items;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BindOpen.Data.Specification
{
    /// <summary>
    /// This class represents a data design statement.
    /// </summary>
    [XmlType("DataDesignStatement", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "design.statement", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class DataDesignStatement : DataItem, IDataDesignStatement
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Control with of this instance.
        /// </summary>
        [XmlElement("controlWidth")]
        [DefaultValue("")]
        public string ControlWidth { get; set; } = "";

        /// <summary>
        /// Control type of this instance.
        /// </summary>
        [XmlElement("controlType")]
        [DefaultValue(DesignControlType.Auto)]
        public DesignControlType ControlType { get; set; } = DesignControlType.Auto;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DataDesignStatement class.
        /// </summary>
        public DataDesignStatement()
        {
        }

        #endregion
    }

}

using System;
using System.ComponentModel;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items.Strings;

namespace BindOpen.Framework.Core.Data.Specification.Design
{
    /// <summary>
    /// This class represents a data element design statement.
    /// </summary>
    [Serializable()]
    [XmlType("DataElementDesignStatement", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "design.statement", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class DataElementDesignStatement : StringValuedDataItem
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Control with of this instance.
        /// </summary>
        [XmlElement("controlWidth")]
        public String ControlWidth { get; set; } = "";

        /// <summary>
        /// Specification of the ControlWidth property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ControlWidthSpecified => !string.IsNullOrEmpty(ControlWidth);

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
        /// Initializes a new instance of the DataElementDesignStatement class.
        /// </summary>
        public DataElementDesignStatement()
        {
        }

        #endregion
    }

}

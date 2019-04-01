using System;
using System.ComponentModel;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.Specification.Design
{
    /// <summary>
    /// This class represents a data design statement.
    /// </summary>
    [Serializable()]
    [XmlType("DataDesignStatement", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "design.statement", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
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
        public string ControlWidth { get; set; } = "";

        /// <summary>
        /// Specification of the ControlWidth property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ControlWidthSpecified => !string.IsNullOrEmpty(ControlWidth);

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

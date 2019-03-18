using BindOpen.Framework.Core.Data.Items.Strings;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

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
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private DesignControlType _ControlType= DesignControlType.Auto;
        private String _ControlWidth = "";

        #endregion


        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Control with of this instance.
        /// </summary>
        [XmlElement("controlWidth")]
        public String ControlWidth
        {
            get
            {
                return this._ControlWidth;
            }
            set { this._ControlWidth = value; }
        }

        /// <summary>
        /// Specification of the ControlWidth property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ControlWidthSpecified
        {
            get
            {
                return !String.IsNullOrEmpty(this._ControlWidth);
            }
        }

        /// <summary>
        /// Control type of this instance.
        /// </summary>
        [XmlElement("controlType")]
        [DefaultValue(DesignControlType.Auto)]
        public DesignControlType ControlType
        {
            get
            {
                return this._ControlType;
            }
            set { this._ControlType = value; }
        }

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

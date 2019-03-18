using BindOpen.Framework.Core.Data.Items;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Specification.Filters
{

    /// <summary>
    /// This interface specifies the value filter statement.
    /// </summary>
    [Serializable()]
    [XmlType("DataValueFilterStatement", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "value.filter.statement", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class DataValueFilterStatement : DataItem
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private List<DataValueFilter> _Specifications = new List<DataValueFilter>();

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Specifications of this instance.
        /// </summary>
        [XmlArray("valueSpecifications")]
        [XmlArrayItem("add.specification")]
        public List<DataValueFilter> Specifications
        {
            get { return this._Specifications; }
            set { this._Specifications = value; }
        }

        #endregion


        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataValueFilterStatement class.
        /// </summary>
        public DataValueFilterStatement()
        {
        }

        #endregion

    }

}

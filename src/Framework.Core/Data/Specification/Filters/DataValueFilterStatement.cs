using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Specification;

namespace BindOpen.Framework.Core.Data.Specification
{
    /// <summary>
    /// This interface specifies the value filter statement.
    /// </summary>
    [Serializable()]
    [XmlType("DataValueFilterStatement", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "value.filter.statement", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class DataValueFilterStatement : DataItem, IDataValueFilterStatement
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Specifications of this instance.
        /// </summary>
        [XmlArray("valueSpecifications")]
        [XmlArrayItem("add.specification")]
        public List<DataValueFilter> Specifications { get; set; } = new List<DataValueFilter>();

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

using BindOpen.Data.Items;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Data.Specification
{
    /// <summary>
    /// This interface specifies the value filter statement.
    /// </summary>
    [XmlType("DataValueFilterStatement", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "value.filter.statement", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
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

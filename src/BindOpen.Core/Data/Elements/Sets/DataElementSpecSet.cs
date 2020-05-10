using BindOpen.Data.Items;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a set of data element specifications.
    /// </summary>
    [XmlType("DataElementSpecSet", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "dataElementSpecSet", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class DataElementSpecSet : TDataItemSet<DataElementSpec>, IDataElementSpecSet
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The DTO items of this instance.
        /// </summary>
        [XmlElement("carrier", typeof(CarrierElementSpec))]
        [XmlElement("document", typeof(DocumentElementSpec))]
        [XmlElement("object", typeof(ObjectElementSpec))]
        [XmlElement("scalar", typeof(ScalarElementSpec))]
        [XmlElement("source", typeof(SourceElementSpec))]
        [XmlElement("specification")]
        public List<DataElementSpec> DtoItems
        {
            get { return Items; }
            set { Items = value; }
        }

        // Conversions -----------------------------

        /// <summary>
        /// Converts from data element specification array.
        /// </summary>
        /// <param name="elements">The elements to consider.</param>
        public static implicit operator DataElementSpecSet(DataElementSpec[] elements)
        {
            return ElementSpecFactory.CreateSet(elements);
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DataElementSpecSet class.
        /// </summary>
        public DataElementSpecSet() : base()
        {
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            return base.Clone();
        }

        #endregion
    }

}

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Document;
using BindOpen.Framework.Core.Data.Elements.Entity;
using BindOpen.Framework.Core.Data.Elements.Scalar;
using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Data.Items.Sets;

namespace BindOpen.Framework.Core.Data.Elements.Sets
{
    /// <summary>
    /// This class represents a set of data element specifications.
    /// </summary>
    [Serializable()]
    [XmlType("DataElementSpecSet", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "dataElementSpecSet", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class DataElementSpecSet : GenericDataItemSet<IDataElementSpec>, IDataElementSpecSet
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Specifications of this instance.
        /// </summary>
        [XmlElement("carrier", typeof(CarrierElementSpec))]
        [XmlElement("document", typeof(DocumentElementSpec))]
        [XmlElement("entity", typeof(EntityElementSpec))]
        [XmlElement("scalar", typeof(ScalarElementSpec))]
        [XmlElement("source", typeof(SourceElementSpec))]
        [XmlElement("specification")]
        public List<IDataElementSpec> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        /// <summary>
        /// Specification of the Specifications property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool SpecificationsSpecified
        {
            get
            {
                return _items?.Count > 0;
            }
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new set of data specifications.
        /// </summary>
        public DataElementSpecSet()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DataElementSpecSet class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public DataElementSpecSet(params IDataElementSpec[] items) : base(items)
        {
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the item with the specified name.
        /// </summary>
        /// <param name="name">The name of the item to return.</param>
        /// <returns>Returns the item with the specified name.</returns>
        public new IDataElementSpec GetItem(String name)
        {
            return GetItem(name) as IDataElementSpec;
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
        public override object Clone()
        {
            return base.Clone();
        }

        #endregion
    }

}

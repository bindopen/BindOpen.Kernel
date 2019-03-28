using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Document;
using BindOpen.Framework.Core.Data.Elements.Entity;
using BindOpen.Framework.Core.Data.Elements.Scalar;
using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Data.Items.Sets;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Elements.Sets
{

    /// <summary>
    /// This class represents a set of data element specifications.
    /// </summary>
    [Serializable()]
    [XmlType("DataElementSpecSet", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "dataElementSpecSet", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class DataElementSpecSet : GenericDataItemSet<DataElementSpec>
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
        public List<DataElementSpec> Items
        {
            get { return this._Items; }
            set { this._Items = value; }
        }

        /// <summary>
        /// Specification of the Specifications property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean SpecificationsSpecified
        {
            get
            {
                return this._Items != null && this._Items.Count > 0;
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
        public DataElementSpecSet(params DataElementSpec[] items) : base(items)
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
        public new DataElementSpec GetItem(String name)
        {
            return this.GetItem(name) as DataElementSpec;
        }

        /// <summary>
        /// Creates a new element set from this instance.
        /// </summary>
        /// <returns>Returns the element set.</returns>
        public DataElementSet NewElementSet()
        {
            DataElementSet dataElementSet = new DataElementSet();
            foreach (DataElementSpec dataElementSpec in this._Items)
                dataElementSet.Add(dataElementSpec.NewElement());

            return dataElementSet;
        }

        #endregion


        // --------------------------------------------------
        // UPDATE, CHECK, REPAIR
        // --------------------------------------------------

        #region Update_Check_Repair

        #endregion


        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override Object Clone()
        {
            return base.Clone();
        }

        #endregion

    }

}

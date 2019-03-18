using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items.Dictionary;

namespace BindOpen.Framework.Core.Data.Items.Sets
{
    /// <summary>
    /// This class represents a data item set.
    /// </summary>
    /// <typeparam name="T">The class of the named data items.</typeparam>
    [Serializable()]
    [XmlRoot(ElementName = "item.set", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class DataItemSet<T> : GenericDataItemSet<T>
        where T : StoredDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Items of this instance.
        /// </summary>
        [XmlElement("item")]
        public List<T> Items
        {
            get
            {
                return this._Items ?? (this._Items = new List<T>());
            }
            set
            {
                this._Items = value;
                this.OnPropertyChanged("Items");
            }
        }

        /// <summary>
        /// Specification of the Items property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ItemsSpecified
        {
            get
            {
                return _Items != null && this._Items.Count > 0;
            }
        }

        #endregion
    
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataItemSet class.
        /// </summary>
        public DataItemSet() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DataItemSet class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        /// <param name="description">The description to consider.</param>
        public DataItemSet(DictionaryDataItem description, params T[] items) : base(description, items)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DataItemSet class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public DataItemSet(params T[] items) : base(items)
        {
        }

        #endregion

    }
}

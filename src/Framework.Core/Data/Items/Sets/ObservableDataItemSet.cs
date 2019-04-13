using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Data.Items.Sets;

namespace BindOpen.Framework.Core.Data.Items.Sets
{
    /// <summary>
    /// This class represents a data item set.
    /// </summary>
    /// <typeparam name="T">The class of the named data items.</typeparam>
    [Serializable()]
    [XmlRoot(ElementName = "item.set", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ObservableDataItemSet<T> : DataItemSet<T>, IDataItemSet<T>
        where T : IdentifiedDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Items of this instance.
        /// </summary>
        [XmlElement("item")]
        public new List<T> Items
        {
            get => base.Items;
            set
            {
                base.Items = value;
                OnPropertyChanged("Items");
            }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ObservableDataItemSet class.
        /// </summary>
        public ObservableDataItemSet() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ObservableDataItemSet class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        /// <param name="description">The description to consider.</param>
        public ObservableDataItemSet(
            IDictionaryDataItem description,
            params T[] items) : base(description, items)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ObservableDataItemSet class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public ObservableDataItemSet(params T[] items) : base(items)
        {
        }

        #endregion
    }
}

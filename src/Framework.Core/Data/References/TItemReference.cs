using BindOpen.Framework.Core.Data.Items;
using System;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.References
{

    /// <summary>
    /// This class represents an item reference.
    /// </summary>
    public class TItemReference<T> : TBaseItemReference<T> where T: DataItem
    {

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The item of this instance.
        /// </summary>
        [XmlElement("item")]
        public T ObjectItem
        {
            get
            {
                return this.Item;
            }
            set
            {
                this.Item = value;
            }
        }

        /// <summary>
        /// Specification of the ObjectItem property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ObjectItemSpecified
        {
            get
            {
                return !this.ReferenceSpecified && this.Item !=null;
            }
        }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Creates a new instance of the TItemReference class.
        /// </summary>
        public TItemReference()
        {
        }

        /// <summary>
        /// Creates a new instance of the TItemReference class.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        public TItemReference(T item)
        {
            this.Item = item;
        }

        /// <summary>
        /// Creates a new instance of the TItemReference class.
        /// </summary>
        /// <param name="reference">The reference to consider.</param>
        public TItemReference(DataReference reference)
        {
            this.Reference = reference;
        }

        #endregion

    }

}

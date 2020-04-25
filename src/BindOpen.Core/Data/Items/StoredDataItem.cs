using BindOpen.Data.Common;
using BindOpen.Data.Helpers.Objects;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a stored data item.
    /// </summary>
    [XmlType("StoredDataItem", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("storedDataItem", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class StoredDataItem : IdentifiedDataItem, IStoredDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Name preffix of this instance.
        /// </summary>
        [XmlIgnore()]
        protected string NamePreffix { get; set; } = "Object_";

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        [XmlElement("creationDate")]
        [DefaultValue("")]
        public string CreationDate { get; set; } = null;

        /// <summary>
        /// Last modification date of this instance.
        /// </summary>
        [XmlElement("lastModificationDate")]
        [DefaultValue("")]
        public string LastModificationDate { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the StoredDataItem class.
        /// </summary>
        public StoredDataItem() : this("")
        {
        }

        /// <summary>
        /// Instantiates a new instance of the StoredDataItem class considering the specified name and preffix name.
        /// </summary>
        /// <param name="id">The ID to consider.</param>
        /// <param name="creationDate">The creation date of this instance.</param>
        public StoredDataItem(
            string id = null,
            DateTime? creationDate = null) : base(id)
        {
            CreationDate = creationDate?.ToString();
        }

        #endregion

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        /// <summary>
        /// Declares this instance has changed.
        /// </summary>
        public virtual void DeclareUpdate()
        {
            // we update the modification date
            LastModificationDate = DateTime.Now.ToString(DataValueType.Date);
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            StoredDataItem item = base.Clone(areas) as StoredDataItem;
            if (CreationDate != null)
                item.CreationDate = DateTime.Now.ToString(DataValueType.Date);
            item.LastModificationDate = null;
            return item;
        }

        #endregion       
    }
}

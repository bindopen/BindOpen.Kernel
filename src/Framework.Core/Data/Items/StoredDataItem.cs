using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Objects;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This class represents a stored data item.
    /// </summary>
    [Serializable()]
    [XmlType("StoredDataItem", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot("storedDataItem", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
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
        protected String NamePreffix { get; set; } = "Object_";

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        [XmlElement("creationDate")]
        public string CreationDate { get; set; } = null;

        /// <summary>
        /// Specification of the CreationDate property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool CreationDateSpecified => !string.IsNullOrEmpty(CreationDate);

        /// <summary>
        /// Last modification date of this instance.
        /// </summary>
        [XmlElement("lastModificationDate")]
        public string LastModificationDate { get; set; } = null;

        /// <summary>
        /// Specification of the LastModificationDate property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool LastModificationDateSpecified => !string.IsNullOrEmpty(LastModificationDate);

        /// <summary>
        /// Indicates whether this instance is locked.
        /// </summary>
        [XmlElement("isLocked")]
        public bool IsLocked { get; set; } = false;

        /// <summary>
        /// Specification of the IsLocked property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool IsLockedSpecified => IsLocked;

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
            String id = null,
            DateTime? creationDate = null) : base(id)
        {
            this.CreationDate = creationDate?.GetString();
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
            this.LastModificationDate = DateTime.Now.GetString();
        }

        /// <summary>
        /// Locks this instance.
        /// </summary>
        /// <param name="isRecursive">Indicates whether the protection is applied to sub objects.</param>
        public virtual void Lock(bool isRecursive = true)
        {
            this.IsLocked = true;
        }

        /// <summary>
        /// Unlocks this instance.
        /// </summary>
        /// <param name="isRecursive">Indicates whether the protection is applied to sub objects.</param>
        public virtual void Unlock(bool isRecursive = true)
        {
            this.IsLocked = false;
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
        public override object Clone()
        {
            StoredDataItem item = base.Clone() as StoredDataItem;
            if (this.CreationDate != null)
                item.CreationDate = DateTime.Now.GetString();
            item.LastModificationDate = null;
            return item;
        }

        #endregion       
    }
}

using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This class represents an identified data item.
    /// </summary>
    [XmlType("IdentifiedDataItem", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("identifiedDataItem", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class IdentifiedDataItem : DataItem, IIdentifiedDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// ID of this instance.
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; } = null;

        /// <summary>
        /// Specification of the ID of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool IdSpecified => !string.IsNullOrEmpty(this.Id);

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the IdentifiedDataItem class.
        /// </summary>
        /// <param name="id">The ID to consider.</param>
        public IdentifiedDataItem(string id = null) : base()
        {
            this.Id = id?.Length == 0 ? IdentifiedDataItem.NewGuid() : id;
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the identifier key.
        /// </summary>
        public virtual string Key()
        {
            return this.Id;
        }

        /// <summary>
        /// Returns a new Guid.
        /// </summary>
        public static string NewGuid()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone()
        {
            IdentifiedDataItem item = base.Clone() as IdentifiedDataItem;
            if (this.Id != null)
                item.Id = IdentifiedDataItem.NewGuid();
            return item;
        }

        #endregion       
    }
}

using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents an identified data item.
    /// </summary>
    [XmlType("IdentifiedDataItem", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen")]
    [XmlRoot("identifiedDataItem", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen", IsNullable = false)]
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
        [DefaultValue("")]
        public string Id { get; set; } = null;

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
            Id = id?.Length == 0 ? IdentifiedDataItem.NewGuid() : id;
        }

        #endregion

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public IIdentifiedDataItem WithId(string id)
        {
            Id = id;

            return this;
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
            return Id;
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
        /// <param name="areas">The areas to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            IdentifiedDataItem item = base.Clone(areas) as IdentifiedDataItem;
            if (Id != null)
                item.Id = IdentifiedDataItem.NewGuid();
            return item;
        }

        #endregion
    }
}

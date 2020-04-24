using BindOpen.Data.Helpers.Objects;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a named data item.
    /// </summary>
    [XmlType("NamedDataItem", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("namedDataItem", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class NamedDataItem : StoredDataItem, INamedDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Name of this instance.
        /// </summary>
        [XmlAttribute("name")]
        [DefaultValue("")]
        public string Name { get; set; } = "";

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the NamedDataItem class.
        /// </summary>
        public NamedDataItem() : this("")
        {
        }

        /// <summary>
        /// Instantiates a new instance of the NamedDataItem class considering the specified name and preffix name.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        /// <param name="namePreffix">The preffix of the name of this instance.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="creationDate">The creation date of this instance.</param>
        public NamedDataItem(
            string name,
            string namePreffix = "",
            string id = null,
            DateTime? creationDate = null) : base(id, creationDate)
        {
            NamePreffix = namePreffix;
            Name = (name ?? NamePreffix + DateTime.Now.Ticks.ToString());
        }

        #endregion

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        public INamedDataItem WithName(string name)
        {
            Name = name;
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
        public override string Key()
        {
            return Name;
        }

        /// <summary>
        /// Returns a new Guid.
        /// </summary>
        public static string GetClonedName(string name, string namePreffix = null)
        {
            return string.IsNullOrEmpty(name) ?
                (namePreffix != null ? namePreffix + "_" : "") + DateTime.Now.Ticks.ToString() :
                "copy_" + name;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            NamedDataItem item = base.Clone(areas) as NamedDataItem;
            item.Name = Name;// NamedDataItem.GetClonedName(Name, NamePreffix);
            if (CreationDate != null)
                item.CreationDate = ObjectHelper.ToString(DateTime.Now);
            item.LastModificationDate = null;
            return item;
        }

        #endregion
    }
}

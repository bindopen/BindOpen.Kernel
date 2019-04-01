using System;
using System.ComponentModel;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Objects;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This class represents a named data item.
    /// </summary>
    [Serializable()]
    [XmlType("NamedDataItem", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot("namedDataItem", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
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

        /// <summary>
        /// Specification of the Name property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool NameSpecified => !string.IsNullOrEmpty(Name);

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
            this.NamePreffix = namePreffix;
            this.Name = (name ?? this.NamePreffix + DateTime.Now.Ticks.ToString());
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
            return this.Name;
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
        public override object Clone()
        {
            NamedDataItem item = base.Clone() as NamedDataItem;
            item.Name =NamedDataItem.GetClonedName(this.Name, this.NamePreffix);
            if (this.CreationDate != null)
                item.CreationDate = DateTime.Now.GetString();
            item.LastModificationDate = null;
            return item;
        }

        #endregion       
    }
}

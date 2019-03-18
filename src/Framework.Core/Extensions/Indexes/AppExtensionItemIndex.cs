using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Definition;

namespace BindOpen.Framework.Core.Extensions.Indexes
{
    /// <summary>
    /// This class represents an application extension item index.
    /// </summary>
    /// <typeparam name="T">The class of this extension item definition.</typeparam>
    [Serializable()]
    [XmlType("TAppExtensionItemIndex", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "index", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class TAppExtensionItemIndex<T> : StoredDataItem
        where T : AppExtensionItemDefinition
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private List<AppExtensionItemGroup> _groups;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// ID of the library of this instance.
        /// </summary>
        [XmlAttribute("libraryId")]
        public String LibraryId { get; set; } = "";

        /// <summary>
        /// Name of the library of this instance.
        /// </summary>
        [XmlAttribute("library")]
        public String LibraryName { get; set; } = "";

        /// <summary>
        /// Definitions of this instance.
        /// </summary>
        [XmlArray("definitions")]
        [XmlArrayItem("add.definition")]
        public List<T> Definitions { get; set; } = new List<T>();

        /// <summary>
        /// Groups of this instance.
        /// </summary>
        [XmlArray("groups")]
        [XmlArrayItem("group")]
        public List<AppExtensionItemGroup> Groups => _groups ?? (_groups = new List<AppExtensionItemGroup>());

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TAppExtensionItemIndex class.
        /// </summary>
        public TAppExtensionItemIndex()
        {
        }

        #endregion
    }
}

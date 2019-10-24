﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Items;

namespace BindOpen.Framework.Core.Extensions.Indexes
{
    /// <summary>
    /// This class represents an application extension item index.
    /// </summary>
    /// <typeparam name="T">The class of this extension item definition.</typeparam>
    [Serializable()]
    [XmlType("TAppExtensionItemIndex", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "index", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class TAppExtensionItemIndexDto<T> : StoredDataItem, ITAppExtensionItemIndexDto<T> where T : AppExtensionItemDefinitionDto
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
        public string LibraryId { get; set; } = "";

        /// <summary>
        /// Name of the library of this instance.
        /// </summary>
        [XmlAttribute("library")]
        public string LibraryName { get; set; } = "";

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
        /// Instantiates a new instance of the TAppExtensionItemIndexDto class.
        /// </summary>
        public TAppExtensionItemIndexDto()
        {
        }

        #endregion
    }
}
using BindOpen.Framework.Data.Items;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Framework.Extensions.Runtime
{
    /// <summary>
    /// This class represents the group of BindOpen extension items.
    /// </summary>
    [XmlType("BdoExtensionItemGroup", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "extension.item.group", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoExtensionItemGroup : DescribedDataItem, IBdoExtensionItemGroup
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private List<BdoExtensionItemGroup> _subGroups;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Sub groups of this instance.
        /// </summary>
        [XmlArray("subGroups")]
        [XmlArrayItem("subGroup")]
        public List<BdoExtensionItemGroup> SubGroups => _subGroups ?? (_subGroups = new List<BdoExtensionItemGroup>());

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoExtensionItemGroup class.
        /// </summary>
        public BdoExtensionItemGroup()
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the group with the specified name.
        /// </summary>
        /// <param name="name">The name of the sub group to return.</param>
        /// <returns>Returns the sub group with the specified name.</returns>
        public IBdoExtensionItemGroup GetGroupWithName(string name)
        {
            if (name == null)
                return null;

            foreach (IBdoExtensionItemGroup group in _subGroups)
            {
                if (group.Name?.Equals(name, StringComparison.OrdinalIgnoreCase) == true)
                {
                    return group;
                }
            }

            return null;
        }

        #endregion
    }
}
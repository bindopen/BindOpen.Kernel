using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Extensions.Items
{
    /// <summary>
    /// This class represents the group of application extension items.
    /// </summary>
    [Serializable()]
    [XmlType("AppExtensionItemGroup", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "extension.item.group", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class AppExtensionItemGroup : DescribedDataItem, IAppExtensionItemGroup
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private List<AppExtensionItemGroup> _subGroups;

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
        public List<AppExtensionItemGroup> SubGroups => _subGroups ?? (_subGroups = new List<AppExtensionItemGroup>());

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AppExtensionItemGroup class.
        /// </summary>
        public AppExtensionItemGroup()
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
        public IAppExtensionItemGroup GetGroupWithName(string name)
        {
            if (name == null)
                return null;

            foreach (IAppExtensionItemGroup group in _subGroups)
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
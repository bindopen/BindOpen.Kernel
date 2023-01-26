using BindOpen.Data;
using BindOpen.Data.Items;
using System;
using System.Collections.Generic;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents the group of BindOpen extension items.
    /// </summary>
    public class BdoExtensionItemGroup : BdoItem, IBdoExtensionItemGroup
    {
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
        // IBdoExtensionItemGroup Implementation
        // ------------------------------------------

        #region IBdoExtensionItemGroup

        /// <summary>
        /// Area specifications of this instance.
        /// </summary>
        public List<IBdoExtensionItemGroup> SubGroups { get; set; }

        /// <summary>
        /// Returns the group with the specified name.
        /// </summary>
        /// <param name="name">The name of the sub group to return.</param>
        /// <returns>Returns the sub group with the specified name.</returns>
        public IBdoExtensionItemGroup GetGroupWithName(string name)
        {
            if (name == null || SubGroups == null)
                return null;

            foreach (IBdoExtensionItemGroup group in SubGroups)
            {
                if (group.Name?.Equals(name, StringComparison.OrdinalIgnoreCase) == true)
                {
                    return group;
                }
            }

            return null;
        }

        #endregion

        // ------------------------------------------
        // IStorable Implementation
        // ------------------------------------------

        #region IStorable

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Last modification date of this instance.
        /// </summary>
        public DateTime? LastModificationDate { get; set; }

        #endregion

        // ------------------------------------------
        // IReferenced Implementation
        // ------------------------------------------

        #region IReferenced

        /// <summary>
        /// 
        /// </summary>
        public string Key() => Name;

        #endregion

        // ------------------------------------------
        // IIdentified Implementation
        // ------------------------------------------

        #region IIdentified

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        #endregion

        // ------------------------------------------
        // INamed Implementation
        // ------------------------------------------

        #region INamed

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        #endregion

        // ------------------------------------------
        // IGloballyTitled Implementation
        // ------------------------------------------

        #region IGloballyTitled

        /// <summary>
        /// 
        /// </summary>
        public IBdoDictionary Title { get; set; }

        public string GetTitleText(string key = StringHelper.__Star, string defaultKey = StringHelper.__Star)
        {
            return Title?[key, defaultKey];
        }

        #endregion

        // ------------------------------------------
        // IGloballyDescribed Implementation
        // ------------------------------------------

        #region IGloballyDescribed

        /// <summary>
        /// 
        /// </summary>
        public IBdoDictionary Description { get; set; }

        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultKey"></param>
        /// <returns></returns>
        public string GetDescriptionText(string key = StringHelper.__Star, string defaultKey = StringHelper.__Star)
        {
            return Description?[key, defaultKey];
        }

        #endregion

    }
}
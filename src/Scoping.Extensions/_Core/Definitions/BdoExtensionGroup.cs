using BindOpen.Data;
using System;
using System.Collections.Generic;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This class represents the group of BindOpen extension items.
    /// </summary>
    public class BdoExtensionGroup : BdoObject, IBdoExtensionGroup
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoExtensionGroup class.
        /// </summary>
        public BdoExtensionGroup()
        {
        }

        #endregion

        // ------------------------------------------
        // IBdoExtensionGroup Implementation
        // ------------------------------------------

        #region IBdoExtensionGroup

        /// <summary>
        /// Area specifications of this instance.
        /// </summary>
        public IList<IBdoExtensionGroup> SubGroups { get; set; }

        /// <summary>
        /// Returns the group with the specified name.
        /// </summary>
        /// <param key="name">The name of the sub group to return.</param>
        /// <returns>Returns the sub group with the specified name.</returns>
        public IBdoExtensionGroup GetGroupWithName(string name)
        {
            if (name == null || SubGroups == null)
                return null;

            foreach (IBdoExtensionGroup group in SubGroups)
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
        public ITBdoDictionary<string> Title { get; set; }

        #endregion

        // ------------------------------------------
        // IGloballyDescribed Implementation
        // ------------------------------------------

        #region IGloballyDescribed

        /// <summary>
        /// 
        /// </summary>
        public ITBdoDictionary<string> Description { get; set; }

        #endregion

    }
}
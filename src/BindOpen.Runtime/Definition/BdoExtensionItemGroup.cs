using BindOpen.MetaData;
using BindOpen.MetaData.Items;
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
        // ITStorablePoco Implementation
        // ------------------------------------------

        #region ITStorablePoco

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        public IBdoExtensionItemGroup WithCreationDate(DateTime? date)
        {
            CreationDate = date;
            return this;
        }

        /// <summary>
        /// Last modification date of this instance.
        /// </summary>
        public DateTime? LastModificationDate { get; set; }

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        public IBdoExtensionItemGroup WithLastModificationDate(DateTime? date)
        {
            LastModificationDate = date;
            return this;
        }

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
        // IIdentifiedPoco Implementation
        // ------------------------------------------

        #region IIdentifiedPoco

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IBdoExtensionItemGroup WithId(string id)
        {
            Id = id;
            return this;
        }

        #endregion

        // ------------------------------------------
        // INamedPoco Implementation
        // ------------------------------------------

        #region INamedPoco

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBdoExtensionItemGroup WithName(string name)
        {
            Name = BdoMeta.NewName(name, "item_");
            return this;
        }

        #endregion

        // ------------------------------------------
        // IGloballyTitled Implementation
        // ------------------------------------------

        #region IGloballyTitled

        /// <summary>
        /// 
        /// </summary>
        public IBdoDictionary Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IBdoExtensionItemGroup AddTitle(KeyValuePair<string, string> item)
        {
            Title ??= BdoMeta.NewDictionary();
            Title.Add(item);
            return this;
        }

        public IBdoExtensionItemGroup WithTitle(IBdoDictionary dico)
        {
            Title = dico;
            return this;
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IBdoExtensionItemGroup AddDescription(KeyValuePair<string, string> item)
        {
            Description ??= BdoMeta.NewDictionary();
            Description.Add(item);
            return this;
        }

        public IBdoExtensionItemGroup WithDescription(IBdoDictionary dico)
        {
            Description = dico;
            return this;
        }

        /// <summary>
        /// 
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
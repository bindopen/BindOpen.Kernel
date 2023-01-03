using BindOpen.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a configuration.
    /// </summary>
    public class BdoConfiguration : BdoElementSet, IBdoConfiguration
    {
        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        public BdoConfiguration() : base()
        {
        }

        #endregion

        // -------------------------------------------------------
        // IBdoConfiguration Implementation
        // -------------------------------------------------------

        #region IBdoBaseConfiguration

        /// <summary>
        /// The using file paths of this instance.
        /// </summary>
        public List<string> UsedItemIds { get; set; }

        /// <summary>
        /// The using configuration statement of this instance.
        /// </summary>
        public IBdoConfiguration UsingConfiguration { get; set; }

        /// <summary>
        /// Sets the file paths of this instance.
        /// </summary>
        /// <param name="filePaths">The file paths to consider.</param>
        public IBdoConfiguration Using(params string[] ids)
        {
            UsedItemIds = ids?.ToList();

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
        public IBdoConfiguration WithCreationDate(DateTime? date)
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
        public IBdoConfiguration WithLastModificationDate(DateTime? date)
        {
            LastModificationDate = date;
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
        public IBdoConfiguration WithName(string name)
        {
            Name = BdoItems.NewName(name, "config_");
            return this;
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

        public IBdoConfiguration AddDescription(KeyValuePair<string, string> item)
        {
            Description ??= BdoItems.NewDictionary();
            Description.Add(item);
            return this;
        }

        public IBdoConfiguration WithDescription(IBdoDictionary dictionary)
        {
            Description = dictionary;
            return this;
        }

        public string GetDescriptionText(string key = "*", string defaultKey = "*")
        {
            return Description?[key, defaultKey];
        }

        #endregion
    }
}

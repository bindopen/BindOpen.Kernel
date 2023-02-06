using BindOpen.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a BindOpen extension dico.
    /// </summary>
    /// <typeparam name="T">The class of extension item definition to consider.</typeparam>
    public class TBdoExtensionDictionary<T> : BdoItem,
        ITBdoExtensionDictionary<T>
        where T : IBdoExtensionItemDefinition
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBdoExtensionDictionary class.
        /// </summary>
        public TBdoExtensionDictionary()
        {
        }

        #endregion

        // ------------------------------------------
        // ITBdoExtensionDictionary Implementation
        // ------------------------------------------

        #region ITBdoExtensionDictionary

        /// <summary>
        /// ID of the library of this instance.
        /// </summary>
        public string LibraryId { get; set; }

        public ITBdoExtensionDictionary<T> WithLibraryId(string libraryId)
        {
            LibraryId = libraryId;
            return this;
        }

        /// <summary>
        /// Name of the library of this instance.
        /// </summary>
        public string LibraryName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="libraryName"></param>
        /// <returns></returns>
        public ITBdoExtensionDictionary<T> WithLibraryName(string libraryName)
        {
            LibraryName = libraryName;
            return this;
        }

        /// <summary>
        /// Definitions of this instance.
        /// </summary>
        public List<T> Definitions { get; set; } = new List<T>();

        public ITBdoExtensionDictionary<T> WithDefinitions(params T[] definitions)
        {
            Definitions = definitions.ToList();
            return this;
        }

        public ITBdoExtensionDictionary<T> AddDefinitions(params T[] definitions)
        {
            Definitions ??= new List<T>();
            Definitions.AddRange(definitions.ToList());
            return this;
        }

        /// <summary>
        /// Groups of this instance.
        /// </summary>
        public List<IBdoExtensionItemGroup> Groups { get; set; }

        public ITBdoExtensionDictionary<T> WithGroups(params IBdoExtensionItemGroup[] groups)
        {
            Groups = groups.ToList();
            return this;
        }

        public ITBdoExtensionDictionary<T> AddGroups(params IBdoExtensionItemGroup[] groups)
        {
            Groups ??= new List<IBdoExtensionItemGroup>();
            Groups.AddRange(groups.ToList());
            return this;
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

        #endregion

        // ------------------------------------------
        // IGloballyDescribed Implementation
        // ------------------------------------------

        #region IGloballyDescribed

        /// <summary>
        /// 
        /// </summary>
        public IBdoDictionary Description { get; set; }

        #endregion

    }
}

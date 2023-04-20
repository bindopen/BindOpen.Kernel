using BindOpen.Data;
using System;
using System.Collections.Generic;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This class represents a BindOpen extension dico.
    /// </summary>
    /// <typeparam name="T">The class of extension item definition to consider.</typeparam>
    public class TBdoExtensionDictionary<T> : TBdoSet<T>,
        ITBdoExtensionDictionary<T>
        where T : IBdoExtensionDefinition
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

        /// <summary>
        /// Name of the library of this instance.
        /// </summary>
        public string LibraryName { get; set; }

        /// <summary>
        /// Groups of this instance.
        /// </summary>
        public IList<IBdoExtensionGroup> Groups { get; set; }

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
        public override string Key() => Name;

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

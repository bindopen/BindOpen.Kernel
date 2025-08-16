using BindOpen.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a key value pair database entity.
    /// </summary>
    public class KeyValuePairDb
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The key of this instance.
        /// </summary>
        public string Key { get; set; } = StringHelper.__Star;

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        public string StringDictionaryId { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        [ForeignKey(nameof(StringDictionaryId))]
        public StringDictionaryDb StringDictionary { get; set; }

        /// <summary>
        /// The value of this instance.
        /// </summary>
        public string Value { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of KeyValuePairDb class.
        /// </summary>
        public KeyValuePairDb()
        {
        }

        #endregion
    }
}

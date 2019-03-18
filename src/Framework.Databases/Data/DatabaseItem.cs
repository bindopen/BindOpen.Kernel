using System;

namespace dkm_core_wdl.data.Carriers.database
{

    /// <summary>
    /// This class represents the information about a database element.
    /// </summary>
    public class DatabaseElement
    {
        
        // ---------------------------------
        // PROPERTIES
        // ---------------------------------

        #region Properties

        /// <summary>
        /// Name of this instance.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Date of creation of this instance.
        /// </summary>
        public DateTime CreationTime { get; set; }

        #endregion


        // **************************************
        // CONSTRUCTORS
        // **************************************

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DatabaseElement class.
        /// </summary>
        public DatabaseElement()
            : base()
        {
        }

        #endregion

    }
}

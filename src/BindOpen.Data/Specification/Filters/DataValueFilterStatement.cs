using BindOpen.Data.Items;
using System.Collections.Generic;

namespace BindOpen.Data.Specification
{
    /// <summary>
    /// This interface specifies the value filter statement.
    /// </summary>
    public class DataValueFilterStatement : BdoItem, IDataValueFilterStatement
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Output detail of this instance.
        /// </summary>
        public List<IDataValueFilter> Filters { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataValueFilterStatement class.
        /// </summary>
        public DataValueFilterStatement()
        {
        }

        #endregion
    }

}

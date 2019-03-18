using System.Collections.Generic;
using System.ComponentModel;

namespace BindOpen.Framework.Databases.Data.Queries
{

    /// <summary>
    /// This class represents the From statement of a database data query.
    /// </summary>
    public class DbDataQueryFromStatement
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private DbDataQueryUnionStatement _UnionStatement;
        private List<DbDataQueryJointureStatement> _JointureStatements = new List<DbDataQueryJointureStatement>();

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Union statement.
        /// </summary>
        public DbDataQueryUnionStatement UnionStatement
        {
            get { return this._UnionStatement; }
            set { this._UnionStatement = value; }
        }

        /// <summary>
        /// List of jointure statements.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<DbDataQueryJointureStatement> JointureStatements
        {
            get { return this._JointureStatements; }
            set { this._JointureStatements = new List<DbDataQueryJointureStatement>(value); }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbDataQueryFromStatement class.
        /// </summary>
        public DbDataQueryFromStatement()
        {
        }

        #endregion

    }
}
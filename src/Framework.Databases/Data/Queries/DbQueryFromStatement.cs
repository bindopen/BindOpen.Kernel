using System.Collections.Generic;
using System.ComponentModel;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// This class represents the From statement of a database data query.
    /// </summary>
    public class DbQueryFromStatement : IDbQueryFromStatement
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private IDbQueryUnionStatement _unionStatement;
        private List<IDbQueryJointureStatement> _jointureStatements = new List<IDbQueryJointureStatement>();

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Union statement.
        /// </summary>
        public IDbQueryUnionStatement UnionStatement
        {
            get { return this._unionStatement; }
            set { this._unionStatement = value; }
        }

        /// <summary>
        /// List of jointure statements.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<IDbQueryJointureStatement> JointureStatements
        {
            get { return this._jointureStatements; }
            set { this._jointureStatements = new List<IDbQueryJointureStatement>(value); }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryFromStatement class.
        /// </summary>
        public DbQueryFromStatement()
        {
        }

        #endregion
    }
}
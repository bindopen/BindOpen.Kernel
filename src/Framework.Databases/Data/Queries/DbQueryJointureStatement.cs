using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// This class represents the Jointure statement of a database data query.
    /// </summary>
    public class DbQueryJointureStatement : IDbQueryJointureStatement
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The kind of jointure of this instance.
        /// </summary>
        public DbQueryJointureKind Kind { get; set; }

        /// <summary>
        /// The data module of this instance.
        /// </summary>
        public DbTable Table { get; set; }

        /// <summary>
        /// The condition of this instance.
        /// </summary>
        public DataExpression Condition { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryJointureStatement class.
        /// </summary>
        public DbQueryJointureStatement()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DbQueryJointureStatement class.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="table">The table to consider.</param>
        public DbQueryJointureStatement(
            DbQueryJointureKind kind,
            DbTable table)
        {
            this.Kind = kind;
            this.Table = table;
        }

        /// <summary>
        /// Instantiates a new instance of the DbQueryJointureStatement class.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="table">The table to consider.</param>
        /// <param name="query">The query to consider.</param>
        public DbQueryJointureStatement(
            DbQueryJointureKind kind,
            DbTable table,
            string query)
        {
            this.Kind = kind;
            this.Table = table;
            this.Condition = query.CreateScript();
        }

        /// <summary>
        /// Instantiates a new instance of the DbQueryJointureStatement class.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="table">The table to consider.</param>
        /// <param name="field1">The field1 to consider.</param>
        /// <param name="field2">The field2 to consider.</param>
        public DbQueryJointureStatement(
            DbQueryJointureKind kind,
            DbTable table,
            DbField field1,
            DbField field2)
        {
            this.Kind = kind;
            this.Table = table;

            if (field1 != null && field2 != null)
            {
                string query = "$sqlEq($" +
                   (string.IsNullOrEmpty(field1.DataModule) ? "" : ("sqlDatabase('" + field1.DataModule + "').")) +
                   (string.IsNullOrEmpty(field1.Schema) ? "" : ("sqlSchema('" + field1.Schema + "').")) +
                   (string.IsNullOrEmpty(field1.DataTable) ? "" : ("sqlTable('" + field1.DataTable + "').")) +
                   (string.IsNullOrEmpty(field1.Name) ? "" : ("sqlField('" + field1.Name + "')"));

                query += ", $" +
                   (string.IsNullOrEmpty(field2.DataModule) ? "" : ("sqlDatabase('" + field2.DataModule + "').")) +
                   (string.IsNullOrEmpty(field2.Schema) ? "" : ("sqlSchema('" + field2.Schema + "').")) +
                   (string.IsNullOrEmpty(field2.DataTable) ? "" : ("sqlTable('" + field2.DataTable + "').")) +
                   (string.IsNullOrEmpty(field2.Name) ? "" : ("sqlField('" + field2.Name + "')"));

                query += ")";

                this.Condition = query.CreateScript();
            }
        }

        #endregion
    }
}
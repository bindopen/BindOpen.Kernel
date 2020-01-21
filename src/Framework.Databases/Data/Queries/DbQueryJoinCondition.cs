using BindOpen.Framework.Data.Common;
using BindOpen.Framework.Extensions.Carriers;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This class represents the condition of a database query join.
    /// </summary>
    public class DbQueryJoinCondition : IDbQueryJoinCondition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The field 1 of this instance.
        /// </summary>
        public DbField Field1 { get; set; }

        /// <summary>
        /// The field 2 of this instance.
        /// </summary>
        public DbField Field2 { get; set; }

        /// <summary>
        /// The operator of this instance.
        /// </summary>
        public DataOperator Operator { get; set; } = DataOperator.Equal;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryJoinCondition class.
        /// </summary>
        public DbQueryJoinCondition()
        {
        }

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Returns the data expression string corresponding to this instance.
        /// </summary>
        public override string ToString()
        {
            string query = "";

            if (Field1 != null && Field2 != null)
            {
                switch (Operator)
                {
                    case DataOperator.Equal:
                        query += "$sqlEq(";
                        break;
                    case DataOperator.Different:
                        query += "$sqlDiff(";
                        break;
                    case DataOperator.Greater:
                        query += "$sqlGt(";
                        break;
                    case DataOperator.GreaterOrEqual:
                        query += "$sqlGte(";
                        break;
                    case DataOperator.Lesser:
                        query += "$sqlLt(";
                        break;
                    case DataOperator.LesserOrEqual:
                        query += "$sqlLte(";
                        break;
                }
                query += "$" +
                    (string.IsNullOrEmpty(Field1.DataModule) ? "" : ("sqlDatabase('" + Field1.DataModule + "').")) +
                    (string.IsNullOrEmpty(Field1.Schema) ? "" : ("sqlSchema('" + Field1.Schema + "').")) +
                    (string.IsNullOrEmpty(Field1.DataTable) ? "" : ("sqlTable('" + Field1.DataTable + "').")) +
                    (string.IsNullOrEmpty(Field1.Name) ? "" : ("sqlField('" + Field1.Name + "')"));

                query += ", $" +
                    (string.IsNullOrEmpty(Field2.DataModule) ? "" : ("sqlDatabase('" + Field2.DataModule + "').")) +
                    (string.IsNullOrEmpty(Field2.Schema) ? "" : ("sqlSchema('" + Field2.Schema + "').")) +
                    (string.IsNullOrEmpty(Field2.DataTable) ? "" : ("sqlTable('" + Field2.DataTable + "').")) +
                    (string.IsNullOrEmpty(Field2.Name) ? "" : ("sqlField('" + Field2.Name + "')"));

                query += ")";
            }

            return query;
        }

        #endregion
    }
}
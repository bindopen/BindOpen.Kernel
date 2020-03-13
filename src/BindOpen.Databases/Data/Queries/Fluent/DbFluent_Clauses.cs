using BindOpen.Extensions.Carriers;
using System.Linq;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a factory of basic database query statement.
    /// </summary>
    public static partial class DbFluent
    {
        // From --------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tables">The tables to consider.</param>
        public static IDbQueryFromClause From(params DbTable[] tables)
            => new DbQueryFromClause() { Tables = tables?.ToList() };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tables">The tables to consider.</param>
        /// <param name="unionClauses">The union clauses to consider.</param>
        public static IDbQueryFromClause From(DbTable[] tables, params DbUnionTable[] unionClauses)
            => new DbQueryFromClause() { Tables = tables?.ToList(), UnionTables = unionClauses?.ToList() };
    }
}

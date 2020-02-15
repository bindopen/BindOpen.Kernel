using BindOpen.Data.Elements;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    internal partial class DbQueryBuilder_PostgreSql : DbQueryBuilder
    {
        // ------------------------------------------
        // BASIC QUERY BUILBING
        // ------------------------------------------

        #region Basic Query Building

        /// <summary>
        /// Builds the SQL text of the specified merge query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the built query text.</returns>
        protected override string GetSqlText(
            ICompositeDbQuery query,
            IDataElementSet parameterSet = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            var queryString = "";

            // we build the query
            switch (query.Kind)
            {
                // Upsert
                case DbQueryKind.Upsert:
                    {
                        queryString = "merge ";
                        queryString += GetSqlText_Table(
                            query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                            DbDataFieldViewMode.CompleteNameAsAlias, query.DataModule, query.Schema,
                            scriptVariableSet: scriptVariableSet, log: log);

                        if (query.SelectJoinStatement != null)
                        {
                            query.SelectJoinStatement.Kind = DbQueryJoinKind.Left;
                            var subQueryString = GetSqlText_Join(query.SelectJoinStatement, query, parameterSet, scriptVariableSet, log);
                            subQueryString = subQueryString.Substring("left join ".Length);
                            queryString += subQueryString;
                        }

                        queryString += " when matched ";
                        queryString += BuildQuery(query.MatchedQuery, null, parameterSet, scriptVariableSet, log);
                        UpdateParameterSet(query.ParameterSet, query.MatchedQuery);

                        queryString += " when not matched ";
                        queryString += BuildQuery(query.NotMatchedQuery, null, parameterSet, scriptVariableSet, log);
                        queryString += ";";
                        UpdateParameterSet(query.ParameterSet, query.NotMatchedQuery);
                    }
                    break;
            }

            return queryString;
        }

        #endregion
    }
}
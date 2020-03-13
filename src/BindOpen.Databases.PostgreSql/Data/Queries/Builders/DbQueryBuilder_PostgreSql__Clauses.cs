using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Extensions.Carriers;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    internal partial class DbQueryBuilder_PostgreSql : DbQueryBuilder
    {
        // From -------------------------------------

        private string GetSqlText_FromClause(
            IDbQueryFromClause clause,
            IDbSingleQuery query = null,
            IDataElementSet parameterSet = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            string queryString = "";

            if (clause == null)
            {
                // we add the query's default table

                queryString += GetSqlText_Table(
                    query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                    query, parameterSet, DbFieldViewMode.CompleteName,
                    query.DataModule, query.Schema,
                    scriptVariableSet: scriptVariableSet, log: log);
            }
            else
            {
                if (clause?.Value != null)
                {
                    string expression = _scope?.Interpreter.Interprete(clause.Value, scriptVariableSet, log) ?? "";
                    queryString += expression;
                }
                else
                {
                    // if the first table is not a joined one then we add first the query's default table

                    if (clause.Tables == null || clause.Tables.Count == 0 || clause.Tables[0] is DbJoinedTable)
                    {
                        queryString += GetSqlText_Table(
                            query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                            query, parameterSet, DbFieldViewMode.CompleteNameAsAlias,
                            query.DataModule, query.Schema,
                            scriptVariableSet: scriptVariableSet, log: log);
                    }
                    else if (clause.Tables?.Count > 0)
                    {
                        foreach (var table in clause.Tables)
                        {
                            queryString += GetSqlText_Table(
                                table,
                                query, parameterSet, DbFieldViewMode.CompleteNameAsAlias,
                                query.DataModule, query.Schema,
                                scriptVariableSet: scriptVariableSet, log: log);
                        }
                    }

                    if (clause.UnionTables?.Count > 0)
                    {
                        foreach (var table in clause.UnionTables)
                        {
                            queryString += GetSqlText_Table(
                                table,
                                query, parameterSet, DbFieldViewMode.CompleteNameAsAlias,
                                query.DataModule, query.Schema,
                                scriptVariableSet: scriptVariableSet, log: log);
                        }
                    }
                }
            }
            queryString = queryString.If(!string.IsNullOrEmpty(queryString), " from " + queryString);

            return queryString;
        }

        // Where -------------------------------------

        private string GetSqlText_WhereClause(
            IDbQueryWhereClause clause,
            IDbSingleQuery query = null,
            IDataElementSet parameterSet = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            string queryString = "";

            if (clause != null)
            {
                if (clause?.Value != null)
                {
                    string expression = _scope?.Interpreter.Interprete(clause.Value, scriptVariableSet, log) ?? "";
                    queryString += expression;
                }
                if (clause.IdFields?.Count > 0)
                {
                    queryString = queryString.If(!string.IsNullOrEmpty(queryString), " (" + queryString + ") ");

                    foreach (DbField field in clause.IdFields)
                    {
                        if (!string.IsNullOrEmpty(queryString))
                        {
                            queryString += " and ";
                        }
                        queryString += GetSqlText_Field(
                            field, query, parameterSet, DbFieldViewMode.NameEqualsValue,
                            query.DataModule, query.Schema, query.DataTable,
                            scriptVariableSet: scriptVariableSet, log: log);
                    }
                }
                queryString = queryString.If(!string.IsNullOrEmpty(queryString), " where " + queryString);
            }

            return queryString;
        }

        // OrderBy -------------------------------------

        private string GetSqlText_OrderByClause(
            IDbQueryOrderByClause clause,
            IDbSingleQuery query = null,
            IDataElementSet parameterSet = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            string queryString = "";

            if (clause != null)
            {
                if (clause?.Value != null)
                {
                    string expression = _scope?.Interpreter.Interprete(clause.Value, scriptVariableSet, log) ?? "";
                    queryString += expression;
                }
                else if (clause.Statements?.Count > 0)
                {
                    foreach (var statement in clause.Statements)
                    {
                        if (!string.IsNullOrEmpty(queryString))
                        {
                            queryString += ", ";
                        }
                        if (statement.Sorting == DataSortingMode.Random)
                        {
                            queryString += "newid()";
                        }
                        else
                        {
                            queryString += GetSqlText_Field(
                                statement.Field, query,
                                parameterSet,
                                DbFieldViewMode.OnlyName,
                                scriptVariableSet: scriptVariableSet, log: log);

                            switch (statement.Sorting)
                            {
                                case DataSortingMode.Ascending:
                                    queryString += " asc";
                                    break;
                                case DataSortingMode.Descending:
                                    queryString += " desc";
                                    break;
                            }
                        }
                    }
                }
                queryString = queryString.If(!string.IsNullOrEmpty(queryString), " order by " + queryString);
            }

            return queryString;
        }

        // GroupBy -------------------------------------

        private string GetSqlText_GroupByClause(
            IDbQueryGroupByClause clause,
            IDbSingleQuery query = null,
            IDataElementSet parameterSet = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            string queryString = "";

            if (clause != null)
            {
                if (clause?.Value != null)
                {
                    string expression = _scope?.Interpreter.Interprete(clause.Value, scriptVariableSet, log) ?? "";
                    queryString += expression;
                }
                else if (clause.Fields?.Count > 0)
                {
                    foreach (DbField field in clause.Fields)
                    {
                        if (!string.IsNullOrEmpty(queryString))
                        {
                            queryString += ", ";
                        }
                        queryString += GetSqlText_Field(
                            field, query, parameterSet, DbFieldViewMode.CompleteNameAsAlias,
                            query.DataModule, query.Schema, query.DataTable,
                            scriptVariableSet: scriptVariableSet, log: log);
                    }
                }
                queryString = queryString.If(!string.IsNullOrEmpty(queryString), " group by " + queryString);
            }

            return queryString;
        }

        // Having -------------------------------------

        private string GetSqlText_HavingClause(
            IDbQueryHavingClause clause,
            IDbSingleQuery query = null,
            IDataElementSet parameterSet = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            string queryString = "";

            if (clause != null)
            {
                if (clause?.Value != null)
                {
                    string expression = _scope?.Interpreter.Interprete(clause.Value, scriptVariableSet, log) ?? "";
                    queryString += expression;
                }


                queryString = queryString.If(!string.IsNullOrEmpty(queryString), " having " + queryString);
            }

            return queryString;
        }
    }
}
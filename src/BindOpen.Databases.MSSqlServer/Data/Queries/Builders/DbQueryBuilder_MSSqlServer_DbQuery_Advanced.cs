using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Extensions.Carriers;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    internal partial class DbQueryBuilder_MSSqlServer : DbQueryBuilder
    {
        // ------------------------------------------
        // ADVANCED QUERY BUILBING
        // ------------------------------------------

        #region Advanced Query Building

        // Builds advanced query ----------------------

        /// <summary>
        /// Builds the specified simple database data query and put the result
        /// into the specified string MS Sql Server query.
        /// <remarks>We assume the query already exits.</remarks>
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the built query text.</returns>
        protected override string GetSqlText(
            IAdvancedDbQuery query,
            IDataElementSet parameterSet = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            var queryString = "";
            int index;

            // we build the query
            switch (query.Kind)
            {
                // Select
                case DbQueryKind.Select:
                    {
                        queryString = "select ";
                        if (query.IsDistinct)
                            queryString += " distinct ";
                        if (query.Limit > -1)
                            queryString += " top " + query.Limit.ToString() + " ";
                        index = 0;
                        if (query.Fields?.Count > 0)
                        {
                            foreach (DbField field in query.Fields)
                            {
                                if (index > 0)
                                    queryString += ",";

                                queryString += GetSqlText_Field(
                                    field, parameterSet, log, DbDataFieldViewMode.CompleteNameAsAlias, scriptVariableSet, query.DataModule, query.Schema
                                );

                                index++;
                            }
                        }
                        else
                        {
                            queryString += " * ";
                        }
                        queryString += " from ";
                        index = 0;

                        foreach (DbQueryFromStatement queryFrom in query.FromStatements)
                        {
                            if (index > 0)
                                queryString += ",";

                            queryString += GetSqlText_From(queryFrom, query, parameterSet, scriptVariableSet, log);

                            index++;
                        }


                        if (query.WhereClause != null && query.WhereClause.Text != null)
                        {
                            queryString += " where ";
                            string expression = _scope?.Interpreter.Interprete(query.WhereClause, scriptVariableSet, log) ?? "";
                            queryString += expression;
                        }
                        if (query.GroupByClause != null)
                        {
                            queryString += " group by ";
                            index = 0;
                            foreach (DbField field in query.GroupByClause.Fields)
                            {
                                if (index > 0)
                                    queryString += ", ";

                                queryString += GetSqlText_Field(
                                    field, parameterSet, log, DbDataFieldViewMode.CompleteNameAsAlias, scriptVariableSet,
                                    query.DataModule, query.Schema, query.DataTable
                                );

                                index++;
                            }
                        }
                        if (query.HavingClause != null)
                        {
                            queryString += " having ";
                            string expression = _scope?.Interpreter.Interprete(query.WhereClause, scriptVariableSet, log) ?? "";
                            queryString += expression;
                        }
                        if (query.OrderByStatements?.Count > 0)
                        {
                            queryString += " order by ";
                            index = 0;
                            foreach (DbQueryOrderByStatement queryOrderByStatement in query.OrderByStatements)
                            {
                                if (index > 0)
                                    queryString += ", ";
                                if (queryOrderByStatement.Sorting == DataSortingMode.Random)
                                {
                                    queryString += "newid()";
                                }
                                else
                                {
                                    queryString += GetSqlText_Field(
                                        queryOrderByStatement.Field,
                                        parameterSet,
                                        log,
                                        DbDataFieldViewMode.OnlyName,
                                        scriptVariableSet);

                                    switch (queryOrderByStatement.Sorting)
                                    {
                                        case DataSortingMode.Ascending:
                                            queryString += " asc";
                                            break;
                                        case DataSortingMode.Descending:
                                            queryString += " desc";
                                            break;
                                    }
                                }
                                index++;
                            }
                        }
                    }
                    break;
                // Update
                case DbQueryKind.Update:
                    {
                        queryString = "update ";
                        queryString += GetSqlText_Table(
                            query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                            DbDataFieldViewMode.CompleteNameAsAlias, query.DataModule, query.Schema);
                        queryString += " set ";
                        index = 0;
                        foreach (DbField field in query.Fields)
                        {
                            if (index > 0)
                                queryString += ",";

                            queryString += GetSqlText_Field(field, parameterSet, log, DbDataFieldViewMode.NameEqualsValue, scriptVariableSet);

                            index++;
                        }
                        if (query.ReturnedIdFields?.Count > 0)
                        {
                            queryString += " output inserted ";
                            index = 0;
                            foreach (DbField field in query.ReturnedIdFields)
                            {
                                if (index > 0)
                                    queryString += ", ";
                                queryString += GetSqlText_Field(
                                    field, parameterSet, log, DbDataFieldViewMode.NameEqualsValue, scriptVariableSet,
                                    query.DataModule, query.Schema, query.DataTable
                                );

                                index++;
                            }
                        }
                        if (query.FromStatements?.Count > 0)
                        {
                            queryString += " from ";
                            index = 0;
                            foreach (DbQueryFromStatement queryFrom in query.FromStatements)
                            {
                                if (index > 0)
                                    queryString += ",";

                                queryString += GetSqlText_From(queryFrom, query, parameterSet, scriptVariableSet, log);

                                index++;
                            }
                        }
                        if (query.WhereClause != null)
                        {
                            queryString += " where ";
                            string expression = _scope?.Interpreter.Interprete(query.WhereClause, scriptVariableSet, log) ?? "";
                            queryString += expression;
                        }
                    }
                    break;
                // Delete
                case DbQueryKind.Delete:
                    {
                        queryString = "delete";
                        if (query.ReturnedIdFields?.Count > 0)
                        {
                            queryString += " output deleted ";
                            index = 0;
                            foreach (DbField field in query.ReturnedIdFields)
                            {
                                if (index > 0)
                                    queryString += ", ";
                                queryString += GetSqlText_Field(
                                    field, parameterSet, log, DbDataFieldViewMode.NameEqualsValue, scriptVariableSet,
                                    query.DataModule, query.Schema, query.DataTable
                                );

                                index++;
                            }
                        }
                        queryString = " from ";
                        if (query.FromStatements?.Count > 0)
                        {
                            index = 0;
                            foreach (DbQueryFromStatement queryFrom in query.FromStatements)
                            {
                                if (index > 0)
                                    queryString += ",";

                                queryString += GetSqlText_From(queryFrom, query, parameterSet, scriptVariableSet, log);

                                index++;
                            }
                        }
                        else
                        {
                            queryString += GetSqlText_Table(
                                query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                                DbDataFieldViewMode.CompleteName, query.DataModule, query.Schema);
                        }

                        if (query.WhereClause != null)
                        {
                            queryString += " where ";
                            string expression = _scope?.Interpreter.Interprete(query.WhereClause, scriptVariableSet, log) ?? "";
                            queryString += expression;
                        }
                    }
                    break;
                // Insert
                case DbQueryKind.Insert:
                    {
                        queryString = "insert into ";
                        queryString += GetSqlText_Table(
                            query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                            DbDataFieldViewMode.CompleteName, query.DataModule, query.Schema);
                        queryString += " (";
                        index = 0;
                        foreach (DbField field in query.Fields)
                        {
                            if (index > 0)
                                queryString += ",";

                            queryString += GetSqlText_Field(
                                field, parameterSet, log, DbDataFieldViewMode.OnlyName,
                                scriptVariableSet, query.DataModule, query.Schema);

                            index++;
                        }
                        queryString += ")";
                        if (query.ReturnedIdFields?.Count > 0)
                        {
                            queryString += " output inserted ";
                            index = 0;
                            foreach (DbField field in query.ReturnedIdFields)
                            {
                                if (index > 0)
                                    queryString += ", ";
                                queryString += GetSqlText_Field(
                                    field, parameterSet, log, DbDataFieldViewMode.NameEqualsValue, scriptVariableSet,
                                    query.DataModule, query.Schema, query.DataTable
                                );

                                index++;
                            }
                        }
                        queryString += " values (";
                        if (query.Fields?.Count > 0)
                        {
                            index = 0;
                            foreach (DbField field in query.Fields)
                            {
                                if (index > 0)
                                    queryString += ",";

                                queryString += GetSqlText_Field(
                                    field, parameterSet, log, DbDataFieldViewMode.OnlyValue,
                                    scriptVariableSet, query.DataModule, query.Schema);

                                index++;
                            }
                        }
                        queryString += ")";
                    }
                    break;
            }

            return queryString;
        }

        #endregion
    }
}
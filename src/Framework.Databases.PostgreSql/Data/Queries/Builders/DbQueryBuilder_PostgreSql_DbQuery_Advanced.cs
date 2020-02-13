using BindOpen.Framework.Data.Common;
using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Data.Helpers.Strings;
using BindOpen.Framework.Extensions.Carriers;
using BindOpen.Framework.System.Diagnostics;
using BindOpen.Framework.System.Scripting;
using System;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    internal partial class DbQueryBuilder_PostgreSql : DbQueryBuilder
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
                        if (query.Top > -1)
                            queryString += " top " + query.Top.ToString() + " ";
                        index = 0;
                        if (query.Fields?.Count > 0)
                        {
                            foreach (DbField field in query.Fields)
                            {
                                if (index > 0)
                                    queryString += ",";

                                queryString += GetSqlText_Field(
                                    field, parameterSet, DbDataFieldViewMode.CompleteNameAsAlias, query.DataModule, query.Schema, null, scriptVariableSet, log);

                                index++;
                            }
                        }
                        else
                        {
                            queryString += " * ";
                        }
                        if ((query.FromStatements == null) | (query.FromStatements.Count == 0))
                        {
                            var tableString = GetSqlText_Table(
                                query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                                DbDataFieldViewMode.CompleteName,
                                query.DataModule, query.Schema);

                            queryString.ConcatenateIf(!string.IsNullOrEmpty(tableString), " from " + tableString);
                        }
                        else
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

                        if (query.WhereClause != null && query.WhereClause.Text != null)
                        {
                            queryString += " where ";
                            String expression = _scope?.Interpreter.Interprete(query.WhereClause, scriptVariableSet, log) ?? "";
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
                                    field, parameterSet, DbDataFieldViewMode.CompleteNameAsAlias,
                                    query.DataModule, query.Schema, query.DataTable, scriptVariableSet, log);

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
                                        DbDataFieldViewMode.OnlyName,
                                        scriptVariableSet: scriptVariableSet,
                                        log: log);

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

                            queryString += GetSqlText_Field(
                                field, parameterSet, DbDataFieldViewMode.NameEqualsValue,
                                scriptVariableSet: scriptVariableSet, log: log);

                            index++;
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
                            String expression = _scope?.Interpreter.Interprete(query.WhereClause, scriptVariableSet, log) ?? "";
                            queryString += expression;
                        }
                        if (query.ReturnedIdFields?.Count > 0)
                        {
                            queryString += " returning ";
                            index = 0;
                            foreach (DbField field in query.ReturnedIdFields)
                            {
                                if (index > 0)
                                    queryString += ", ";
                                queryString += GetSqlText_Field(
                                    field, parameterSet, DbDataFieldViewMode.NameEqualsValue,
                                    query.DataModule, query.Schema, query.DataTable,
                                    scriptVariableSet: scriptVariableSet, log: log
                                );

                                index++;
                            }
                        }
                    }
                    break;
                // Delete
                case DbQueryKind.Delete:
                    {
                        queryString = "delete";
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
                                DbDataFieldViewMode.CompleteName, query.DataModule, query.Schema,
                                scriptVariableSet: scriptVariableSet, log: log);
                        }

                        if (query.WhereClause != null)
                        {
                            queryString += " where ";
                            string expression = _scope?.Interpreter.Interprete(query.WhereClause, scriptVariableSet, log) ?? "";
                            queryString += expression;
                        }
                        if (query.ReturnedIdFields?.Count > 0)
                        {
                            queryString += " returning ";
                            index = 0;
                            foreach (DbField field in query.ReturnedIdFields)
                            {
                                if (index > 0)
                                    queryString += ", ";
                                queryString += GetSqlText_Field(
                                    field, parameterSet, DbDataFieldViewMode.NameEqualsValue,
                                    query.DataModule, query.Schema, query.DataTable,
                                    scriptVariableSet: scriptVariableSet, log: log
                                );

                                index++;
                            }
                        }
                    }
                    break;
                // Insert
                case DbQueryKind.Insert:
                    {
                        queryString = "insert into ";
                        queryString += GetSqlText_Table(
                            query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                            DbDataFieldViewMode.CompleteName, query.DataModule, query.Schema,
                            scriptVariableSet: scriptVariableSet, log: log);
                        queryString += " (";
                        index = 0;
                        foreach (DbField field in query.Fields)
                        {
                            if (index > 0)
                                queryString += ",";

                            queryString += GetSqlText_Field(
                                field, parameterSet, DbDataFieldViewMode.OnlyName,
                                query.DataModule, query.Schema,
                                scriptVariableSet: scriptVariableSet, log: log);

                            index++;
                        }
                        queryString += ") values (";
                        if (query.Fields?.Count > 0)
                        {
                            index = 0;
                            foreach (DbField field in query.Fields)
                            {
                                if (index > 0)
                                    queryString += ",";

                                queryString += GetSqlText_Field(
                                    field, parameterSet, DbDataFieldViewMode.OnlyValue,
                                    query.DataModule, query.Schema,
                                    scriptVariableSet: scriptVariableSet, log: log);

                                index++;
                            }
                        }
                        queryString += ")";
                        if (query.ReturnedIdFields?.Count > 0)
                        {
                            queryString += " returning ";
                            index = 0;
                            foreach (DbField field in query.ReturnedIdFields)
                            {
                                if (index > 0)
                                    queryString += ", ";
                                queryString += GetSqlText_Field(
                                    field, parameterSet, DbDataFieldViewMode.NameEqualsValue,
                                    query.DataModule, query.Schema, query.DataTable,
                                    scriptVariableSet: scriptVariableSet, log: log
                                );

                                index++;
                            }
                        }
                    }
                    break;
            }

            return queryString;
        }

        #endregion
    }
}
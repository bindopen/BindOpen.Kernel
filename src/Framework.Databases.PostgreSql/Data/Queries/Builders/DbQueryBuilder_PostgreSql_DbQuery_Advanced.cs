using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Data.Queries;
using BindOpen.Framework.Databases.Data.Queries.Builders;
using BindOpen.Framework.Databases.Extensions.Carriers;
using System;

namespace BindOpen.Framework.Databases.PostgreSql.Data.Queries.Builders
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
        /// Builds the specified advanced database data query and put the result
        /// into the specified string MS Sql Server query.
        /// <remarks>We assume the query already exits.</remarks>
        /// </summary>
        /// <param name="query"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="queryString"></param>
        protected override IBdoLog Build(
            IAdvancedDbQuery query,
            IDataElementSet parameterSet,
            IBdoScriptVariableSet scriptVariableSet,
            out string queryString)
        {
            IBdoLog log = new BdoLog();
            queryString = "";


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

                                queryString += GetFieldSqlText(
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

                            queryString += GetJointureSqlText(queryFrom, parameterSet, scriptVariableSet, log);

                            index++;
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

                                queryString += GetFieldSqlText(
                                    field, parameterSet, log, DbDataFieldViewMode.CompleteNameAsAlias, scriptVariableSet,
                                    query.DataModule, query.Schema, query.DataTable
                                );

                                index++;
                            }
                        }
                        if (query.HavingClause != null)
                        {
                            queryString += " having ";
                            String expression = _scope?.Interpreter.Interprete(query.WhereClause, scriptVariableSet, log) ?? "";
                            queryString += expression;
                        }
                        if (query.OrderByStatements.Count > 0)
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
                                    queryString += GetFieldSqlText(
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
                        queryString += GetTableSqlText(
                            query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                            DbDataFieldViewMode.CompleteNameAsAlias, query.DataModule, query.Schema);
                        queryString += " set ";
                        index = 0;
                        foreach (DbField field in query.Fields)
                        {
                            if (index > 0)
                                queryString += ",";

                            queryString += GetFieldSqlText(
                                field, parameterSet, log, DbDataFieldViewMode.NameEqualsValue,
                                scriptVariableSet);

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

                                queryString += GetJointureSqlText(queryFrom, parameterSet, scriptVariableSet, log);

                                index++;
                            }
                        }
                        if (query.WhereClause != null)
                        {
                            queryString += " where ";
                            String expression = _scope?.Interpreter.Interprete(query.WhereClause, scriptVariableSet, log) ?? "";
                            queryString += expression;
                        }
                    }
                    break;
                // Delete
                case DbQueryKind.Delete:
                    {
                        queryString = "delete from ";

                        queryString += GetTableSqlText(
                            query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                            DbDataFieldViewMode.CompleteName, query.DataModule, query.Schema);

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
                        queryString += GetTableSqlText(
                            query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                            DbDataFieldViewMode.CompleteName, query.DataModule, query.Schema);
                        queryString += " (";
                        index = 0;
                        foreach (DbField field in query.Fields)
                        {
                            if (index > 0)
                                queryString += ",";

                            queryString += GetFieldSqlText(
                                field, parameterSet, log, DbDataFieldViewMode.OnlyName,
                                scriptVariableSet, query.DataModule, query.Schema);

                            index++;
                        }
                        queryString += ") values (";
                        if (query.Fields.Count > 0)
                        {
                            index = 0;
                            foreach (DbField field in query.Fields)
                            {
                                if (index > 0)
                                    queryString += ",";

                                queryString += GetFieldSqlText(
                                    field, parameterSet, log, DbDataFieldViewMode.OnlyValue,
                                    scriptVariableSet, query.DataModule, query.Schema);

                                index++;
                            }
                        }
                        queryString += ")";
                    }
                    break;
            }

            return log;
        }

        #endregion
    }
}
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Data.Queries;
using BindOpen.Framework.Databases.Data.Queries.Builders;
using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Databases.PostgreSql.Data.Queries.Builders
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
        /// Builds the specified basic  database data query and put the result
        /// into the specified string MS Sql Server query.
        /// <remarks>We assume the query already exits.</remarks>
        /// </summary>
        /// <param name="query"></param>
        /// <param name="log">The log to consider.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="queryString"></param>
        /// <returns>Returns the built query text.</returns>
        protected override string Build(
            IBasicDbQuery query,
            IBdoLog log = null,
            IDataElementSet parameterSet = null,
            IBdoScriptVariableSet scriptVariableSet = null)
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

                                queryString += GetFieldSqlText(
                                    field, parameterSet, log, DbDataFieldViewMode.CompleteNameAsAlias, scriptVariableSet,
                                    query.DataModule, query.Schema
                                );

                                index++;
                            }
                        }
                        else
                        {
                            queryString += " * ";
                        }
                        queryString += " from ";
                        if ((query.FromStatements == null) | (query.FromStatements.Count == 0))
                        {
                            queryString += GetTableSqlText(
                                query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                                DbDataFieldViewMode.CompleteName,
                                query.DataModule, query.Schema);
                        }
                        else
                        {
                            index = 0;
                            foreach (DbQueryFromStatement queryFrom in query.FromStatements)
                            {
                                if (index > 0)
                                    queryString += ",";

                                queryString += GetJointureSqlText(queryFrom, parameterSet, scriptVariableSet, log);

                                index++;
                            }
                        }
                        if (query.IdFields.Count > 0)
                        {
                            queryString += " where ";
                            index = 0;
                            foreach (DbField field in query.IdFields)
                            {
                                if (index > 0)
                                    queryString += " and ";
                                queryString += GetFieldSqlText(
                                    field, parameterSet, log, DbDataFieldViewMode.NameEqualsValue, scriptVariableSet,
                                    query.DataModule, query.Schema, query.DataTable
                                );

                                index++;
                            }
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
                                scriptVariableSet, query.DataModule);

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
                        if (query.IdFields.Count > 0)
                        {
                            queryString += " where ";
                            index = 0;
                            foreach (DbField field in query.IdFields)
                            {
                                if (index > 0)
                                    queryString += " and ";

                                queryString += GetFieldSqlText(
                                    field, parameterSet, log, DbDataFieldViewMode.NameEqualsValue,
                                    scriptVariableSet
                                );

                                index++;
                            }
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
                        if (query.IdFields.Count > 0)
                        {
                            queryString += " where ";
                            index = 0;
                            foreach (DbField field in query.IdFields)
                            {
                                if (index > 0)
                                    queryString += " and ";

                                queryString += GetFieldSqlText(
                                    field, parameterSet, log, DbDataFieldViewMode.NameEqualsValue,
                                    scriptVariableSet
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
                                field, parameterSet, log, DbDataFieldViewMode.CompleteNameAsAlias,
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
                                    scriptVariableSet);

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
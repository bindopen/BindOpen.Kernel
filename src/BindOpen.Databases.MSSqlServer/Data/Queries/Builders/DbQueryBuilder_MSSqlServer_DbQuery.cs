using BindOpen.Data.Elements;
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
        protected override string GetSqlText_Query(
            IDbSingleQuery query,
            IDataElementSet parameterSet = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            var queryString = "";
            //int index;

            //// we build the query
            //switch (query.Kind)
            //{
            //    // Select
            //    case DbQueryKind.Select:
            //        {
            //            queryString = "select ";
            //            if (query.IsDistinct)
            //                queryString += " distinct ";
            //            if (query.Limit > -1)
            //                queryString += " top " + query.Limit.ToString() + " ";
            //            index = 0;
            //            if (query.Fields?.Count > 0)
            //            {
            //                foreach (DbField field in query.Fields)
            //                {
            //                    if (index > 0)
            //                        queryString += ",";

            //                    queryString += GetSqlText_Field(
            //                        field, parameterSet, log, DbFieldViewMode.CompleteNameAsAlias, scriptVariableSet, query.DataModule, query.Schema
            //                    );

            //                    index++;
            //                }
            //            }
            //            else
            //            {
            //                queryString += " * ";
            //            }

            //            if (query.FromClause != null)
            //            {
            //                queryString += " from ";

            //                queryString += GetSqlText_From(query.FromClause, query, parameterSet, scriptVariableSet, log);

            //                index++;
            //            }

            //            if (query.IdFields?.Count > 0)
            //            {
            //                queryString += " where ";
            //                index = 0;
            //                foreach (DbField field in query.IdFields)
            //                {
            //                    if (index > 0)
            //                        queryString += " and ";
            //                    queryString += GetSqlText_Field(
            //                        field, parameterSet, log, DbFieldViewMode.NameEqualsValue, scriptVariableSet,
            //                        query.DataModule, query.Schema, query.DataTable
            //                    );

            //                    index++;
            //                }
            //            }
            //            else if (query.WhereClause != null && query.WhereClause.Text != null)
            //            {
            //                queryString += " where ";
            //                string expression = _scope?.Interpreter.Interprete(query.WhereClause, scriptVariableSet, log) ?? "";
            //                queryString += expression;
            //            }
            //            if (query.GroupByClause != null)
            //            {
            //                queryString += " group by ";
            //                index = 0;
            //                foreach (DbField field in query.GroupByClause.Fields)
            //                {
            //                    if (index > 0)
            //                        queryString += ", ";

            //                    queryString += GetSqlText_Field(
            //                        field, parameterSet, log, DbFieldViewMode.CompleteNameAsAlias, scriptVariableSet,
            //                        query.DataModule, query.Schema, query.DataTable
            //                    );

            //                    index++;
            //                }
            //            }
            //            if (query.HavingClause != null)
            //            {
            //                queryString += " having ";
            //                string expression = _scope?.Interpreter.Interprete(query.WhereClause, scriptVariableSet, log) ?? "";
            //                queryString += expression;
            //            }
            //            if (query.OrderByClause != null)
            //            {
            //                queryString += " order by ";
            //                index = 0;
            //                foreach (DbQueryOrderByStatement statement in query.OrderByClause.Statements)
            //                {
            //                    if (index > 0)
            //                        queryString += ", ";
            //                    if (statement.Sorting == DataSortingMode.Random)
            //                    {
            //                        queryString += "newid()";
            //                    }
            //                    else
            //                    {
            //                        queryString += GetSqlText_Field(
            //                            statement.Field,
            //                            parameterSet,
            //                            log,
            //                            DbFieldViewMode.OnlyName,
            //                            scriptVariableSet);

            //                        switch (statement.Sorting)
            //                        {
            //                            case DataSortingMode.Ascending:
            //                                queryString += " asc";
            //                                break;
            //                            case DataSortingMode.Descending:
            //                                queryString += " desc";
            //                                break;
            //                        }
            //                    }
            //                    index++;
            //                }
            //            }
            //        }
            //        break;
            //    // Update
            //    case DbQueryKind.Update:
            //        {
            //            queryString = "update ";
            //            queryString += GetSqlText_Table(
            //                query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
            //                DbFieldViewMode.CompleteNameAsAlias, query.DataModule, query.Schema);
            //            queryString += " set ";
            //            index = 0;
            //            foreach (DbField field in query.Fields)
            //            {
            //                if (index > 0)
            //                    queryString += ",";

            //                queryString += GetSqlText_Field(field, parameterSet, log, DbFieldViewMode.NameEqualsValue, scriptVariableSet);

            //                index++;
            //            }
            //            if (query.ReturnedIdFields?.Count > 0)
            //            {
            //                queryString += " output inserted ";
            //                index = 0;
            //                foreach (DbField field in query.ReturnedIdFields)
            //                {
            //                    if (index > 0)
            //                        queryString += ", ";
            //                    queryString += GetSqlText_Field(
            //                        field, parameterSet, log, DbFieldViewMode.NameEqualsValue, scriptVariableSet,
            //                        query.DataModule, query.Schema, query.DataTable
            //                    );

            //                    index++;
            //                }
            //            }
            //            if (query.FromClause != null)
            //            {
            //                queryString += " from ";

            //                queryString += GetSqlText_From(query.FromClause, query, parameterSet, scriptVariableSet, log);

            //                index++;
            //            }

            //            if (query.IdFields?.Count > 0)
            //            {
            //                queryString += " where ";
            //                index = 0;
            //                foreach (DbField field in query.IdFields)
            //                {
            //                    if (index > 0)
            //                        queryString += " and ";

            //                    queryString += GetSqlText_Field(
            //                        field, parameterSet, log, DbFieldViewMode.NameEqualsValue,
            //                        scriptVariableSet
            //                    );

            //                    index++;
            //                }
            //            }
            //            else if (query.WhereClause != null)
            //            {
            //                queryString += " where ";
            //                string expression = _scope?.Interpreter.Interprete(query.WhereClause, scriptVariableSet, log) ?? "";
            //                queryString += expression;
            //            }
            //        }
            //        break;
            //    // Delete
            //    case DbQueryKind.Delete:
            //        {
            //            queryString = "delete";
            //            if (query.ReturnedIdFields?.Count > 0)
            //            {
            //                queryString += " output deleted ";
            //                index = 0;
            //                foreach (DbField field in query.ReturnedIdFields)
            //                {
            //                    if (index > 0)
            //                        queryString += ", ";
            //                    queryString += GetSqlText_Field(
            //                        field, parameterSet, log, DbFieldViewMode.NameEqualsValue, scriptVariableSet,
            //                        query.DataModule, query.Schema, query.DataTable
            //                    );

            //                    index++;
            //                }
            //            }
            //            if (query.FromClause != null)
            //            {
            //                queryString += " from ";

            //                queryString += GetSqlText_From(query.FromClause, query, parameterSet, scriptVariableSet, log);
            //            }
            //            else
            //            {
            //                queryString += GetSqlText_Table(
            //                    query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
            //                    DbFieldViewMode.CompleteName, query.DataModule, query.Schema);
            //            }

            //            if (query.IdFields?.Count > 0)
            //            {
            //                queryString += " where ";
            //                index = 0;
            //                foreach (DbField field in query.IdFields)
            //                {
            //                    if (index > 0)
            //                        queryString += " and ";

            //                    queryString += GetSqlText_Field(
            //                        field, parameterSet, log, DbFieldViewMode.NameEqualsValue,
            //                        scriptVariableSet
            //                    );

            //                    index++;
            //                }
            //            }
            //            else if (query.WhereClause != null)
            //            {
            //                queryString += " where ";
            //                string expression = _scope?.Interpreter.Interprete(query.WhereClause, scriptVariableSet, log) ?? "";
            //                queryString += expression;
            //            }
            //        }
            //        break;
            //    // Insert
            //    case DbQueryKind.Insert:
            //        {
            //            queryString = "insert into ";
            //            queryString += GetSqlText_Table(
            //                query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
            //                DbFieldViewMode.CompleteName, query.DataModule, query.Schema);
            //            queryString += " (";
            //            index = 0;
            //            foreach (DbField field in query.Fields)
            //            {
            //                if (index > 0)
            //                    queryString += ",";

            //                queryString += GetSqlText_Field(
            //                    field, parameterSet, log, DbFieldViewMode.OnlyName,
            //                    scriptVariableSet, query.DataModule, query.Schema);

            //                index++;
            //            }
            //            queryString += ")";
            //            if (query.ReturnedIdFields?.Count > 0)
            //            {
            //                queryString += " output inserted ";
            //                index = 0;
            //                foreach (DbField field in query.ReturnedIdFields)
            //                {
            //                    if (index > 0)
            //                        queryString += ", ";
            //                    queryString += GetSqlText_Field(
            //                        field, parameterSet, log, DbFieldViewMode.NameEqualsValue, scriptVariableSet,
            //                        query.DataModule, query.Schema, query.DataTable
            //                    );

            //                    index++;
            //                }
            //            }
            //            queryString += " values (";
            //            if (query.Fields?.Count > 0)
            //            {
            //                index = 0;
            //                foreach (DbField field in query.Fields)
            //                {
            //                    if (index > 0)
            //                        queryString += ",";

            //                    queryString += GetSqlText_Field(
            //                        field, parameterSet, log, DbFieldViewMode.OnlyValue,
            //                        scriptVariableSet, query.DataModule, query.Schema);

            //                    index++;
            //                }
            //            }
            //            queryString += ")";
            //        }
            //        break;
            //}

            return queryString;
        }

        #endregion
    }
}
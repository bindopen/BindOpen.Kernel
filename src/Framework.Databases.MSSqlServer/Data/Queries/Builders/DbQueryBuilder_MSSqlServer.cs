using System;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Data.Queries;
using BindOpen.Framework.Databases.Data.Queries.Builders;
using BindOpen.Framework.Databases.Extensions.Carriers;
using BindOpen.Framework.Databases.Extensions.Runtime.Connectors;

namespace BindOpen.Framework.Databases.MSSqlServer.Data.Queries.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_MSSqlServer : DbQueryBuilder
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryBuilder_MSSqlServer class.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        public DbQueryBuilder_MSSqlServer(
            IAppScope appScope = null)
            : base(DatabaseConnectorKind.MSSqlServer, appScope)
        {
        }

        #endregion

        // ------------------------------------------
        // BASIC QUERY BUILBING
        // ------------------------------------------

        #region Basic Query Building

        private string GetFieldSqlText(
            DbField field,
            Log log,
            DbDataFieldViewMode viewMode = DbDataFieldViewMode.CompleteName,
            ScriptVariableSet scriptVariableSet = null,
            string defaultDataModule = null,
            string defaultSchema = null,
            string defaultDataTable = null)
        {
            log = log ?? new Log();

            string queryString = "";

            if (field!=null)
            {
                switch (viewMode)
                {
                    case DbDataFieldViewMode.CompleteName:
                    case DbDataFieldViewMode.CompleteNameOrValue:
                    case DbDataFieldViewMode.CompleteNameAsAlias:
                        if (field.IsAll)
                        {
                            string tableName = this.GetTableSqlText(
                                field.DataModule,
                                field.Schema,
                                field.DataTable,
                                field.DataTableAlias,
                                log,
                                viewMode,
                                scriptVariableSet,
                                defaultDataModule,
                                defaultSchema);

                            if (!string.IsNullOrEmpty(tableName))
                            {
                                queryString += tableName + ".";
                            }

                            queryString += "*";
                        }
                        else
                        {
                            if ((viewMode != DbDataFieldViewMode.CompleteNameOrValue) || (field.Value == null))
                            {
                                string tableName = this.GetTableSqlText(
                                    field.DataModule,
                                    field.Schema,
                                    field.DataTable ?? defaultDataTable,
                                    field.DataTableAlias,
                                    log,
                                    viewMode,
                                    scriptVariableSet,
                                    defaultDataModule,
                                    defaultSchema);

                                if (!string.IsNullOrEmpty(tableName))
                                {
                                    queryString += tableName + ".";
                                }

                                queryString += this.GetFieldSqlText(
                                    field,
                                    log,
                                    viewMode == DbDataFieldViewMode.CompleteNameAsAlias ?
                                        DbDataFieldViewMode.OnlyNameAsAlias :
                                        DbDataFieldViewMode.OnlyName,
                                    scriptVariableSet,
                                    defaultDataModule,
                                    defaultSchema,
                                    defaultDataTable);
                            }
                            else
                            {
                                queryString += this.GetFieldSqlText(
                                    field,
                                    log,
                                    DbDataFieldViewMode.OnlyValue,
                                    scriptVariableSet,
                                    defaultDataModule,
                                    defaultSchema,
                                    defaultDataTable);

                                if (viewMode == DbDataFieldViewMode.CompleteNameAsAlias && !string.IsNullOrEmpty(field.Alias))
                                    queryString += " as [" + field.Alias + "]";
                            }
                        }
                        break;
                    case DbDataFieldViewMode.OnlyName:
                        if (!string.IsNullOrEmpty(field.Alias))
                        {
                            queryString += "[" + field.Alias + "]";
                        }
                        else if (field.IsNameAsScript)
                        {
                            String name = this._appScope?.ScriptInterpreter.Interprete(DataExpression.CreateScript(field.Name), scriptVariableSet, log) ?? "";
                            queryString += "[" + name + "]";
                        }
                        else
                        {
                            queryString += "[" + field.Name + "]";
                        }
                        break;
                    case DbDataFieldViewMode.OnlyNameAsAlias:
                        if (field.IsNameAsScript)
                        {
                            String name = this._appScope?.ScriptInterpreter.Interprete(DataExpression.CreateScript(field.Name), scriptVariableSet, log) ?? "";
                            queryString += "[" + name + "]";
                        }
                        else
                        {
                            queryString += "[" + field.Name + "]";
                        }
                        if ((field.Alias != null) & (field.Alias != ""))
                            queryString += " as [" + field.Alias + "]";

                        break;
                    case DbDataFieldViewMode.OnlyValue:
                        String value = this._appScope?.ScriptInterpreter.Interprete(field.Value, scriptVariableSet, log) ?? "";

                        if (field.Query !=null)
                        {
                            String subQueryText = "";
                            if (field.Query is BasicDbDataQuery)
                                this.Build(field.Query as BasicDbDataQuery, scriptVariableSet, out subQueryText);
                            else if (field.Query is AdvancedDbDataQuery)
                                this.Build(field.Query as AdvancedDbDataQuery, scriptVariableSet, out subQueryText);

                            queryString += "(" + subQueryText + ")";
                        }
                        else if ((field.ValueType == DataValueType.Number)
                            || (field.ValueType == DataValueType.Integer)
                            || (field.ValueType == DataValueType.None)
                            || (field.ValueType == DataValueType.Any))
                        {
                            queryString += (value.Trim()?.Length == 0 ? "null" : value);
                        }
                        else
                        {
                            queryString += "'" + value.Replace("'", "''") + "'";
                        }
                        break;
                    case DbDataFieldViewMode.NameEqualsValue:
                        queryString += this.GetFieldSqlText(
                            field,
                            log,
                            DbDataFieldViewMode.CompleteName,
                            scriptVariableSet,
                            defaultDataModule,
                            defaultSchema,
                            defaultDataTable);

                        queryString += "=";

                        queryString += this.GetFieldSqlText(
                            field,
                            log,
                            DbDataFieldViewMode.OnlyValue,
                            scriptVariableSet,
                            defaultDataModule,
                            defaultSchema,
                            defaultDataTable);

                        break;
                    default:
                        break;
                }
            }

            return queryString;
        }

        private string GetTableSqlText(
            DbTable table,
            Log log,
            DbDataFieldViewMode viewMode = DbDataFieldViewMode.CompleteName,
            ScriptVariableSet scriptVariableSet = null,
            string defaultDataModule = null,
            string defaultSchema = null)
        {
            log = log ?? new Log();

            if (table != null)
            {
                return this.GetTableSqlText(
                   table.DataModule,
                   table.Schema,
                   table.Name,
                   table.Alias,
                   log,
                   viewMode,
                   scriptVariableSet,
                   defaultDataModule,
                   defaultSchema);
            }

            return "";
        }

        private string GetTableSqlText(
            string tableDataModule,
            string tableSchema,
            string tableName,
            string tableAlias,
            Log log,
            DbDataFieldViewMode viewMode = DbDataFieldViewMode.CompleteName,
            ScriptVariableSet scriptVariableSet = null,
            string defaultDataModule = null,
            string defaultSchema = null)
        {
            log = log ?? new Log();

            string queryString = "";

            if ((viewMode == DbDataFieldViewMode.CompleteName) && (!string.IsNullOrEmpty(tableAlias)))
            {
                queryString += "[" + tableAlias + "]";
            }
            else if (!string.IsNullOrEmpty(tableName))
            {
                if ((viewMode == DbDataFieldViewMode.CompleteName) || (viewMode == DbDataFieldViewMode.CompleteNameAsAlias))
                {
                    if (string.IsNullOrEmpty(tableDataModule))
                        tableDataModule = defaultDataModule;

                    if (!string.IsNullOrEmpty(tableDataModule))
                    {
                        string databaseName = this.GetDatabaseName(tableDataModule);
                        if (databaseName?.Length > 0)
                            queryString += "[" + databaseName + "].";
                    }

                    if (string.IsNullOrEmpty(tableSchema))
                        tableSchema = defaultSchema;

                    if (!string.IsNullOrEmpty(tableSchema))
                    {
                        queryString += "[" + tableSchema + "].";
                    }
                }

                queryString += "[" + tableName + "]";

                if ((viewMode == DbDataFieldViewMode.CompleteNameAsAlias)
                    && (!string.IsNullOrEmpty(tableAlias)))
                {
                    queryString += " as [" + tableAlias + "]";
                }
            }

            return queryString;
        }

        private string GetJointureSqlText(
            DbDataQueryFromStatement queryFrom,
            ScriptVariableSet scriptVariableSet,
            Log log)
        {
            log = log ?? new Log();

            String queryString = "";
            foreach (DbDataQueryJointureStatement queryJointure in queryFrom.JointureStatements)
            {
                switch (queryJointure.Kind)
                {
                    case DbDataQueryJointureKind.Inner:
                        {
                            queryString += " inner join ";
                            break;
                        }
                    case DbDataQueryJointureKind.Left:
                        {
                            queryString += " left join ";
                            break;
                        }
                    case DbDataQueryJointureKind.Right:
                        {
                            queryString += " right join ";
                            break;
                        }
                    case DbDataQueryJointureKind.Union:
                        {
                            queryString += " inner join ";
                            break;
                        }
                }
                queryString += this.GetTableSqlText(queryJointure.Table, log, DbDataFieldViewMode.CompleteNameAsAlias);

                if (queryJointure.Kind != DbDataQueryJointureKind.None)
                {
                    queryString += " on ";
                    String expression = this._appScope?.ScriptInterpreter.Interprete(queryJointure.Condition, scriptVariableSet, log) ?? String.Empty;
                    queryString += expression;
                }
            }
            if (queryFrom.UnionStatement?.DataQuery != null)
            {
                switch (queryFrom.UnionStatement.Type)
                {
                    case DbDataQueryUnionKind.Union:
                        {
                            queryString += " union ";
                            break;
                        }
                }
                String subQuery = String.Empty;
                log.AddEvents(
                    this.BuildQuery(queryFrom.UnionStatement.DataQuery, scriptVariableSet, out subQuery));
                queryString += "(" + subQuery + ")";
            }

            return queryString;
        }

        /// <summary>
        /// Builds the specified simple database data query and put the result
        /// into the specified string MS Sql Server query.
        /// <remarks>We assume the query already exits.</remarks>
        /// </summary>
        /// <param name="query"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="queryString"></param>
        protected override Log Build(
            BasicDbDataQuery query,
            ScriptVariableSet scriptVariableSet,
            out String queryString)
        {
            ILog log = new Log();

            int index = 0;

            queryString = "";

            // we build the query
            switch (query.Kind)
            {
                // Select
                case DbDataQueryKind.Select:
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

                                queryString += this.GetFieldSqlText(
                                    field, log, DbDataFieldViewMode.CompleteNameAsAlias, scriptVariableSet,
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
                        if ((query.FromClauses == null) | (query.FromClauses.Count == 0))
                        {
                            queryString += this.GetTableSqlText(
                                query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                                log, DbDataFieldViewMode.CompleteName, scriptVariableSet,
                                query.DataModule, query.Schema);
                        }
                        else
                        {
                            index = 0;
                            foreach (DbDataQueryFromStatement queryFrom in query.FromClauses)
                            {
                                if (index > 0)
                                    queryString += ",";

                                queryString += this.GetJointureSqlText(queryFrom, scriptVariableSet, log);

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
                                queryString += this.GetFieldSqlText(
                                    field, log, DbDataFieldViewMode.NameEqualsValue, scriptVariableSet,
                                    query.DataModule, query.Schema, query.DataTable
                                );

                                index++;
                            }
                        }
                        if (query.OrderByStatements.Count > 0)
                        {
                            queryString += " order by ";
                            index = 0;
                            foreach (DbDataQueryOrderByStatement queryOrderByStatement in query.OrderByStatements)
                            {
                                if (index > 0)
                                    queryString += ", ";
                                if (queryOrderByStatement.Sorting == DataSortingMode.Random)
                                {
                                    queryString += "newid()";
                                }
                                else
                                {
                                    queryString += this.GetFieldSqlText(
                                        queryOrderByStatement.Field,
                                        log,
                                        DbDataFieldViewMode.OnlyName,
                                        scriptVariableSet);

                                    switch(queryOrderByStatement.Sorting)
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
                case DbDataQueryKind.Update:
                    {
                        queryString = "update ";
                        queryString += this.GetTableSqlText(
                            query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                            log, DbDataFieldViewMode.CompleteName, scriptVariableSet, query.Schema, query.DataTable);
                        queryString += " set ";
                        index = 0;
                        foreach (DbField field in query.Fields)
                        {
                            if (index > 0)
                                queryString += ",";

                            queryString += this.GetFieldSqlText(
                                field, log, DbDataFieldViewMode.NameEqualsValue,
                                scriptVariableSet, query.DataModule);

                            index++;
                        }
                        if (query.FromClauses?.Count > 0)
                        {
                            queryString += " from ";
                            index = 0;
                            foreach (DbDataQueryFromStatement queryFrom in query.FromClauses)
                            {
                                if (index > 0)
                                    queryString += ",";

                                queryString += this.GetJointureSqlText(queryFrom, scriptVariableSet, log);

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

                                queryString += this.GetFieldSqlText(
                                    field, log, DbDataFieldViewMode.NameEqualsValue,
                                    scriptVariableSet
                                );

                                index++;
                            }
                        }
                    }
                    break;
                // Delete
                case DbDataQueryKind.Delete:
                    {
                        queryString = "delete from ";
                        queryString += this.GetTableSqlText(
                            query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                            log, DbDataFieldViewMode.CompleteName, scriptVariableSet,
                            query.DataModule, query.Schema);
                        if (query.IdFields.Count > 0)
                        {
                            queryString += " where ";
                            index = 0;
                            foreach (DbField field in query.IdFields)
                            {
                                if (index > 0)
                                    queryString += " and ";

                                queryString += this.GetFieldSqlText(
                                    field, log, DbDataFieldViewMode.NameEqualsValue,
                                    scriptVariableSet
                                );

                                index++;
                            }
                        }
                    }
                    break;
                // Insert
                case DbDataQueryKind.Insert:
                    {
                        queryString = "insert into ";
                        queryString += this.GetTableSqlText(
                            query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                            log, DbDataFieldViewMode.CompleteName, scriptVariableSet, query.DataModule, query.Schema);
                        queryString += " (";
                        index = 0;
                        foreach (DbField field in query.Fields)
                        {
                            if (index > 0)
                                queryString += ",";

                            queryString += this.GetFieldSqlText(
                                field, log, DbDataFieldViewMode.CompleteNameAsAlias,
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

                                queryString += this.GetFieldSqlText(
                                    field, log, DbDataFieldViewMode.OnlyValue,
                                    scriptVariableSet);

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
        protected override Log Build(
            AdvancedDbDataQuery query,
            ScriptVariableSet scriptVariableSet,
            out String queryString)
        {
            ILog log = new Log();

            int index = 0;
            queryString = "";

            // we build the query
            switch (query.Kind)
            {
                // Select
                case DbDataQueryKind.Select:
                    {
                        queryString = "select ";
                        if (query.IsDistinct)
                            queryString += " distinct ";
                        if (query.Top > -1)
                            queryString += " top " + query.Top.ToString() + " ";
                        index = 0;
                        if (query.Fields?.Count>0)
                        {
                            foreach (DbField field in query.Fields)
                            {
                                if (index > 0)
                                    queryString += ",";

                                queryString += this.GetFieldSqlText(
                                    field, log, DbDataFieldViewMode.CompleteNameAsAlias, scriptVariableSet, query.DataModule, query.Schema
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

                        foreach (DbDataQueryFromStatement queryFrom in query.FromClauses)
                        {
                            if (index > 0)
                                queryString += ",";

                            queryString += this.GetJointureSqlText(queryFrom, scriptVariableSet, log);

                            index++;
                        }


                        if (query.WhereClause != null && query.WhereClause.Text != null)
                        {
                            queryString += " where ";
                            String expression = this._appScope?.ScriptInterpreter.Interprete(query.WhereClause, scriptVariableSet, log) ?? "";
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

                                queryString += this.GetFieldSqlText(
                                    field, log, DbDataFieldViewMode.CompleteNameAsAlias, scriptVariableSet,
                                    query.DataModule, query.Schema, query.DataTable
                                );

                                index++;
                            }
                        }
                        if (query.HavingClause != null)
                        {
                            queryString += " having ";
                            String expression = this._appScope?.ScriptInterpreter.Interprete(query.WhereClause, scriptVariableSet, log) ?? "";
                            queryString += expression;
                        }
                        if (query.OrderByStatements.Count > 0)
                        {
                            queryString += " order by ";
                            index = 0;
                            foreach (DbDataQueryOrderByStatement queryOrderByStatement in query.OrderByStatements)
                            {
                                if (index > 0)
                                    queryString += ", ";
                                if (queryOrderByStatement.Sorting == DataSortingMode.Random)
                                {
                                    queryString += "newid()";
                                }
                                else
                                {
                                    queryString += this.GetFieldSqlText(
                                        queryOrderByStatement.Field,
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
                case DbDataQueryKind.Update:
                    {
                        queryString = "update ";
                        queryString += this.GetTableSqlText(
                            query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                            log, DbDataFieldViewMode.CompleteName, scriptVariableSet,
                            query.DataModule, query.Schema);
                        queryString += " set ";
                        index = 0;
                        foreach (DbField field in query.Fields)
                        {
                            if (index > 0)
                                queryString += ",";

                            queryString += this.GetFieldSqlText(
                                field, log, DbDataFieldViewMode.NameEqualsValue,
                                scriptVariableSet);

                            index++;
                        }
                        if (query.FromClauses?.Count > 0)
                        {
                            queryString += " from ";
                            index = 0;
                            foreach (DbDataQueryFromStatement queryFrom in query.FromClauses)
                            {
                                if (index > 0)
                                    queryString += ",";

                                queryString += this.GetJointureSqlText(queryFrom, scriptVariableSet, log);

                                index++;
                            }
                        }
                        if (query.WhereClause != null)
                        {
                            queryString += " where ";
                            String expression = this._appScope?.ScriptInterpreter.Interprete(query.WhereClause, scriptVariableSet, log) ?? "";
                            queryString += expression;
                        }
                    }
                    break;
                // Delete
                case DbDataQueryKind.Delete:
                    {
                        queryString = "delete from ";

                        queryString += this.GetTableSqlText(
                            query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                            log, DbDataFieldViewMode.CompleteName, scriptVariableSet,
                            query.DataModule, query.Schema);

                        if (query.WhereClause != null)
                        {
                            queryString += " where ";
                            string expression = this._appScope?.ScriptInterpreter.Interprete(query.WhereClause, scriptVariableSet, log) ?? "";
                            queryString += expression;
                        }
                    }
                    break;
                // Insert
                case DbDataQueryKind.Insert:
                    {
                        queryString = "insert into ";
                        queryString += this.GetTableSqlText(
                            query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                            log, DbDataFieldViewMode.CompleteName, scriptVariableSet,
                            query.DataModule, query.Schema);
                        queryString += " (";
                        index = 0;
                        foreach (DbField field in query.Fields)
                        {
                            if (index > 0)
                                queryString += ",";

                            queryString += this.GetFieldSqlText(
                                field, log, DbDataFieldViewMode.OnlyName,
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

                                queryString += this.GetFieldSqlText(
                                    field, log, DbDataFieldViewMode.OnlyValue,
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
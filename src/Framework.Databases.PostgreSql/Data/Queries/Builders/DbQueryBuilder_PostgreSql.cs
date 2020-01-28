using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Data.Expression;
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
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryBuilder_PostgreSql class.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        public DbQueryBuilder_PostgreSql(
            IBdoScope scope = null)
            : base(scope)
        {
            Id = "databases.postgresql$client";
        }

        #endregion

        // ------------------------------------------
        // BASIC QUERY BUILBING
        // ------------------------------------------

        #region Basic Query Building

        private string GetFieldSqlText(
            DbField field,
            IDataElementSet parameterSet,
            IBdoLog log,
            DbDataFieldViewMode viewMode = DbDataFieldViewMode.CompleteName,
            IBdoScriptVariableSet scriptVariableSet = null,
            string defaultDataModule = null,
            string defaultSchema = null,
            string defaultDataTable = null)
        {
            string queryString = "";

            if (field != null)
            {
                switch (viewMode)
                {
                    case DbDataFieldViewMode.CompleteName:
                    case DbDataFieldViewMode.CompleteNameOrValue:
                    case DbDataFieldViewMode.CompleteNameAsAlias:
                        if (field.IsAll)
                        {
                            string tableName = GetTableSqlText(
                                field.DataModule,
                                field.Schema,
                                field.DataTable,
                                field.DataTableAlias,
                                viewMode,
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
                                string tableName = GetTableSqlText(
                                    field.DataModule,
                                    field.Schema,
                                    field.DataTable ?? defaultDataTable,
                                    field.DataTableAlias,
                                    viewMode,
                                    defaultDataModule,
                                    defaultSchema);

                                if (!string.IsNullOrEmpty(tableName))
                                {
                                    queryString += tableName + ".";
                                }

                                queryString += GetFieldSqlText(
                                    field,
                                    parameterSet,
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
                                queryString += GetFieldSqlText(
                                    field,
                                    parameterSet,
                                    log,
                                    DbDataFieldViewMode.OnlyValue,
                                    scriptVariableSet,
                                    defaultDataModule,
                                    defaultSchema,
                                    defaultDataTable);

                                if (viewMode == DbDataFieldViewMode.CompleteNameAsAlias && !string.IsNullOrEmpty(field.Alias))
                                {
                                    queryString += " as " + GetSqlText_Field(field.Alias);
                                }
                            }
                        }
                        break;
                    case DbDataFieldViewMode.OnlyName:
                        if (!string.IsNullOrEmpty(field.Alias))
                        {
                            queryString += GetSqlText_Field(field.Alias);
                        }
                        else if (field.IsNameAsScript)
                        {
                            string name = _scope?.Interpreter.Interprete(field.Name.CreateScript(), scriptVariableSet, log) ?? "";
                            queryString += GetSqlText_Field(name);
                        }
                        else
                        {
                            queryString += GetSqlText_Field(field.Name);
                        }
                        break;
                    case DbDataFieldViewMode.OnlyNameAsAlias:
                        if (field.IsNameAsScript)
                        {
                            string name = _scope?.Interpreter.Interprete(field.Name.CreateScript(), scriptVariableSet, log) ?? "";
                            queryString += GetSqlText_Field(name);
                        }
                        else
                        {
                            queryString += GetSqlText_Field(field.Name);
                        }
                        if (!string.IsNullOrEmpty(field.Alias))
                        {
                            queryString += " as " + GetSqlText_Field(field.Alias);
                        }

                        break;
                    case DbDataFieldViewMode.OnlyValue:
                        string value = _scope?.Interpreter.Interprete(field.Value, scriptVariableSet, log) ?? "";

                        if (field.Query != null)
                        {
                            string subQueryText = "";
                            if (field.Query is BasicDbQuery)
                                subQueryText = Build(field.Query as BasicDbQuery, log, parameterSet, scriptVariableSet);
                            else if (field.Query is AdvancedDbQuery)
                                subQueryText = Build(field.Query as AdvancedDbQuery, log, parameterSet, scriptVariableSet);

                            queryString += "(" + subQueryText + ")";
                        }
                        else
                        {
                            queryString += GetValuedSqlText(value, field.ValueType);
                        }
                        break;
                    case DbDataFieldViewMode.NameEqualsValue:
                        queryString += GetFieldSqlText(
                            field,
                            parameterSet,
                            log,
                            DbDataFieldViewMode.CompleteName,
                            scriptVariableSet,
                            defaultDataModule,
                            defaultSchema,
                            defaultDataTable);

                        queryString += "=";

                        queryString += GetFieldSqlText(
                            field,
                            parameterSet,
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
            DbDataFieldViewMode viewMode = DbDataFieldViewMode.CompleteName,
            string defaultDataModule = null,
            string defaultSchema = null)
        {
            if (table != null)
            {
                return GetTableSqlText(
                   table.DataModule,
                   table.Schema,
                   table.Name,
                   table.Alias,
                   viewMode,
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
            DbDataFieldViewMode viewMode = DbDataFieldViewMode.CompleteName,
            string defaultDataModule = null,
            string defaultSchema = null)
        {
            string queryString = "";

            if ((viewMode == DbDataFieldViewMode.CompleteName) && (!string.IsNullOrEmpty(tableAlias)))
            {
                queryString += GetSqlText_Table(tableAlias);
            }
            else if (!string.IsNullOrEmpty(tableName))
            {
                if ((viewMode == DbDataFieldViewMode.CompleteName) || (viewMode == DbDataFieldViewMode.CompleteNameAsAlias))
                {
                    if (string.IsNullOrEmpty(tableDataModule))
                        tableDataModule = defaultDataModule;

                    if (!string.IsNullOrEmpty(tableDataModule))
                    {
                        string databaseName = GetDatabaseName(tableDataModule);
                        if (databaseName?.Length > 0)
                        {
                            queryString += GetSqlText_Table(databaseName) + ".";
                        }
                    }

                    if (string.IsNullOrEmpty(tableSchema))
                    {
                        tableSchema = defaultSchema;
                    }

                    if (!string.IsNullOrEmpty(tableSchema))
                    {
                        queryString += GetSqlText_Schema(tableSchema) + ".";
                    }
                }

                queryString += GetSqlText_Table(tableName);

                if ((viewMode == DbDataFieldViewMode.CompleteNameAsAlias)
                    && (!string.IsNullOrEmpty(tableAlias)))
                {
                    queryString += " as " + GetSqlText_Table(tableAlias);
                }
            }

            return queryString;
        }

        private string GetJoinSqlText(
            IDbQueryFromStatement queryFrom,
            IDbQuery query,
            IDataElementSet parameterSet,
            IBdoScriptVariableSet scriptVariableSet,
            IBdoLog log)
        {
            string queryString = "";
            foreach (DbQueryJoinStatement queryJoin in queryFrom.JoinStatements)
            {
                switch (queryJoin.Kind)
                {
                    case DbQueryJoinKind.Inner:
                        {
                            queryString += " inner join ";
                            break;
                        }
                    case DbQueryJoinKind.Left:
                        {
                            queryString += " left join ";
                            break;
                        }
                    case DbQueryJoinKind.Right:
                        {
                            queryString += " right join ";
                            break;
                        }
                    case DbQueryJoinKind.Union:
                        {
                            queryString += " inner join ";
                            break;
                        }
                }

                if (queryJoin.Table == null || (string.IsNullOrEmpty(queryJoin.Table.Name) && string.IsNullOrEmpty(queryJoin.Table.Alias)))
                {
                    queryJoin.Table = DbFluent.Table(query?.DataTable).WithAlias(query?.DataTableAlias);
                }

                queryString += GetTableSqlText(queryJoin.Table, DbDataFieldViewMode.CompleteNameAsAlias);

                if (queryJoin.Kind != DbQueryJoinKind.None)
                {
                    queryString += " on ";
                    string expression = _scope?.Interpreter.Interprete(queryJoin.Condition, scriptVariableSet, log) ?? String.Empty;
                    queryString += expression;
                }
            }
            if (queryFrom.UnionStatement?.Query != null)
            {
                switch (queryFrom.UnionStatement.Type)
                {
                    case DbQueryUnionKind.Union:
                        {
                            queryString += " union ";
                            break;
                        }
                }
                string subQuery = BuildSqlText(queryFrom.UnionStatement.Query, log, false, parameterSet, scriptVariableSet);
                queryString += "(" + subQuery + ")";
            }

            return queryString;
        }

        #endregion
    }
}
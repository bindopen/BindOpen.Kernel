using BindOpen.Data.Elements;
using BindOpen.Data.Expression;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Extensions.Carriers;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    internal partial class DbQueryBuilder_MSSqlServer : DbQueryBuilder
    {
        // ------------------------------------------
        // FIELDS
        // ------------------------------------------

        #region Fields

        private string GetSqlText_Field(
            DbField field,
            IDbQuery query = null,
            IDataElementSet parameterSet = null,
            DbFieldViewMode viewMode = DbFieldViewMode.CompleteName,
            string defaultDataModule = null,
            string defaultSchema = null,
            string defaultDataTable = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            string queryString = "";

            if (field != null)
            {
                switch (viewMode)
                {
                    case DbFieldViewMode.CompleteName:
                    case DbFieldViewMode.CompleteNameOrValue:
                    case DbFieldViewMode.CompleteNameAsAlias:
                        if (field.IsAll)
                        {
                            string tableName = GetSqlText_Table(
                                field.DataModule,
                                field.Schema,
                                field.DataTable,
                                field.DataTableAlias,
                                query, parameterSet,
                                viewMode,
                                defaultDataModule,
                                defaultSchema,
                                scriptVariableSet: scriptVariableSet, log: log);

                            queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(tableName), tableName + ".");

                            queryString += "*";
                        }
                        else
                        {
                            if ((viewMode != DbFieldViewMode.CompleteNameOrValue) || (field.Expression == null))
                            {
                                string tableName = GetSqlText_Table(
                                    field.DataModule,
                                    field.Schema,
                                    field.DataTable ?? defaultDataTable,
                                    field.DataTableAlias,
                                    query, parameterSet,
                                    viewMode,
                                    defaultDataModule,
                                    defaultSchema,
                                    scriptVariableSet: scriptVariableSet, log: log);

                                queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(tableName), tableName + ".");

                                queryString += GetSqlText_Field(
                                    field,
                                    query, parameterSet,
                                    viewMode == DbFieldViewMode.CompleteNameAsAlias ?
                                        DbFieldViewMode.OnlyNameAsAlias :
                                        DbFieldViewMode.OnlyName,
                                    defaultDataModule,
                                    defaultSchema,
                                    defaultDataTable,
                                    scriptVariableSet,
                                    log);
                            }
                            else
                            {
                                queryString += GetSqlText_Field(
                                    field,
                                    query, parameterSet,
                                    DbFieldViewMode.OnlyValue,
                                    defaultDataModule,
                                    defaultSchema,
                                    defaultDataTable,
                                    scriptVariableSet,
                                    log);

                                if (viewMode == DbFieldViewMode.CompleteNameAsAlias)
                                {
                                    queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(field.Alias), " as " + GetSqlText_Field(field.Alias));
                                }
                            }
                        }
                        break;
                    case DbFieldViewMode.OnlyName:
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
                    case DbFieldViewMode.OnlyNameAsAlias:
                        if (field.IsNameAsScript)
                        {
                            string name = _scope?.Interpreter.Interprete(field.Name.CreateScript(), scriptVariableSet, log) ?? "";
                            queryString += GetSqlText_Field(name);
                        }
                        else
                        {
                            queryString += GetSqlText_Field(field.Name);
                        }
                        queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(field.Alias), " as " + GetSqlText_Field(field.Alias));

                        break;
                    case DbFieldViewMode.OnlyValue:
                        string value = _scope?.Interpreter.Interprete(field.Expression, scriptVariableSet, log) ?? "";

                        if (field.Query != null)
                        {
                            string subQueryText = "";
                            if (field.Query is DbSingleQuery)
                            {
                                subQueryText = GetSqlText_Query(field.Query as DbSingleQuery, parameterSet, scriptVariableSet, log);
                            }
                            else if (field.Query is DbSingleQuery)
                            {
                                subQueryText = GetSqlText_Query(field.Query as DbSingleQuery, parameterSet, scriptVariableSet, log);
                            }
                            queryString += "(" + subQueryText + ")";
                        }
                        else
                        {
                            queryString += GetSqlText_Value(value, field.ValueType);
                        }
                        break;
                    case DbFieldViewMode.NameEqualsValue:
                        queryString += GetSqlText_Field(
                            field,
                            query, parameterSet,
                            DbFieldViewMode.CompleteName,
                            defaultDataModule,
                            defaultSchema,
                            defaultDataTable,
                            scriptVariableSet,
                            log);

                        queryString += "=";

                        queryString += GetSqlText_Field(
                            field,
                            query, parameterSet,
                            DbFieldViewMode.OnlyValue,
                            defaultDataModule,
                            defaultSchema,
                            defaultDataTable,
                            scriptVariableSet,
                            log);

                        break;
                    default:
                        break;
                }
            }

            return queryString;
        }

        #endregion

        // ------------------------------------------
        // TABLES
        // ------------------------------------------

        #region Tables

        private string GetSqlText_Table(
            DbTable table,
            IDbQuery query = null,
            IDataElementSet parameterSet = null,
            DbFieldViewMode viewMode = DbFieldViewMode.CompleteName,
            string defaultDataModule = null,
            string defaultSchema = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            var queryString = "";

            if (table?.Expression != null)
            {
                string expression = _scope?.Interpreter.Interprete(table.Expression, scriptVariableSet, log) ?? "";
                queryString += expression;
            }
            else if (table is DbJoinedTable joinedTable)
            {
                switch (joinedTable.Kind)
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
                }

                queryString += GetSqlText_Table(
                    joinedTable.Table,
                    query, parameterSet, DbFieldViewMode.CompleteNameAsAlias,
                    query.DataModule, query.Schema,
                    scriptVariableSet: scriptVariableSet, log: log);

                if (joinedTable.Kind != DbQueryJoinKind.None)
                {
                    queryString += " on ";
                    string expression = _scope?.Interpreter.Interprete(joinedTable.Condition, scriptVariableSet, log) ?? String.Empty;
                    queryString += expression;
                }
            }
            else if (table is DbDerivedTable derivedTable)
            {
                string subQuery = BuildQuery(derivedTable.Query, DbQueryParameterMode.Scripted, parameterSet, scriptVariableSet, log);
                UpdateParameterSet(query.ParameterSet, derivedTable.Query);
                queryString += "(" + subQuery + ")";

                if ((viewMode == DbFieldViewMode.CompleteName) || (viewMode == DbFieldViewMode.CompleteNameAsAlias))
                {
                    queryString += " as " + derivedTable.Alias;
                }
            }
            else if (table != null)
            {
                queryString += GetSqlText_Table(
                    table.DataModule,
                    table.Schema,
                    table.Name,
                    table.Alias,
                    query, parameterSet,
                    viewMode,
                    defaultDataModule,
                    defaultSchema,
                    scriptVariableSet: scriptVariableSet, log: log);
            }

            return queryString;
        }

        private string GetSqlText_Table(
            string tableDataModule,
            string tableSchema,
            string tableName,
            string tableAlias,
            IDbQuery query = null,
            IDataElementSet parameterSet = null,
            DbFieldViewMode viewMode = DbFieldViewMode.CompleteName,
            string defaultDataModule = null,
            string defaultSchema = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            string queryString = "";

            if ((viewMode == DbFieldViewMode.CompleteName) && (!string.IsNullOrEmpty(tableAlias)))
            {
                queryString += GetSqlText_Table(tableAlias);
            }
            else if (!string.IsNullOrEmpty(tableName))
            {
                if ((viewMode == DbFieldViewMode.CompleteName) || (viewMode == DbFieldViewMode.CompleteNameAsAlias))
                {

                    if (string.IsNullOrEmpty(tableDataModule))
                        tableDataModule = defaultDataModule;
                    if (!string.IsNullOrEmpty(tableDataModule))
                    {
                        tableDataModule = GetDatabaseName(tableDataModule);
                    }

                    if (string.IsNullOrEmpty(tableSchema))
                    {
                        tableSchema = defaultSchema;
                    }
                    string script = DbFluent.Table(tableName, tableSchema, tableDataModule);
                    queryString += _scope?.Interpreter.Interprete(script, DataExpressionKind.Script, scriptVariableSet, log) ?? String.Empty;
                }
                else
                {
                    queryString += GetSqlText_Table(tableName);
                }

                if (viewMode == DbFieldViewMode.CompleteNameAsAlias)
                {
                    queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(tableAlias), " as " + GetSqlText_Table(tableAlias));
                }
            }

            return queryString;
        }

        #endregion
    }
}
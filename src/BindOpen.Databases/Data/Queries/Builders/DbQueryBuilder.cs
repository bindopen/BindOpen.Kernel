using BindOpen.Application.Scopes;
using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Items;
using BindOpen.Data.Stores;
using BindOpen.Extensions.Scriptwords;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public abstract partial class DbQueryBuilder : IdentifiedDataItem
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// The application scope of this instance.
        /// </summary>
        protected readonly IBdoScope _scope = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryBuilder class.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        public DbQueryBuilder(IBdoScope scope = null)
        {
            _scope = scope;
        }

        #endregion

        // ------------------------------------------
        // QUERY BUILBING
        // ------------------------------------------

        #region Query Building

        /// <summary>
        /// Gets the database name corresponding to the specified data module name.
        /// </summary>
        /// <param name="dataModuleName">The data module name to consider.</param>
        /// <remarks>If not found, it returns the specified data module name.</remarks>
        protected string GetDatabaseName(string dataModuleName)
        {
            var dataSourceDepot = _scope?.DataStore?.Get<IBdoDatasourceDepot>();
            if (dataSourceDepot == null)
                return dataModuleName;
            else
            {
                var databaseName = dataSourceDepot.GetInstanceOtherwiseModuleName(dataModuleName);
                if (databaseName == StringHelper.__NoneString)
                {
                    databaseName = dataModuleName;
                }
                return databaseName;
            }
        }

        /// <summary>
        /// Updates the specified parameter set with the specified query.
        /// </summary>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="query">The query to consider.</param>
        protected void UpdateParameterSet(IDataElementSet parameterSet, IDbQuery query)
        {
            parameterSet?.Update(query?.ParameterSet);
            parameterSet?.Update(query?.ParameterSpecSet);
        }

        /// <summary>
        /// Builds the SQL text from the specified database query.
        /// </summary>
        /// <param name="query">The database data query to build.</param>
        /// <param name="parameterMode">The display mode of parameters to consider.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the built query text.</returns>
        public string BuildQuery(
            IDbQuery query,
            DbQueryParameterMode parameterMode = DbQueryParameterMode.ValueInjected,
            IDataElementSet parameterSet = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            var queryString = "";

            if (query != null)
            {
                try
                {
                    if (query is DbSingleQuery singleDbQuery)
                    {
                        (scriptVariableSet ?? (scriptVariableSet = new ScriptVariableSet())).SetValue(ScriptVariableKey_Database.DbBuilder, this);
                        queryString = GetSqlText_Query(singleDbQuery, parameterSet, scriptVariableSet, log);
                    }
                    else if (query is DbCompositeQuery compositeDbQuery)
                    {
                        (scriptVariableSet ?? (scriptVariableSet = new ScriptVariableSet())).SetValue(ScriptVariableKey_Database.DbBuilder, this);
                        queryString = GetSqlText_Query(compositeDbQuery, parameterSet, scriptVariableSet, log);
                    }
                    else if (query is DbStoredQuery storedDbQuery)
                    {
                        if (!storedDbQuery.QueryTexts.TryGetValue(Id, out queryString))
                        {
                            queryString = BuildQuery(storedDbQuery.Query, DbQueryParameterMode.Scripted, parameterSet, scriptVariableSet, log);
                            storedDbQuery.QueryTexts.Add(Id, queryString);
                        }
                    }

                    if (parameterMode != DbQueryParameterMode.Scripted)
                    {
                        parameterSet = parameterSet ?? new DataElementSet();
                        UpdateParameterSet(parameterSet, query);

                        if (query is DbStoredQuery storedDbQuery)
                        {
                            UpdateParameterSet(parameterSet, storedDbQuery.Query);
                        }

                        if (parameterSet?.Elements != null)
                        {
                            foreach (var parameter in parameterSet.Elements)
                            {
                                if (parameterMode == DbQueryParameterMode.ValueInjected)
                                {
                                    queryString = queryString.Replace(parameter?.CreateParameterWildString(),
                                        GetSqlText_Value(parameter?.GetObject(_scope, scriptVariableSet, log)?.ToString(), parameter.ValueType));
                                }
                                else
                                {
                                    queryString = queryString.Replace(parameter?.CreateParameterWildString(),
                                        parameter?.CreateParameterString());
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log?.AddError(
                        "Error trying to build query '" + (query?.Name ?? "(Undefinied)") + "'",
                        description: ex.ToString() + ". Built query is : '" + queryString + "'.");
                }
            }

            return queryString;
        }

        // Builds single query ----------------------

        /// <summary>
        /// Builds the SQL text of the specified basic query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the built query text.</returns>
        protected virtual string GetSqlText_Query(
            IDbSingleQuery query,
            IDataElementSet parameterSet = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            return "";
        }

        // Builds merge query ----------------------

        /// <summary>
        /// Builds the SQL text of the specified merge query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <returns>Returns the built query text.</returns>
        protected virtual string GetSqlText_Query(
            IDbCompositeQuery query,
            IDataElementSet parameterSet = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            return "";
        }

        // -------------------------------------------

        /// <summary>
        /// Gets the Sql string corresponding to the specified value.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <returns>Returns the Sql string.</returns>
        protected virtual string GetSqlText_Value(string value, DataValueType valueType = DataValueType.Text)
        {
            switch (valueType)
            {
                case DataValueType.Number:
                case DataValueType.Integer:
                case DataValueType.None:
                case DataValueType.Any:
                    return (value?.Trim()?.Length == 0 ? "null" : value);
                default:
                    return GetSqlText_Text(value);
            }
        }

        #endregion

        // --------------------------------------------------
        // IDisposable IMPLEMENTATION
        // --------------------------------------------------

        #region IDisposable Implementation

        /// <summary>
        /// Disposes specifying whether this instance is disposing.
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposing)
            {
                _scope?.Dispose();
            }
        }

        #endregion
    }
}
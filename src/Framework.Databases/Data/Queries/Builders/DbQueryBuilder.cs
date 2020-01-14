using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.Data.Common;
using BindOpen.Framework.Data.Depots;
using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Data.Helpers.Strings;
using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Extensions.Scriptwords;
using BindOpen.Framework.System.Diagnostics;
using BindOpen.Framework.System.Scripting;
using System;

namespace BindOpen.Framework.Data.Queries
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
        // BASIC QUERY BUILBING
        // ------------------------------------------

        #region Basic Query Building

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
        /// Builds the SQL text from the specified database query.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="query">The database data query to build.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="isParametersInjected">Indicates whether parameters are replaced.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <returns>Returns the built query text.</returns>
        public string BuildSqlText(
            IDbQuery query,
            IBdoLog log = null,
            IDataElementSet parameterSet = null,
            bool isParametersInjected = true,
            IBdoScriptVariableSet scriptVariableSet = null)
        {
            var queryString = "";

            if (query != null)
            {
                try
                {
                    if (query is BasicDbQuery basicDbQuery)
                    {
                        (scriptVariableSet ?? (scriptVariableSet = new ScriptVariableSet())).SetValue(ScriptVariableKey_Database.DbBuilder, this);
                        queryString = Build(basicDbQuery, log, parameterSet, scriptVariableSet);
                    }
                    else if (query is AdvancedDbQuery advancedDbQuery)
                    {
                        (scriptVariableSet ?? (scriptVariableSet = new ScriptVariableSet())).SetValue(ScriptVariableKey_Database.DbBuilder, this);
                        queryString = Build(advancedDbQuery, log, parameterSet, scriptVariableSet);
                    }
                    else if (query is StoredDbQuery storedDbQuery)
                    {
                        if (!storedDbQuery.QueryTexts.TryGetValue(Id, out queryString))
                        {
                            queryString = BuildSqlText(storedDbQuery.Query, log, parameterSet, false, scriptVariableSet);
                            storedDbQuery.QueryTexts.Add(Id, queryString);
                        }

                        return queryString;
                    }

                    if (isParametersInjected)
                    {
                        if (parameterSet == null)
                        {
                            parameterSet = query.ParameterSet;
                        }

                        if (parameterSet?.Elements != null)
                        {
                            foreach (var parameter in parameterSet.Elements)
                            {
                                queryString = queryString.Replace(parameter.CreateParameterString(),
                                    GetValuedSqlText(parameter.GetObject(_scope, scriptVariableSet, log).ToString(), parameter.ValueType));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.AddError(
                        "Error trying to build query '" + (query?.Name ?? "(Undefinied)") + "'",
                        description: ex.ToString() + ". Built query is : '" + queryString + "'.");
                }
            }

            return queryString;
        }

        // Builds simple query ----------------------

        /// <summary>
        /// Builds the specified simple database data query and put the result
        /// into the specified string MS Sql Server query.
        /// <remarks>We assume the query already exits.</remarks>
        /// </summary>
        /// <param name="query"></param>
        /// <param name="log">The log to consider.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="queryString"></param>
        /// <param name="isParametersInjected">Indicates whether parameters are replaced.</param>
        /// <returns>Returns the built query text.</returns>
        protected virtual string Build(
            IBasicDbQuery query,
            IBdoLog log = null,
            IDataElementSet parameterSet = null,
            IBdoScriptVariableSet scriptVariableSet = null)
        {
            return "";
        }


        // Builds advanced query ----------------------

        /// <summary>
        /// Builds the specified simple database data query and put the result
        /// into the specified string MS Sql Server query.
        /// <remarks>We assume the query already exits.</remarks>
        /// </summary>
        /// <param name="query"></param>
        /// <param name="log">The log to consider.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="queryString"></param>
        /// <returns>Returns the built query text.</returns>
        protected virtual string Build(
            IAdvancedDbQuery query,
            IBdoLog log = null,
            IDataElementSet parameterSet = null,
            IBdoScriptVariableSet scriptVariableSet = null)
        {
            return "";
        }

        /// <summary>
        /// Gets the Sql string corresponding to the specified value.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <returns>Returns the Sql string.</returns>
        protected virtual string GetValuedSqlText(string value, DataValueType valueType)
        {
            switch (valueType)
            {
                case DataValueType.Number:
                case DataValueType.Integer:
                case DataValueType.None:
                case DataValueType.Any:
                    return (value.Trim()?.Length == 0 ? "null" : value);
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
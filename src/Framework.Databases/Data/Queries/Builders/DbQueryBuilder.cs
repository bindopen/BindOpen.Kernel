using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Depots.Datasources;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Extensions.Carriers;
using BindOpen.Framework.Databases.Extensions.Connectors;
using BindOpen.Framework.Databases.Extensions.Scriptwords;
using System;

namespace BindOpen.Framework.Databases.Data.Queries.Builders
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

        private readonly BdoDbConnectorKind _databaseConnectorKind;

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
        /// <param name="databaseKind">The kind of database to consider.</param>
        /// <param name="scope">The scope to consider.</param>
        public DbQueryBuilder(
            BdoDbConnectorKind databaseKind,
            IBdoScope scope = null)
        {
            _databaseConnectorKind = databaseKind;
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
        /// Builds the specified simple database data query and put the result into the specified string query.
        /// </summary>
        /// <param name="query">The database data query to build.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="isParametersInjected">Indicates whether parameters are replaced.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="queryString">The output string query.</param>
        /// <returns>The log of the build task.</returns>
        public IBdoLog BuildQuery(
            IDbQuery query,
            IDataElementSet parameterSet,
            bool isParametersInjected,
            IBdoScriptVariableSet scriptVariableSet,
            out string queryString)
        {
            var log = new BdoLog();

            queryString = "";

            try
            {
                if (query is BasicDbQuery basicDbQuery)
                {
                    (scriptVariableSet ?? (scriptVariableSet = new ScriptVariableSet())).SetValue(ScriptVariableKey_Database.DbBuilder, this);
                    log.AddEvents(Build(basicDbQuery, parameterSet, scriptVariableSet, out queryString));
                }
                else if (query is AdvancedDbQuery advancedDbQuery)
                {
                    (scriptVariableSet ?? (scriptVariableSet = new ScriptVariableSet())).SetValue(ScriptVariableKey_Database.DbBuilder, this);
                    log.AddEvents(Build(advancedDbQuery, parameterSet, scriptVariableSet, out queryString));
                }
                else if (query is StoredDbQuery storedDbQuery)
                {
                    if (!storedDbQuery.QueryTexts.TryGetValue(Id, out queryString))
                    {
                        BuildQuery(storedDbQuery.Query, parameterSet, false, scriptVariableSet, out queryString);
                        storedDbQuery.QueryTexts.Add(Id, queryString);
                    }

                    return log;
                }

                if (isParametersInjected)
                {
                    if (parameterSet == null)
                    {
                        parameterSet = query.ParameterSet;
                    }

                    if (parameterSet!=null)
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
                    "Error trying to build query '" + (query.Name ?? "(Undefinied)") + "'",
                    description: ex.ToString() + ". Built query is : '" + queryString + "'.");
            }

            return null;
        }

        // Builds simple query ----------------------

        /// <summary>
        /// Builds the specified simple database data query and put the result
        /// into the specified string MS Sql Server query.
        /// <remarks>We assume the query already exits.</remarks>
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="queryString"></param>
        /// <param name="isParametersInjected">Indicates whether parameters are replaced.</param>
        protected virtual IBdoLog Build(
            IBasicDbQuery query,
            IDataElementSet parameterSet,
            IBdoScriptVariableSet scriptVariableSet,
            out string queryString)
        {
            queryString = "";
            return new BdoLog();
        }


        // Builds advanced query ----------------------

        /// <summary>
        /// Builds the specified simple database data query and put the result
        /// into the specified string MS Sql Server query.
        /// <remarks>We assume the query already exits.</remarks>
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="queryString"></param>
        protected virtual IBdoLog Build(
            IAdvancedDbQuery query,
            IDataElementSet parameterSet,
            IBdoScriptVariableSet scriptVariableSet,
            out string queryString)
        {
            queryString = "";
            return new BdoLog();
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
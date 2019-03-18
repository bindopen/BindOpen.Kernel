using bdo.core.application.scope;
using bdo.core.data.elements.sets;
using bdo.core.data.items.source;
using bdo.core.system.diagnostics;
using System;
using System.Xml.Serialization;

namespace bdo.standard.extension.connectors
{

    /// <summary>
    /// This class represents a repository data source.
    /// </summary>
    [Serializable()]
    public class RepositoryDataSource : DataSource
    {

        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The path of this instance.
        /// </summary>
        [XmlIgnore()]
        public String Path
        {
            get;
            set;
        }

        #endregion


        // -----------------------------------------------
        // CONSTRUCTORS
        // -----------------------------------------------

        #region Constructors

        /// <summary>
        /// This instantiates a new instance of the RepositoryDataSource class.
        /// </summary>
        public RepositoryDataSource() 
            : base()
        {
        }

        #endregion


        // -----------------------------------------------
        // CREATION
        // -----------------------------------------------

        #region Creation

        /// <summary>
        /// Creates a data source of the specified unique name.
        /// </summary>
        /// <param name="aAppScope">The application scope to consider.</param>
        /// <param name="aName">The name of this instance.</param>
        /// <param name="aConnectorUniqueName">The unique name of connector to consider.</param>
        /// <param name="aConnectionString">The connection string of this instance.</param>
        /// <param name="aDetail">The detail to consider.</param>
        /// <param name="aLog">The log to consider.</param>
        /// <returns>The created data source.</returns>
        public new static RepositoryDataSource Create(
            AppScope aAppScope,
            String aName,
            String aConnectorUniqueName,
            String aConnectionString = null,
            DataElementSet aDetail = null,
            Log aLog = null)
        {
            aConnectorUniqueName = RepositoryConnector.GetConnectorUniqueName(aConnectorUniqueName);

            return DataSource.Create(aAppScope, aName, aConnectorUniqueName, aConnectionString, aDetail, aLog) as RepositoryDataSource;
        }

        /// <summary>
        /// Creates a data source of the specified unique name.
        /// </summary>
        /// <param name="aAppScope">The application scope to consider.</param>
        /// <param name="aName">The name of this instance.</param>
        /// <param name="aConnectorUniqueName">The unique name of connector to consider.</param>
        /// <param name="aConnectionString">The connection string of this instance.</param>
        /// <param name="aDynamicObject">The dynamic object of this instance.</param>
        /// <param name="aLog">The log to consider.</param>
        /// <returns>The created data source.</returns>
        public new static RepositoryDataSource Create(
            AppScope aAppScope,
            String aName,
            String aConnectorUniqueName,
            String aConnectionString = null,
            dynamic aDynamicObject = null,
            Log aLog = null)
        {
            aConnectorUniqueName = RepositoryConnector.GetConnectorUniqueName(aConnectorUniqueName);

            return DataSource.Create(aAppScope, aName, aConnectorUniqueName, aDynamicObject, aLog) as RepositoryDataSource;
        }

        /// <summary>
        /// Creates a data source of the specified unique name.
        /// </summary>
        /// <param name="aAppScope">The application scope to consider.</param>
        /// <param name="aName">The name of this instance.</param>
        /// <param name="aConnectorUniqueName">The unique name of connector to consider.</param>
        /// <param name="aDataSource">The database source to consider.</param>
        /// <param name="aLog">The log to consider.</param>
        /// <returns>The created data source.</returns>
        public new static RepositoryDataSource Create(
            AppScope aAppScope,
            String aName,
            String aConnectorUniqueName,
            DataSource aDataSource = null,
            Log aLog = null)
        {
            aConnectorUniqueName = RepositoryConnector.GetConnectorUniqueName(aConnectorUniqueName);

            return DataSource.Create(aAppScope, aName, aConnectorUniqueName, aDataSource, aLog) as RepositoryDataSource;
        }

        #endregion

    }
}

using BindOpen.Framework.Core.Data.Depots;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items.Datasources;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using BindOpen.Framework.Databases.Data.Queries;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Framework.Databases.Data.Depots.DbQueries
{
    /// <summary>
    /// This class represents a data queries depot.
    /// </summary>
    [Serializable()]
    [XmlType("DbQueriesDepot", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "dBQueries.depot", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoDbQueryDepot : TBdoDepot<DbQuery>, IBdoDbQueryDepot
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Queries of this instance.
        /// </summary>
        [XmlArray("queries")]
        [XmlArrayItem("add")]
        public List<DbQuery> Queries
        {
            get { return _items; }
            set { _items = value; }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoDbQueryDepot class.
        /// </summary>
        public BdoDbQueryDepot() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoDbQueryDepot class.
        /// </summary>
        /// <param name="queries">The queries to consider.</param>
        public BdoDbQueryDepot(params DbQuery[] queries) : base(queries)
        {
            Id = "dbQueries";
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the database query with the specified name.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <returns>Returns the database query with the specified name.</returns>
        public DbQuery GetQuery(string name) => GetItem(name);

        #endregion
    }
}

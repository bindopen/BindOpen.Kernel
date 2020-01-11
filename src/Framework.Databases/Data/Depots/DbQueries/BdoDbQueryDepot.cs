using BindOpen.Framework.Data.Queries;
using BindOpen.Framework.System.Assemblies;
using BindOpen.Framework.System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace BindOpen.Framework.Data.Depots
{
    /// <summary>
    /// This class represents a data queries depot.
    /// </summary>
    [Serializable()]
    [XmlType("DbQueriesDepot", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "dBQueries.depot", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoDbQueryDepot : TBdoDepot<StoredDbQuery>, IBdoDbQueryDepot
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
        public List<StoredDbQuery> Queries
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
        public BdoDbQueryDepot(params StoredDbQuery[] queries) : base(queries)
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
        public StoredDbQuery GetQuery(string name) => GetItem(name);

        /// <summary>
        /// Add the items from the specified assembly.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly.</param>
        /// <param name="log">The log to consider.</param>
        public override IBdoDepot AddFromAssembly(string assemblyName, IBdoLog log = null)
        {
            Assembly assembly = AppDomain.CurrentDomain.GetAsssembly(assemblyName);
            if (assembly != null)
            {
                var types = assembly.GetTypes().Where(p => p.GetCustomAttributes(typeof(DbQueryDepotAttribute)).Any());
                foreach (Type type in types)
                {
                    // we feach methods
                    var methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.Static);
                    foreach (MethodInfo methodInfo in methodInfos)
                    {
                        if (methodInfo.GetCustomAttribute(typeof(StoredDbQueryAttribute)) is StoredDbQueryAttribute dbQueryAttribute)
                        {
                            if (methodInfo.GetParameters().Length > 0)
                            {
                                log?.AddError("Method '" + methodInfo.Name + "' must not have parameters as it defines a stored database query");
                            }
                            else
                            {
                                // we determine the name of the definition

                                IDbQuery query = methodInfo.Invoke(null, null) as IDbQuery;

                                string queryName = dbQueryAttribute.Name;
                                if (string.IsNullOrEmpty(queryName))
                                {
                                    queryName = query.Name;
                                }

                                if (query != null)
                                {
                                    StoredDbQuery storedQuery = new StoredDbQuery(query, queryName);
                                    Add(storedQuery);
                                }
                            }
                        }
                    }
                }
            }


            return this;
        }

        #endregion
    }
}

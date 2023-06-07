namespace BindOpen.Scoping.Data.Stores
{
    /// <summary>
    /// This class represents a data source depot.
    /// </summary>
    public class BdoDatasourceDepot : TBdoDepot<IBdoDatasource>, IBdoSourceDepot
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoDatasourceDepot class.
        /// </summary>
        public BdoDatasourceDepot() : base()
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the module name of the specified data source.
        /// </summary>
        /// <param key="sourceName">The name of the data source to consider.</param>
        /// <returns>The module name corresponding to the specified data module name.</returns>
        public string GetModuleName(string sourceName = null)
        {
            IBdoDatasource source = Get(sourceName);

            return source != null ? source.ModuleName : null;
        }

        /// <summary>
        /// Returns the instance name of the specified data source.
        /// </summary>
        /// <param key="sourceName">The name of the data source to consider.</param>
        /// <returns>The instance name corresponding to the specified data module name.</returns>
        public string GetInstanceName(string sourceName = null)
        {
            IBdoDatasource source = Get(sourceName);

            return source != null ? source.InstanceName : null;
        }

        /// <summary>
        /// Returns the instance name otherwise module name of the specified data source.
        /// </summary>
        /// <param key="sourceName">The name of the data source to consider.</param>
        /// <returns>The instance name corresponding to the specified data module name.</returns>
        public string GetInstanceOtherwiseModuleName(string sourceName = null)
        {
            IBdoDatasource source = Get(sourceName);

            string name = (source == null ?
                null :
                source.InstanceName?.Length > 0 ? source.InstanceName : source.ModuleName);

            return name;
        }

        #endregion
    }
}
namespace BindOpen.Framework.Databases.Extensions.Carriers
{
    /// <summary>
    /// This static class represents a factory of data table.
    /// </summary>
    public static class DbTableFactory
    {
        /// <summary>
        /// Creates a new instance of the DbTable class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModuleName">The name of the data module to consider.</param>
        public static DbTable Create(string name, string schema = null, string dataModuleName = null)
            => new DbTable()
            {
                Name = name,
                DataModule = dataModuleName,
                Schema = schema,
            };
    }
}

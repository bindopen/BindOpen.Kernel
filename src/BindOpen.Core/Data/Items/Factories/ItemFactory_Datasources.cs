namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data item factory.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    public static partial class ItemFactory
    {
        // Data sources -----------------------------

        /// <summary>
        /// Instantiates a new instance of the Datasource class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="kind">The kind of the data source to consider.</param>
        public static Datasource CreateDatasource(
                string name,
                DatasourceKind kind)
        {
            var datasource = new Datasource(name)
            {
                Kind = kind
            };

            return datasource;
        }
    }
}

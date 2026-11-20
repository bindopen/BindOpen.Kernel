namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data item factory.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    public static partial class BdoData
    {
        /// <summary>
        /// The name of this meta data.
        /// </summary>
        public static readonly string __VarName_This = "$this";

        /// <summary>
        /// Instantiates a new instance of the NewResultItem class.
        /// </summary>
        /// <returns>The new instance of the NewResultItem class.</returns>
        public static ResultItem NewResultItem(ResourceStatus status = ResourceStatus.None, string key = null)
        {
            var result = new ResultItem()
                .WithKey(key)
                .WithStatus(status);

            return result;
        }

        /// <summary>
        /// Instantiates a new instance of the NewResultItem class.
        /// </summary>
        /// <returns>The new instance of the NewResultItem class.</returns>
        public static ResultItem NewResultItem(string key, ResourceStatus status = ResourceStatus.None)
            => NewResultItem(status, key);
    }
}

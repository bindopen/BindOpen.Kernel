namespace BindOpen.Kernel.Data
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
        public static BdoResultItem NewResultItem(string key, ResourceStatus status = ResourceStatus.None)
        {
            var result = new BdoResultItem()
                .WithKey(key)
                .WithStatus(status);

            return result;
        }
    }
}

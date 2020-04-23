using BindOpen.Data.Items;

namespace BindOpen.System.Assemblies.References
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoAssemblyReference : IDataItem
    {
        /// <summary>
        /// The library name of this instance.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The library version of this instance.
        /// </summary>
        string Version { get; set; }

        /// <summary>
        /// The file name of this instance.
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        /// Sets the file name of this instance.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>Returns this instance.</returns>
        IBdoAssemblyReference WithFileName(string fileName);
    }
}
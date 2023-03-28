namespace BindOpen.Data.Assemblies
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IBdoAssemblyReferenceExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="assemblyFileName"></param>
        public static T WithFileName<T>(
            this T obj,
            string assemblyFileName)
            where T : IBdoAssemblyReference
        {
            if (obj != null)
            {
                obj.AssemblyFileName = assemblyFileName;
            }
            return obj;
        }
    }
}

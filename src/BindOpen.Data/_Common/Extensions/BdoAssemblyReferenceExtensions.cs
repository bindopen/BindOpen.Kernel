namespace BindOpen.Data.Assemblies
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class BdoAssemblyReferenceExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyFileName"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="definitionUniqueId"></param>
        public static T WithDefinitionUniqueId<T>(
            this T obj,
            string definitionUniqueId)
            where T : IBdoAssemblyReference
        {
            if (obj != null)
            {
                obj.DefinitionUniqueId = definitionUniqueId;
            }
            return obj;
        }
    }
}

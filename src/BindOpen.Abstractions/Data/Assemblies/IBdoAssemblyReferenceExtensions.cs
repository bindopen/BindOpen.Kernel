using BindOpen.Data.Assemblies;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IBdoAssemblyReferenceExtensions
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
        /// <param name="definitionUniqueName"></param>
        public static T WithDefinitionUniqueName<T>(
            this T obj,
            string definitionUniqueName)
            where T : IBdoAssemblyReference
        {
            if (obj != null)
            {
                obj.DefinitionUniqueName = definitionUniqueName;
            }
            return obj;
        }
    }
}

using System;

namespace BindOpen.System.Data.Assemblies
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

        /// <summary>
        /// 
        /// </summary>
        /// <param key="assemblyFileName"></param>
        public static T WithAssemblyVersion<T>(
            this T obj,
            Version assemblyVersion)
            where T : IBdoAssemblyReference
        {
            if (obj != null)
            {
                obj.AssemblyVersion = assemblyVersion;
            }
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="assemblyFileName"></param>
        public static T WithAssemblyFileName<T>(
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

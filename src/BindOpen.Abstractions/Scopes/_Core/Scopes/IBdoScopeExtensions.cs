using BindOpen.Data;

namespace BindOpen.Scopes
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static class IBdoScopeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="assemblyFileName"></param>
        public static bool IsScope(this BdoDataType dataType)
        {
            return dataType >= typeof(IBdoScope);
        }
    }
}

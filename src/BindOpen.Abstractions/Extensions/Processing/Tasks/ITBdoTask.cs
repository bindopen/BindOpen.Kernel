using BindOpen.Meta;
using BindOpen.Meta.Elements;
using BindOpen.Runtime.Scopes;
using BindOpen.Logging;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoTask<T> : IBdoTask
        where T : IBdoTask
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="relativePath"></param>
        new T UpdateAbsolutePaths(string relativePath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varElementSet">The variable element set to consider.</param>
        /// <param name="runtimeMode"></param>
        /// <param name="log"></param>
        new T Execute(
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            RuntimeModes runtimeMode = RuntimeModes.Normal,
            IBdoLog log = null);
    }
}
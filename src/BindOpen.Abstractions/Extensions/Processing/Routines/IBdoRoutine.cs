using BindOpen.Extensions;
using BindOpen.Data.Elements;
using BindOpen.Runtime.Definition;
using BindOpen.Runtime.Scopes;
using BindOpen.Logging;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoRoutine : ITBdoExtensionItem<IBdoRoutineDefinition, IBdoRoutineConfiguration, IBdoRoutine>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varElementSet">The variable element set to consider.</param>
        /// <param name="item"></param>
        /// <param name="dataElement"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        IBdoRoutine Execute(
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            object item = null,
            IBdoElement dataElement = null,
            IBdoLog log = null,
            params object[] objects);
    }
}
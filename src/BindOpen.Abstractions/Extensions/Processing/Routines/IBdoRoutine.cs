using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Runtime.Definition;
using BindOpen.Runtime.Scopes;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoRoutine : ITBdoExtensionItem<IBdoRoutine, IBdoRoutineDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet">The variable element set to consider.</param>
        /// <param name="item"></param>
        /// <param name="dataElement"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        IBdoRoutine Execute(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            object item = null,
            IBdoMetaItem dataElement = null,
            IBdoLog log = null,
            params object[] objects);
    }
}
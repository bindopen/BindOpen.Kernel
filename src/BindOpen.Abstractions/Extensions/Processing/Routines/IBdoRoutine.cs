using BindOpen.Extensions;
using BindOpen.Data.Meta;
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
        /// <param name="varSet">The variable element set to consider.</param>
        /// <param name="item"></param>
        /// <param name="dataElement"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        IBdoRoutine Execute(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            object item = null,
            IBdoMetaData dataElement = null,
            IBdoLog log = null,
            params object[] objects);
    }
}
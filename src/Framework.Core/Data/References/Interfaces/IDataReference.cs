using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.References
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataReference : IDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        object SourceObject { get; }

        /// <summary>
        /// 
        /// </summary>
        object TargetObject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IDataReferenceDto Dto { get; }

        /// <summary>
        /// 
        /// </summary>
        IStoredDataItem RootSource { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        void SetDto(IDataReferenceDto item);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDatasource GetDatasource();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        object Get(
            IBdoScope scope = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null);
    }
}
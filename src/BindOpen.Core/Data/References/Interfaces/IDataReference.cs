using BindOpen.Application.Scopes;
using BindOpen.Data.Items;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;

namespace BindOpen.Data.References
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
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        object Get(
            IBdoScope scope = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null);
    }
}
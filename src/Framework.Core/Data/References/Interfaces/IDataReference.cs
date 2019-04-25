using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.References
{
    public interface IDataReference : IDataItem
    {
        object SourceObject { get; }
        object TargetObject { get; set; }

        IDataReferenceDto Dto { get; }

        void SetDto(IDataReferenceDto item);

        IStoredDataItem RootSource { get; }

        IDataSource GetDataSource();

        object Get(
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null);
    }
}
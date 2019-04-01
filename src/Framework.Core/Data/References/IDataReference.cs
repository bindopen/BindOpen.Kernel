using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.References
{
    public interface IDataReference : IDataItem
    {
        string DataHandlerUniqueName { get; set; }
        IDataElementSet PathDetail { get; set; }
        object Source { get; }
        IDataElement SourceElement { get; set; }
        object TargetItem { get; set; }

        IDataSource GetDataSource();

        DataSourceKind GetDataSourceKind();

        IStoredDataItem GetPrimarySource();

        DataValueType GetPrimarySourceValueType();

        object Get(IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null);

        List<object> Post(List<object> items, IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null);
    }
}
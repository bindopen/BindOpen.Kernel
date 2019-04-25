using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;

namespace BindOpen.Framework.Core.Extensions
{
    public interface IAppExtensionFilter : IDataItem
    {
        string FileName { get; set; }
        string FolderPath { get; set; }
        string Name { get; set; }
        List<DataSourceKind> SourceKinds { get; set; }
    }
}
using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Definition.Libraries;

namespace BindOpen.Framework.Core.Extensions
{
    public interface IAppExtensionFilter
    {
        string FileName { get; set; }
        string FolderPath { get; set; }
        string Name { get; set; }
        List<DataSourceKind> SourceKinds { get; set; }

        ILibraryDefinition ToDefinition();
    }
}
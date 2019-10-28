using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Definition.Libraries;

namespace BindOpen.Framework.Core.Extensions.Indexes.Libraries
{
    public interface ILibraryIndex : IDescribedDataItem
    {
        List<ILibraryDefinition> Definitions { get; }

        ILibraryDefinition GetDefinition(string name);

        List<ILibraryDefinition> GetDefinitions(List<string> names = null);

        List<string> GetLibraryNames();
    }
}
using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;

namespace BindOpen.Framework.Core.Extensions
{
    public interface IAppExtensionConfiguration : IDataItem
    {
        string DefaultFolderPath { get; set; }

        List<DataSourceKind> DefaultSourceKinds { get; set; }

        List<IAppExtensionFilter> Filters { get; set; }

        List<IAppExtensionFilter> GetFilters();

        IAppExtensionConfiguration AddFilter(
            string libraryName = null,
            string libraryFileName = null,
            DataSourceKind[] sourceKinds = null,
            string libraryFolderPath = null);

        IAppExtensionConfiguration AddFilter(IAppExtensionFilter filter);

        void Merge(IAppExtensionConfiguration configuration);
    }
}
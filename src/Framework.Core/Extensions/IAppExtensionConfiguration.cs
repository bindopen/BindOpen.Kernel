using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;

namespace BindOpen.Framework.Core.Extensions
{
    public interface IAppExtensionConfiguration : IDataItem
    {
        string DefaultFolderPath { get; set; }

        List<DataSourceKind> DefaultSourceKinds { get; set; }

        List<AppExtensionFilter> Filters { get; set; }

        List<AppExtensionFilter> GetFilters();

        AppExtensionConfiguration Add(
            string libraryName = null,
            string libraryFileName = null,
            DataSourceKind[] sourceKinds = null,
            string libraryFolderPath = null);

        AppExtensionConfiguration AddFilter(AppExtensionFilter filter);

        void Merge(AppExtensionConfiguration configuration);
    }
}
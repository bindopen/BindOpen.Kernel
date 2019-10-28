using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;

namespace BindOpen.Framework.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppExtensionConfiguration : IDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        string DefaultFolderPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<DataSourceKind> DefaultSourceKinds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<IAppExtensionFilter> Filters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<IAppExtensionFilter> GetFilters();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="libraryName"></param>
        /// <param name="libraryFileName"></param>
        /// <param name="sourceKinds"></param>
        /// <param name="libraryFolderPath"></param>
        /// <returns></returns>
        IAppExtensionConfiguration AddFilter(
            string libraryName = null,
            string libraryFileName = null,
            DataSourceKind[] sourceKinds = null,
            string libraryFolderPath = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IAppExtensionConfiguration AddFilter(IAppExtensionFilter filter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        void Merge(IAppExtensionConfiguration configuration);
    }
}
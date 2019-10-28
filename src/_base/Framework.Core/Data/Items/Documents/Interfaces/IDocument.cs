using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Items.Carriers;
using BindOpen.Framework.Core.Extensions.Items.Entities;
using BindOpen.Framework.Core.Extensions.Items.Formats;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Items.Documents
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        /// 
        /// </summary>
        CarrierConfiguration Container { get; set; }

        /// <summary>
        /// 
        /// </summary>
        EntityConfiguration Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        IFormatConfiguration DetectFormat(IDataSource dataSource, ref ILog log);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentDataItem"></param>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        ILog Update(IDocument documentDataItem, string relativePath = "");
    }
}
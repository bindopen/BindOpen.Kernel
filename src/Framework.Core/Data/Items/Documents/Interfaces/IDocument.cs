using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
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
        BdoCarrierConfiguration Container { get; set; }

        /// <summary>
        /// 
        /// </summary>
        BdoEntityConfiguration Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        IBdoFormatConfiguration DetectFormat(IDatasource dataSource, ref IBdoLog log);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentDataItem"></param>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        IBdoLog Update(IDocument documentDataItem, string relativePath = "");
    }
}
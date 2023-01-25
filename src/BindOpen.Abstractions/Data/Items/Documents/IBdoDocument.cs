using BindOpen.Extensions.Modeling;
using BindOpen.Logging;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDocument
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoEntityConfiguration Container { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoEntityConfiguration Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        IBdoFormatConfiguration DetectFormat(IBdoDatasource dataSource, ref IBdoLog log);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentDataItem"></param>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        IBdoLog Update(IBdoDocument documentDataItem, string relativePath = "");
    }
}
using BindOpen.Data.Configuration;
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
        IBdoConfiguration Container { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoConfiguration Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        IBdoConfiguration DetectFormat(IBdoDatasource dataSource, ref IBdoLog log);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentDataItem"></param>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        IBdoLog Update(IBdoDocument documentDataItem, string relativePath = "");
    }
}
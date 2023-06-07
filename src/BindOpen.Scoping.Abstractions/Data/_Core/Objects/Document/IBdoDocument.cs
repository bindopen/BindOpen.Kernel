using BindOpen.Scoping.Data.Meta;
using BindOpen.Logging;

namespace BindOpen.Scoping.Data
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
        /// <param key="dataSource"></param>
        /// <param key="log"></param>
        /// <returns></returns>
        IBdoConfiguration DetectFormat(IBdoDatasource dataSource, ref IBdoLog log);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="documentDataItem"></param>
        /// <param key="relativePath"></param>
        /// <returns></returns>
        IBdoLog Update(IBdoDocument documentDataItem, string relativePath = "");
    }
}
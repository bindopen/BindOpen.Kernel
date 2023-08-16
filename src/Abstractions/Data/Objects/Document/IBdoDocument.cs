using BindOpen.System.Data.Meta;
using BindOpen.System.Logging;

namespace BindOpen.System.Data
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
        /// <param key="documentDataItem"></param>
        /// <param key="relativePath"></param>
        /// <returns></returns>
        IBdoLog Update(IBdoDocument documentDataItem, string relativePath = "");
    }
}
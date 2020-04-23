using BindOpen.System.Diagnostics;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This interface represents a savable DTO.
    /// </summary>
    public interface ISavable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        bool SaveXml(string filePath, IBdoLog log = null);
    }
}

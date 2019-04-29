using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Items.Dto
{
    /// <summary>
    /// This interface represents a savable DTO.
    /// </summary>
    public interface ISavable
    {
        bool SaveXml(string filePath, ILog log = null);
    }
}

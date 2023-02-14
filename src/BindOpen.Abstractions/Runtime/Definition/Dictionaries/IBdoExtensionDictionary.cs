using BindOpen.Data;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoExtensionDictionary :
        IIdentified, INamed,
        IBdoTitled, IBdoDescribed,
        IStorable
    {
    }
}
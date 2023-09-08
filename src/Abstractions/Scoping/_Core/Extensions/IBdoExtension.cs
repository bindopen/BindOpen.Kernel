using BindOpen.Kernel.Data;

namespace BindOpen.Kernel.Scoping
{
    /// <summary>
    /// This class represents a BindOpen extension runtime item.
    /// </summary>
    public interface IBdoExtension : IBdoObject, IIdentified, IBdoDefinable
    {
        /// <summary>
        /// 
        /// </summary>
        BdoExtensionKinds ExtensionKind { get; set; }
    }
}


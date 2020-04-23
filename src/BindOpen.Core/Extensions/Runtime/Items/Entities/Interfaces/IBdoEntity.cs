using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Extensions.Definition;
using BindOpen.System.Diagnostics;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoEntity : ITBdoExtensionItem<BdoEntityDefinition>, INamed
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        IObjectElement AsElement(string name = null, IBdoLog log = null);
    }
}
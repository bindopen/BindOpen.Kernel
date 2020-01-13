using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Extensions.Definition;
using BindOpen.Framework.System.Diagnostics;

namespace BindOpen.Framework.Extensions.Runtime
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
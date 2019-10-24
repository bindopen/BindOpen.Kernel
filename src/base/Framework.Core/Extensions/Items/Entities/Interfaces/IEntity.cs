using BindOpen.Framework.Core.Data.Elements._Object;
using BindOpen.Framework.Core.Data.Items.Dto;
using BindOpen.Framework.Core.Extensions.Items.Entities.Definition;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Items.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEntity : ITAppExtensionItem<EntityDefinition>, INamed
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        IObjectElement AsElement(string name = null, ILog log = null);
    }
}
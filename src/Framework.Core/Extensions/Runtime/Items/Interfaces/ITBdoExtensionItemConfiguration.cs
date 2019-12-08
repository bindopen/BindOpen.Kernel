using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Definition.Items;

namespace BindOpen.Framework.Core.Extensions.Runtime.Items
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITBdoExtensionItemConfiguration<T> : IBdoBaseConfiguration, IReferenced
        where T : IBdoExtensionItemDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        BdoExtensionItemKind Kind { get; }

        /// <summary>
        /// 
        /// </summary>
        string DefinitionUniqueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Group { get; set; }
    }
}
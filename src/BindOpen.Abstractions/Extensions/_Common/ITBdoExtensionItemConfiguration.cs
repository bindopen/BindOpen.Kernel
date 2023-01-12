using BindOpen.Abstractions.Meta.Configuration;
using BindOpen.Meta;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITBdoExtensionItemConfiguration<T> : IBdoConfiguration, IReferenced
        where T : IBdoExtensionItemDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        BdoExtensionItemKind Kind { get; }

        /// <summary>
        /// 
        /// </summary>
        string DefinitionUniqueId { get; }

        /// <summary>
        /// 
        /// </summary>
        string GroupId { get; set; }
    }
}
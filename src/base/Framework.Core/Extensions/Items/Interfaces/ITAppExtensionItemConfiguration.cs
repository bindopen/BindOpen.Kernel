using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Data.Items.Dto;
using BindOpen.Framework.Core.Extensions.Common;

namespace BindOpen.Framework.Core.Extensions.Items
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITAppExtensionItemConfiguration<T> : IBaseConfiguration, IReferenced
        where T : IAppExtensionItemDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        AppExtensionItemKind Kind { get; }

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
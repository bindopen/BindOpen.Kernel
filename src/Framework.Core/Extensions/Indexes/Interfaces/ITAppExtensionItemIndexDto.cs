using System.Collections.Generic;
using BindOpen.Framework.Core.Extensions.Items;

namespace BindOpen.Framework.Core.Extensions.Indexes
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITAppExtensionItemIndexDto<T> : IAppExtensionItemIndexDto where T : AppExtensionItemDefinitionDto
    {
        /// <summary>
        /// The definitions.
        /// </summary>
        List<T> Definitions { get; set; }

        /// <summary>
        /// The groups.
        /// </summary>
        List<AppExtensionItemGroup> Groups { get; }

        /// <summary>
        /// ID of library.
        /// </summary>
        string LibraryId { get; set; }

        /// <summary>
        /// Name of library.
        /// </summary>
        string LibraryName { get; set; }
    }
}
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Definition;

namespace BindOpen.Framework.Core.Extensions.Configuration
{
    public interface ITAppExtensionItemConfiguration<T> : INamedDataItem where T : IAppExtensionItemDefinition
    {
        T Definition { get; }
        string DefinitionUniqueId { get; set; }
        string Group { get; set; }

        AppExtensionItemKind GetExtensionItemKind();

        void SetDefinition(T definition);
    }
}
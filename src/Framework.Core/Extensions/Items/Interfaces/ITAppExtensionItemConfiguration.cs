using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Data.Items.Dto;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Definitions;

namespace BindOpen.Framework.Core.Extensions.Items
{
    public interface ITAppExtensionItemConfiguration<T> : IConfiguration, IReferenced
        where T : IAppExtensionItemDefinition
    {
        AppExtensionItemKind Kind { get; }

        string DefinitionUniqueId { get; set; }

        string Group { get; set; }
    }
}
using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Data.Meta.Reflection;

namespace BindOpen.System.Scoping
{
    /// <summary>
    /// This static class provides methods to handle configs.
    /// </summary>
    public static class BdoExtensions
    {
        public static IBdoConfiguration ToConfig(
            this IBdoExtension extension,
            IBdoScope scope,
            string name = null,
            bool onlyMetaAttributes = true)
        {
            IBdoConfiguration config = null;

            if (scope != null && extension != null)
            {
                var extensionKind = extension.GetValueType().GetExtensionKind();
                var extensionDefinition = scope.ExtensionStore?.GetDefinitionFromType(
                    extensionKind,
                    BdoData.Class(extension.GetType()));

                config = BdoData.NewConfig(name)
                    .WithDefinition(extensionKind, extensionDefinition?.UniqueName)
                    .WithDataType(extension.GetValueType())
                    .WithId(extension?.Id)
                    .WithData(extension)
                    .UpdateTree(onlyMetaAttributes)
                    .WithName(name);

                return config;
            }

            return config;
        }
    }
}
